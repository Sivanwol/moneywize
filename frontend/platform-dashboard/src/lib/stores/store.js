import { get, writable } from 'svelte/store';
import { browser } from '$app/environment';
import moment from 'moment';

const storage = (key, initValue) => {
	const store = writable(initValue);
	if (!browser) return store;

	let storedValueStr = localStorage.getItem(key);
	if (storedValueStr != null) store.set(JSON.parse(storedValueStr));

	store.subscribe((val) => {
		if ([null, undefined].includes(val)) {
			localStorage.removeItem(key);
		} else {
			localStorage.setItem(key, JSON.stringify(val));
		}
	});


	window.addEventListener('storage', () => {
		const storedValueStr = localStorage.getItem(key);
		if (storedValueStr == null) return;

		const localValue = JSON.parse(storedValueStr);
		localValue.expireAt = moment(localValue.expireAt);
		if (localValue.isValid()) {
			if (localValue.expireAt.isBefore(moment())) {
				localStorage.removeItem(key);
				return;
			}
		} else {
			localStorage.removeItem(key);
			return;
		}
		if (localValue !== get(store)) store.set(localValue);
	});

	return store;
}

export default storage;