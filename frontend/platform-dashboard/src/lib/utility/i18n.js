import { browser } from '$app/environment';
import { derived, get } from 'svelte/store';
import { init, locale, register } from 'svelte-i18n';

const defaultLocale = 'en';

register('en', () => import('@translations/en.json'));

init({
	fallbackLocale: defaultLocale,
	initialLocale: browser ? window.navigator.language : defaultLocale
});

export function formatDate(date, options) {
	return new Intl.DateTimeFormat(getLocaleForFormat(), options).format(new Date(date));
}

export function formatNumber(num) {
	return new Intl.NumberFormat(getLocaleForFormat()).format(num);
}

function getLocaleForFormat() {
	const selectedLocale = get(locale);
	switch (selectedLocale) {
		case 'en':
			return 'en-US';
		default:
			return 'en-US';
	}
}

export const isLocaleLoaded = derived(locale, ($locale) => typeof $locale === 'string');