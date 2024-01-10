import { derived, readable, writable } from 'svelte/store';

export const maintenanceTime = readable(new Date(), function start(set) {
	const interval = setInterval(() => {
		set(new Date());
	}, 1000);

	return function stop() {
		clearInterval(interval);
	};
});
let maintenanceStart = new Date();
export const resetMaintenanceStart = () => {
	maintenanceStart = new Date();
};
export const requestMaintenanceStatus = writable(true);
export const maintenanceElapsed = derived(
	maintenanceTime,
	($maintenanceTime) => Math.round(($maintenanceTime - maintenanceStart) / 1000)
);