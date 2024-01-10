<script>
	import '../app.css';
	import { onMount } from 'svelte';
	import { isLocaleLoaded } from '@utility/i18n.js';
	import { browser } from '$app/environment';
	import { locale, waitLocale } from 'svelte-i18n';

	let rotateDevice = false;
	let isMobile = false;
	export const load = async () => {
		if (browser) {
			locale.set(window.navigator.language);
		}
		await waitLocale();
	};
	$: load();
	onMount(async () => {
		window.addEventListener('orientationchange', checkOrientation);
	});

	function checkOrientation() {
		isMobile = false;
		rotateDevice = false;
		if (/Mobi|Android/i.test(navigator.userAgent)) {
			isMobile = true;
			if (window.orientation == 90 || window.orientation == -90) {
				rotateDevice = true;
			} else {
				rotateDevice = false;
			}
		}
	}
</script>
<style lang="postcss">
    :host {
        display: block;
    }
</style>
<div>
	{#if isLocaleLoaded}
		{#if isMobile && rotateDevice}
			<div class="flex items-center justify-center h-screen">
				<div class="flex flex-col items-center justify-center">
					<div class="flex items-center justify-center w-16 h-16 mb-4">
						<div class="w-2 h-2 bg-blue-600 rounded-full animate-bounce"></div>
						<div class="w-2 h-2 ml-1 bg-blue-600 rounded-full animate-bounce"></div>
						<div class="w-2 h-2 ml-1 bg-blue-600 rounded-full animate-bounce"></div>
					</div>
					<div class="text-2xl font-semibold text-gray-700 dark:text-gray-200">Please rotate your device</div>
				</div>
			</div>
		{:else}
			<div class="relative max-h-fit scroll-auto ">
				<main>
					<section>
						<slot />
					</section>
				</main>
			</div>
		{/if}
	{:else}
		<div class="flex items-center justify-center h-screen">
			<div class="flex flex-col items-center justify-center">
				<div class="flex items-center justify-center w-16 h-16 mb-4">
					<div class="w-2 h-2 bg-blue-600 rounded-full animate-bounce"></div>
					<div class="w-2 h-2 ml-1 bg-blue-600 rounded-full animate-bounce"></div>
					<div class="w-2 h-2 ml-1 bg-blue-600 rounded-full animate-bounce"></div>
				</div>
				<div class="text-2xl font-semibold text-gray-700 dark:text-gray-200">Loading...</div>
			</div>
		</div>
	{/if}
</div>