import { derived, readable, writable } from 'svelte/store';
// import { debounce } from 'lodash'

export const maintenanceTime = readable(new Date(), function start(set) {
	const interval = setInterval(() => {
		set(new Date());
	}, 1000);

	return function stop() {
		clearInterval(interval);
	};
});
let maintenanceStart = new Date();
const resetMaintenanceStart = () => {
	maintenanceStart = new Date();
};
export const requestMaintenanceStatus = writable(false);
export const maintenanceElapsed = derived(
	maintenanceTime,
	($maintenanceTime) => {
		const seconds = Math.round(($maintenanceTime - maintenanceStart) / 1000);
		if (seconds >= 30) {
			console.log('Maintenance checking');
			resetMaintenanceStart();
			requestMaintenanceStatus.set(true);
		}
		return seconds;
	}
);