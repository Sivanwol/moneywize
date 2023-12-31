import Cookies from 'js-cookie';

const getItem = key => {
  const data = Cookies.get(key);
  if (!data) {
    return null;
  }
  try {
    return JSON.parse(data);
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
  const stringify = typeof value !== 'string' ? JSON.stringify(value) : value;
  if (expire > 0) {
    return Cookies.set(key, stringify, { expires: expire });
  }
  return Cookies.set(key, stringify);
};

const removeItem = key => {
  Cookies.remove(key);
};

export { getItem, setItem, hasItem, removeItem };