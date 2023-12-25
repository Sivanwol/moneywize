<script>
	import '../app.css';
	import { isLocaleLoaded } from '@utility/i18n.js';
	import { Footer, FooterBrand, FooterCopyright, FooterLink, FooterLinkGroup } from 'flowbite-svelte';
	import { browser } from '$app/environment';
	import { _, locale, waitLocale } from 'svelte-i18n';

	export const load = async () => {
		if (browser) {
			locale.set(window.navigator.language);
		}
		await waitLocale();
	};
</script>
<style lang="postcss">
    :host {
        display: block;
    }
</style>
<div>
	{#if isLocaleLoaded}
		<main class="flex-1 overflow-scroll pb-16">
			<slot />
		</main>
		<Footer class="absolute bottom-0 start-0 z-20 w-full" footerType="logo">
			<div class="sm:flex sm:items-center sm:justify-between">
				<FooterBrand alt="{$_('footer.logo')}" href="https://flowbite.com" name="{$_('footer.logo')}"
										 src="img/logo/logo.svg" />
				<FooterLinkGroup ulClass="flex flex-wrap items-center mb-6 text-sm text-gray-500 sm:mb-0 dark:text-gray-400">
					<FooterLink href="/terms">{$_("navigation.terms")}</FooterLink>
					<FooterLink href="/policy">{$_("navigation.privacy")}</FooterLink>
				</FooterLinkGroup>
			</div>
			<hr class="my-6 border-gray-200 dark:border-gray-700 lg:my-8" />
			<FooterCopyright by="{$_('footer.credit')}" href="/platform/main" />
		</Footer>
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