import storage from '@stores/store.js';

function createUserStore() {
	const localStorageKey = 'userData';
	const initialValues = {
		profile: {},
		userViews: [],
		userActivities: [],
		isEmptyState: true,
		loading: false
	};
	const { subscribe, set, update, get } = storage(localStorageKey, initialValues);

	return {
		subscribe,
		updateUserProfile: async (entity) => update((state) => {
			state.profile = { ...state.profile, ...entity };
			state.isEmptyState = false;
		}),
		updateViews: (entities) => update((state) => {
			state.userViews = [...state.userViews, ...entities];
			state.isEmptyState = false;
		}),
		updateUserActivities: (entities) => update((state) => {
			state.userActivities = [...state.userActivities, ...entities];
			state.isEmptyState = false;
		}),
		clearViews: () => update((state) => {
			state.userViews = initialValues.userViews;
		}),
		clearActivities: () => update((state) => {
			state.userActivities = initialValues.userActivities;
		}),
		logout: () => set(initialValues)
	};
}

export const userStore = createUserStore();