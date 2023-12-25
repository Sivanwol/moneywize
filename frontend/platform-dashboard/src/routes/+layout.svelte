<script>
	import '../app.css';
	import { Footer, FooterBrand, FooterCopyright, FooterLink, FooterLinkGroup } from 'flowbite-svelte';
</script>
<style lang="postcss">
    :host {
        display: block;
    }
</style>
<div>

	<main class="flex-1 overflow-scroll pb-16">
		<slot />
	</main>
	<Footer class="absolute bottom-0 start-0 z-20 w-full" footerType="logo">
		<div class="sm:flex sm:items-center sm:justify-between">
			<FooterBrand alt="Wolberg Pro LTD" href="https://flowbite.com" name="Wolberg Pro LTD" src="img/logo/logo.svg" />
			<FooterLinkGroup ulClass="flex flex-wrap items-center mb-6 text-sm text-gray-500 sm:mb-0 dark:text-gray-400">
				<FooterLink href="/terms">Terms and conditions</FooterLink>
				<FooterLink href="/policy">Privacy Policy</FooterLink>
			</FooterLinkGroup>
		</div>
		<hr class="my-6 border-gray-200 dark:border-gray-700 lg:my-8" />
		<FooterCopyright by="Wolberg Pro LTD™" href="/platform/main" />
	</Footer>
</div>