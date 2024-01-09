import storage from '@stores/store.js';

function createUserStore() {
	const localStorageKey = 'userData';
	const initialValues = {
		profile: {},
		userViews: [],
		userActivities: [],
		loading: false
	};
	const { subscribe, set, update } = storage(localStorageKey, initialValues);

	return {
		subscribe,
		updateUserProfile: (entity) => update((n) => {
			n.profile = { ...n.profile, ...entity };
		}),
		updateViews: (entities) => update((n) => {
			n.userViews = [...n.userViews, ...entities];
		}),
		updateUserActivities: (entities) => update((n) => {
			n.userActivities = [...n.userActivities, ...entities];
		}),
		loading: (isLoading) => update((n) => {
			n.loading = isLoading;
		}),
		clearViews: () => update((n) => {
			n.userViews = initialValues.userViews;
		}),
		clearActivities: () => update((n) => {
			n.userActivities = initialValues.userActivities;
		}),
		logout: () => set(initialValues)
	};
}

export const userStore = createUserStore();