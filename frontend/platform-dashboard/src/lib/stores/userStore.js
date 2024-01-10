import storage from '@stores/store.js';

function createUserStore() {
	const localStorageKey = 'userData';
	const initialValues = {
		profile: {},
		userViews: [],
		userActivities: [],
		loading: false,
		errorMessage: null,
		systemNotAvailable: false
	};
	const { subscribe, set, update } = storage(localStorageKey, initialValues);
	return {
		subscribe,
		setLoading: (loading) => update((state) => {
			state.loading = loading;
			return { ...state };
		}),

		updateUserProfile: (profile) => update((state) => {
			state.loading = false;
			state.profile = { ...state.profile, ...profile };
			return { ...state };
		}),
		updateViews: (entities) => update((state) => {
			state.userViews = entities;
			return { ...state };
		}),
		updateUserActivities: (entities) => update((state) => {
			state.userActivities = entities;
			return { ...state };
		}),
		clearActivities: () => update((state) => {
			state.userActivities = initialValues.userActivities;
			return { ...state };
		}),
		setError: (errorMessage) => update((state) => {
			state.errorMessage = errorMessage;
			return { ...state };
		}),
		logout: () => set(initialValues)
	};
}

export const userStore = createUserStore();