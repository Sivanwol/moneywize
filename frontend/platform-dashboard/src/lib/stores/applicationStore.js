import storage from '@stores/store.js';
import { PUBLIC_CACHE_STORE_MINUTES } from '$env/static/public';
import moment from 'moment';

function createApplicationStore() {
	const localStorageKey = 'applicationData';
	const initialValues = {
		countries: [],
		maintenanceMode: false,
		loading: false,
		errorMessage: null,
		systemNotAvailable: false,
		expireAt: null
	};
	const { subscribe, set, update } = storage(localStorageKey, initialValues);
	return {
		subscribe,
		setLoading: (loading) => update((state) => {
			state.loading = loading;
			state.expireAt = moment().add(PUBLIC_CACHE_STORE_MINUTES, 'minutes').toDate();
			return { ...state, expireAt: moment().add(PUBLIC_CACHE_STORE_MINUTES, 'minutes').toDate() };
		}),
		updateCountries: (entities) => update((state) => {
			state.countries = entities;
			state.expireAt = moment().add(PUBLIC_CACHE_STORE_MINUTES, 'minutes').toDate();
			return { ...state, expireAt: moment().add(PUBLIC_CACHE_STORE_MINUTES, 'minutes').toDate() };
		}),
		setMaintenanceMode: (mode) => update((state) => {
			state.userViews = mode;
			state.expireAt = moment().add(PUBLIC_CACHE_STORE_MINUTES, 'minutes').toDate();
			return { ...state };
		}),
		setError: (errorMessage) => update((state) => {
			state.errorMessage = errorMessage;
			return { ...state };
		})
	};
}

export const applicationStore = createApplicationStore();