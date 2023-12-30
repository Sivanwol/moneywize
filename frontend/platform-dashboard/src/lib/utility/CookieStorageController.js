import Cookies from 'js-cookie';
import Crypt from '@5no/crypt';
import { SAUTH_TOKEN } from '../constants';

const getItem = key => {
  const data = Cookies.get(key);
  if (!data) return null;
  try {
    const token = SAUTH_TOKEN(key);
    const decrypt = Crypt(data, token).decrypt();
    return JSON.parse(decrypt);
  } catch (err) {
    return data;
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