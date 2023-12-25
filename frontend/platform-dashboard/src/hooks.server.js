import { locale } from 'svelte-i18n';

export const handle = async ({ event, resolve }) => {
	const lang = event.request.headers.get('accept-language')?.split(',')[0];
	if (lang) {
		locale.set(lang);
	}
	return resolve(event);
};

export const handleError = ({ error }) => {
	const message = 'Error caught in [server-hooks]: ' + (error)?.message;
	console.error(message);
	return { message };
};