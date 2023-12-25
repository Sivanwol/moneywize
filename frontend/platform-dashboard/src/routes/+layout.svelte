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
	<main class="flex-1">
		<slot />
	</main>
	<div class="mt-10">x
		<Footer footerType="logo">
			<div class="sm:flex sm:items-center sm:justify-between">
				<FooterBrand alt="Wolberg Pro LTD" href="https://flowbite.com" name="Wolberg Pro LTD" src="img/logo/logo.svg" />
				<FooterLinkGroup ulClass="flex flex-wrap items-center mb-6 text-sm text-gray-500 sm:mb-0 dark:text-gray-400">
					<FooterLink href="/about-us">About</FooterLink>
					<FooterLink href="/policy">Privacy Policy</FooterLink>
					<FooterLink href="/contact">Contact</FooterLink>
				</FooterLinkGroup>
			</div>
			<hr class="my-6 border-gray-200 sm:mx-auto dark:border-gray-700 lg:my-8" />
			<FooterCopyright by="Wolberg Pro LTD™" href="/" />
		</Footer>
	</div>
</div>