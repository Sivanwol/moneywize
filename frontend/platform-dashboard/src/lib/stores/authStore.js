import { getItem, hasItem, removeItem, setItem } from '@utility/CookieStorageController';
import { AuthJWT } from '@models/AuthJWT.js';
import moment from 'moment';

/** @type {(AuthJWT)} */
export function createAuth(initialValue = null) {
	const AuthCookieKey = 'app_user_auth';
	return {
		login(userId, accessToken, refreshToken, expireAt) {
			const auth = AuthJWT.fromJson({
				userId,
				accessToken,
				refreshToken,
				expireAt
			});
			removeItem(AuthCookieKey);
			const expiresInDays = auth.expireAt.days();
			setItem(AuthCookieKey, JSON.stringify(auth), expiresInDays);
		},

		/** @returns {(string| null)} */
		getRefreshToken() {
			try {
				/** @type {(AuthJWT)} */
				const auth = getItem(AuthCookieKey);
				if (!auth) return null;
				return auth.refreshToken;
			} catch (e) {
				console.log(e);
				removeItem(AuthCookieKey);
			}
			return null;
		},
		/** @returns {(string| null)} */
		getUserId() {
			try {
				/** @type {(AuthJWT)} */
				const auth = getItem(AuthCookieKey);
				if (!auth) return null;
				return auth.userId;
			} catch (e) {
				removeItem(AuthCookieKey);
				return null;
			}
		},
		reloadTokens({ accessToken, refreshToken, expireAt }) {
			try {
				/** @type {(AuthJWT)} */
				const authOld = getItem(AuthCookieKey);
				if (!authOld) return null;
				const userId = authOld.userId;
				const auth = AuthJWT.fromJson({
					userId,
					accessToken,
					refreshToken,
					expireAt
				});
				const expiresInDays = auth.expireAt.days();
				removeItem(AuthCookieKey);
				setItem(AuthCookieKey, JSON.stringify(auth), expiresInDays);
			} catch (e) {// invalid auth object in cookie someone has tempered with it
				console.log(e);
				removeItem(AuthCookieKey);
			}
		},
		/** @returns {(string| null)} */
		getAccessToken() {
			try {
				/** @type {(AuthJWT)} */
				const auth = getItem(AuthCookieKey);
				if (!auth) return null;
				return auth.accessToken;
			} catch (e) {// invalid auth object in cookie someone has tempered with it
				console.log(e);
				removeItem(AuthCookieKey);
			}
			return null;
		},
		isLoggedIn() {
			const hasAuth = hasItem(AuthCookieKey);
			let hasAuthValid = true;
			try {
				/** @type {(AuthJWT)} */
				const auth = getItem(AuthCookieKey);
				if (!auth) hasAuthValid = false;
			} catch (e) { // invalid auth object in cookie someone has tempered with it
				console.log(e);
				debugger;
				hasAuthValid = false;
				removeItem(AuthCookieKey);
			}
			return hasAuthValid && hasAuth;
		},

		/** @returns {(boolean| null)} */
		isRenewToken() {
			try {
				/** @type {(AuthJWT)} */
				const auth = getItem(AuthCookieKey);
				if (!auth) return null;
				const now = moment();
				const expireAt = moment(auth.expireAt);
				const diff = expireAt.diff(now, 'minutes');
				return diff < 3;
			} catch (e) {// invalid auth object in cookie someone has tempered with it
				console.log(e);
				removeItem(AuthCookieKey);
			}
			return null;
		},

		logout(note) {
			localStorage.clear();
			removeItem(AuthCookieKey);
		}
	};

}