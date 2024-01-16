import moment from 'moment';
import { writable } from 'svelte/store';

function createUserStore() {
	const initialValues = {
		profile: {},
		userViews: [],
		userActivities: [],
		errorMessage: null,
		expireAt: null
	};
	const { subscribe, set, update } = writable(initialValues);
	return {
		subscribe,
		updateUserProfile: (profile) => update((state) => ({
			...state,
			profile: { ...state.profile, ...profile },
			expireAt: moment().add(1, 'minutes').toDate()
		})),
		updateViews: (entities) => update((state) => ({
			...state,
			userViews: entities,
			expireAt: moment().add(1, 'minutes').toDate()
		})),
		updateUserActivities: (entities) => update((state) => ({
			...state,
			userActivities: entities,
			expireAt: moment().add(1, 'minutes').toDate()
		})),
		setError: (errorMessage) => update((state) => ({ ...state, errorMessage, expireAt: null }))
	};
}

export const userStore = createUserStore();