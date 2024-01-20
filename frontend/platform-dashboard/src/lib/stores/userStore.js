import moment from 'moment';
import { PUBLIC_CACHE_STORE_MINUTES } from '$env/static/public';
import storage from '@stores/store.js';

function createUserStore() {
	const localStorageKey = 'userData';
	const initialValues = {
		profile: {},
		userViews: [],
		userActivities: [],
		errorMessage: null,
		expireAt: null
	};
	const { subscribe, set, update } = storage(localStorageKey, initialValues);
	return {
		subscribe,
		updateUserProfile: (profile) => update((state) => ({
			...state,
			profile: { ...state.profile, ...profile },
			expireAt: moment().add(PUBLIC_CACHE_STORE_MINUTES, 'minutes').toDate()
		})),
		updateViews: (entities) => update((state) => ({
			...state,
			userViews: entities,
			expireAt: moment().add(PUBLIC_CACHE_STORE_MINUTES, 'minutes').toDate()
		})),
		updateUserActivities: (entities) => update((state) => ({
			...state,
			userActivities: entities,
			expireAt: moment().add(PUBLIC_CACHE_STORE_MINUTES, 'minutes').toDate()
		})),
		setError: (errorMessage) => update((state) => ({ ...state, errorMessage, expireAt: null }))
	};
}

export const userStore = createUserStore();