import { writable } from 'svelte/store';

function createSystemMessagesStore() {
	const initialValues = {
		messages: []
	};
	const { subscribe, set, update } = writable(initialValues);
	return {
		subscribe,
		addMessage: (message) => update((state) => {
			state.messages.push(message);
			return state;
		}),
		clearMessages: () => update((state) => {
			state.messages = [];
			return state;
		})
	};
}

export const systemMessagesStore = createSystemMessagesStore();