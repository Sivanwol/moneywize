import { writable } from 'svelte/store';
import { getItem, hasItem, removeItem, setItem } from '@utility/CookieStorageController';
import { AuthJWT } from '@models/AuthJWT.js';
import moment from 'moment';

/** @type {(AuthJWT)} */
export function createAuth(initialValue = null) {
	const { subscribe, update } = writable(initialValue);

	return {
		subscribe,
		login(userId, accessToken, refreshToken, expireAt) {
			const auth = AuthJWT.fromJson({
				userId,
				accessToken,
				refreshToken,
				expireAt
			});
			removeItem('auth');
			const expiresInDays = auth.expireAt.days();
			setItem('auth', auth, expiresInDays);
			update((prev) => {
				return [auth, ...prev];
			});
		},

		getRefreshToken() {
			/** @type {(AuthJWT)} */
			const auth = getItem('auth');
			if (!auth) return null;
			return auth.refreshToken;
		},
		getUserId() {
			/** @type {(AuthJWT)} */
			const auth = getItem('auth');
			if (!auth) return null;
			return auth.userId;
		},
		reloadTokens({ accessToken, refreshToken, expireAt }) {
			/** @type {(AuthJWT)} */
			const authOld = getItem('auth');
			if (!authOld) return null;
			const userId = authOld.userId;
			const auth = AuthJWT.fromJson({
				userId,
				accessToken,
				refreshToken,
				expireAt
			});
			const expiresInDays = auth.expireAt.days();
			removeItem('auth');
			setItem('auth', auth, expiresInDays);
			update((prev) => {
				return [auth, ...prev];
			});
		},
		getAccessToken() {
			/** @type {(AuthJWT)} */
			const auth = getItem('auth');
			if (!auth) return null;
			return auth.accessToken;
		},
		isLoggedIn() {
			return hasItem('auth');
		},

		isRenewToken() {
			/** @type {(AuthJWT)} */
			const auth = getItem('auth');
			if (!auth) return false;
			const now = moment();
			const expireAt = moment(auth.expireAt);
			const diff = expireAt.diff(now, 'minutes');
			return diff < 3;
		},

		logout(note) {
			removeItem('auth');
			update((prev) => {
				return prev.filter(item => item !== note);
			});
		}
	};

}