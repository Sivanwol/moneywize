import storage from '@stores/store.js';
import { getClient } from 'svelte-apollo';
import { GetMyProfileQuery } from '@api/queries/user.query.js';

function createUserStore() {
	const localStorageKey = 'userData';
	const initialValues = {
		profile: {},
		userViews: [],
		userActivities: [],
		isEmptyState: true,
		loading: false,
		errorMessage: null,
		systemNotAvailable: false
	};
	const { subscribe, set, update } = storage(localStorageKey, initialValues);
	return {
		subscribe,
		init: async () => {
			try {
				const client = getClient();
				update((state) => {
					state.loading = true;
				});
				const { data } = await client.query({ query: GetMyProfileQuery });
				const { getMyProfile } = data.user;
				update((state) => {
					state.loading = false;
					state.profile = { ...state.profile, ...getMyProfile };
					state.isEmptyState = false;
				});
			}
			catch (error) {
				set({
					isEmptyState: true,
					errorMessage: error.message
				});
			}
			
		},
		updateUserProfile:  async () => {
			const client = getClient();
			update((state) => {
				state.loading = true;
			})
			try {
				const { data } = await client.query({ query: GetMyProfileQuery });
				const { getMyProfile } = data.user;

				update((state) => {
					state.loading = false;
					state.profile = { ...state.profile, ...getMyProfile };
				})
			} catch (error) {
				set({
					isEmptyState: true,
					errorMessage: error.message
				});
			}
		},
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