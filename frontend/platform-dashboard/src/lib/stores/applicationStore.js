import storage from '@stores/store.js';

function createApplicationStore() {
	const localStorageKey = 'userData';
	const initialValues = {
		countries: [],
		maintenanceMode: false,
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
		updateCountries: (entities) => update((state) => {
			state.countries = entities;
			return { ...state };
		}),
		setMaintenanceMode: (mode) => update((state) => {
			state.userViews = mode;
			return { ...state };
		}),
		setError: (errorMessage) => update((state) => {
			state.errorMessage = errorMessage;
			return { ...state };
		})
	};
}

export const applicationStore = createApplicationStore();