import { writable } from 'svelte/store';
import { getItem, hasItem, removeItem, setItem } from '@utility/CookieStorageController';
import { AuthJWT } from '@models/AuthJWT.js';
import moment from 'moment';

/** @type {(AuthJWT)} */
export function createAuth(initialValue = null) {
	const { subscribe, update } = writable(initialValue);

	return {
		subscribe,
		create(userId, accessToken, tokenString, expireAt) {
			const auth = AuthJWT.fromJson({
				userId,
				accessToken,
				tokenString,
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
		getToken() {
			/** @type {(AuthJWT)} */
			const auth = getItem('auth');
			if (!auth) return null;
			return auth.accessToken;
		},
		hasToken() {
			return hasItem('auth');
		},

		isRenewToken() {
			/** @type {(AuthJWT)} */
			const auth = getItem('auth');
			if (!auth) return false;
			const now = moment();
			const expireAt = moment(auth.expireAt);
			const diff = expireAt.diff(now, 'minutes');
			return diff < 30;
		},

		remove(note) {
			removeItem('auth');
			update((prev) => {
				return prev.filter(item => item !== note);
			});
		}
	};

}