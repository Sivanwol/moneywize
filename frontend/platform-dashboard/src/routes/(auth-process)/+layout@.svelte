<script>
	import { _ } from 'svelte-i18n';
	import { Footer, FooterBrand, FooterCopyright, FooterLink, FooterLinkGroup } from 'flowbite-svelte';
	import { onMount } from 'svelte';
	import { createAuth } from '@stores/authStore.js';
	import { goto } from '$app/navigation';

	$: auth = createAuth();
	onMount(async () => {
		if (auth.isLoggedIn()) {
			await goto('/platform/main');
		}
	});
</script>
<style lang="postcss">
    .signin-statment {
        background: url('img/signin-bg.png') no-repeat center center/cover;
    }
</style>
<div class="relative">
	<section>
		<div class="overflow-hidden rounded-[0.5rem]  bg-background shadow-md md:shadow-xl">
			<div class="md:hidden">
				<div class="relative h-min-[400px] flex-col bg-muted p-280 text-white">
					<div class="absolute signin-statment inset-0 bg-zinc-900" style="z-index: -1"></div>
					<div class="inset-0 flex items-center justify-center text-white">
						<div class="flex flex-col items-center">
							<img alt="Wolberg Pro LTD" class="w-32 h-32" src="img/logos/logo.svg" />
							<h1 class="text-3xl font-medium">{$_('credit')}</h1>
						</div>
					</div>

					<div class="space-y-4"></div>
					<div>
						<slot />
					</div>
				</div>
			</div>
			<div
				class="container relative hidden h-[700px] flex-col items-center justify-center md:grid lg:max-w-none lg:grid-cols-2 lg:px-0">
				<div class=" relative hidden h-full flex-col bg-muted p-10 text-white lg:flex dark:border-r">
					<div class="signin-statment absolute inset-0 bg-zinc-900" style="z-index: -1"></div>
					<div class="relative z-20 flex items-center text-lg font-medium">
						<img alt="{$_('credit')}" class="w-32 h-32" src="img/logo/logo.svg" />
						{$_('credit')}
					</div>
					<div class="relative z-20 mt-auto">
						<blockquote class="4 mb-10">
							<p class="text-2xl font-medium">
								{$_('login.statement_1')}
							</p>
							<p class="text-lg font-medium">
								{$_('login.statement_1')}
							</p>
							<footer class="text-sm">
								{$_('login.statement_by')}
							</footer>
						</blockquote>
					</div>
				</div>
				<div class="lg:p-8">
					<slot />
				</div>
			</div>
		</div>
	</section>
	<Footer class="dark bg-background px-[25px] dark:bg-box-dark w-full" footerType="logo">
		<div class="sm:flex sm:items-center sm:justify-between">
			<FooterBrand alt="{$_('footer.logo')}" href="https://flowbite.com" name="{$_('footer.logo')}"
									 src="/img/logo/logo.svg" />
			<FooterLinkGroup
				ulClass="flex flex-wrap items-center mb-6 text-sm text-gray-500 sm:mb-0 dark:text-gray-400">
				<FooterLink href="/terms">{$_("navigation.terms")}</FooterLink>
				<FooterLink href="/policy">{$_("navigation.privacy")}</FooterLink>
			</FooterLinkGroup>
		</div>
		<hr class="my-6 border-gray-200 dark:border-gray-700 lg:my-8" />
		<FooterCopyright by="{$_('footer.credit')}" href="/platform/main" />
	</Footer>
</div>