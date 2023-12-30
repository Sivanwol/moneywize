<script>
	import { Footer, FooterBrand, FooterCopyright, FooterLink, FooterLinkGroup } from 'flowbite-svelte';
	import { setAccessToken } from '@utility/apiCaller.js';
	import { _ } from 'svelte-i18n';
	import ApplicationBar from '@components/common/ApplicationBar.svelte';
	import SideBar from '@components/common/sidebar/SideBar.svelte';
	import { createAuth } from '@stores/authStore.js';
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { requestRefreshToken } from '@api/auth.js';

	$: auth = createAuth();
	let sidebarCollapsed = false;
	let userRoles = ['admin'];
	function handleToggleSidebar(event) {
		sidebarCollapsed = !sidebarCollapsed;
		if (sidebarCollapsed) {
			document.querySelectorAll('.navBar .sub-item-wrapper').forEach(function(item) {
				if (item != null) {
					var parent = this.parentNode;
					var siblingUl = parent.querySelector('ul');

					document.querySelectorAll('.navBar .sub-item-wrapper').forEach(function(item) {
						if (item !== parent) {
							item.classList.remove('open');
							item.querySelector('ul').style.display = 'none';
						}
					});
					siblingUl.style.display = parent.classList.contains('open') ? 'block' : 'none';
				}
			});
		}
	}

	onMount(async () => {
		if (!auth.isLoggedIn()) {
			await goto('/login');
		}
		if (auth.isLoggedIn()) {
			setAccessToken(auth.getAccessToken());
			if (auth.isRenewToken()) {
				const res = await requestRefreshToken(auth.getRefreshToken());
				if (res.status === 200) {
					/** @type {{accessToken: string, refreshToken: {tokenString: string, expiresIn: number}}} */
					const { accessToken, refreshToken } = res.data;
					if (accessToken && refreshToken) {
						auth.reloadTokens({
							accessToken,
							refreshToken: refreshToken.tokenString,
							expireAt: refreshToken.expiresIn
						});
						setAccessToken(auth.getAccessToken());
						return;
					}
					auth.logout();
					await goto('/login');
				} else {
					auth.logout();
					await goto('/login');
				}
			}
		}
	});
</script>
<style lang="postcss">
    /* Mobile devices */
    @media only screen and (max-width: 600px) {
        .responsive-grid {
            height: 70vh;
        }
    }

    /* Desktop devices */
    @media only screen and (min-width: 601px) {
        .responsive-grid {
            height: 90vh;
        }
    }
</style>

<div>
	<!-- Header -->
	<aside
		class="asidebar dark:bg-box-dark fixed start-0 top-0 z-[1035] h-screen overflow-hidden bg-dark xl:!w-[280px] xl:[&.collapsed]:!w-[80px] [&.collapsed]:!w-[250px] !transition-all !duration-[0.2s] ease-linear delay-[0s] !w-0 xl:[&.collapsed>.logo-wrapper]:w-[80px] {sidebarCollapsed? 'collapsed': ''}">
		<div class="flex w-[280px] border-e border-[#edf2f9] dark:border-box-dark-up logo-wrapper items-center h-[71px]">
			<a class="block text-center" href="/platform/main">
				<div class="{sidebarCollapsed? 'hidden': ''} logo-full">
					<img alt="Logo" class="ps-[27px]" src="/img/logo/logo.svg" width="100">
				</div>
				<div class="{sidebarCollapsed? '': 'hidden'} logo-fold">
					<img alt="Logo" class="p-[27px] max-w-[80px]" src="/img/logo/logo.svg" width="80">
				</div>
			</a>
		</div>
		<SideBar sidebarCollapsed={sidebarCollapsed} userRoles={userRoles} />
	</aside>

	<div
		class="relative flex flex-col flex-1 xl:ps-[280px] xl:[&.expanded]:ps-[80px] !transition-all !duration-[0.2s] ease-linear delay-[0s] bg-normalBG dark:bg-main-dark {sidebarCollapsed? 'expanded': ''}">
		<ApplicationBar on:toggleSidebar={handleToggleSidebar} />

		<main class="bg-normalBG dark:bg-main-dark  ">
			<div class=" mx-[30px] -z-[1] min-h-[calc(100vh-195px)] mb-[30px] ssm:mt-[30px] mt-[15px]">
				<div class="grid grid-cols-12 gap-5">
					<div class="col-span-12">
						<div class="p-[25px] bg-dark rounded-10 dark:bg-box-dark ">
							<slot />
						</div>
					</div>
				</div>
			</div>
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
		</main>
	</div>
</div>
	