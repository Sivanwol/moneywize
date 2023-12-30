import Cookies from 'js-cookie';
import Crypt from '@5no/crypt';
import { SAUTH_TOKEN } from '../constants';

const getItem = key => {
  const data = Cookies.get(key);
  if (!data) {
    // system will save null value to cookies if we remove it
    removeItem(key);
    return null;
  }
  try {
    const token = SAUTH_TOKEN(key);
    const decrypt = Crypt(data, token).decrypt();
    return JSON.parse(decrypt);
  } catch (err) {
    // this is not a json string and not matched for decrypt so we remove it from cookies
    removeItem(key);
    return null;
  }
};

const hasItem = key => {
  return Cookies.get(key) !== undefined;
};
const setItem = (key, value, expire = -1) => {
  const token = SAUTH_TOKEN(key);
  const stringify = typeof value !== 'string' ? JSON.stringify(value) : value;
  const encrypt = Crypt(stringify, token).encrypt();
  if (expire > 0) {
    return Cookies.set(key, encrypt, { expires: expire });
  }
  return Cookies.set(key, encrypt);
};

const removeItem = key => {
  Cookies.remove(key);
};

export { getItem, setItem, hasItem, removeItem };