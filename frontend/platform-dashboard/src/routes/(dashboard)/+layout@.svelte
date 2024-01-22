<script>
	import { Footer, FooterBrand, FooterCopyright, FooterLink, FooterLinkGroup } from 'flowbite-svelte';
	import { setAccessToken } from '@utility/apiCaller.js';
	import { createApolloClient } from '@utility/gqlClient.js';
	import { _ } from 'svelte-i18n';
	import ApplicationBar from '@components/common/ApplicationBar.svelte';
	import SideBar from '@components/common/sidebar/SideBar.svelte';
	import { createAuth } from '@stores/authStore.js';
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import { requestRefreshToken } from '@api/auth.js';
	import { getClient } from 'svelte-apollo';
	import { userStore } from '@stores/userStore.js';
	import { applicationStore } from '@stores/applicationStore.js';
	import { GetInitialUserData } from '@api/queries/user.query.js';
	import { GetMaintenanceMode } from '@api/queries/general.query.js';
	import { isEmpty } from 'lodash-es';
	import { PUBLIC_MAINTENANCE_URL } from '$env/static/public';

	$: auth = createAuth();
	$: loggedUser = auth.isLoggedIn();
	$: auth.isLoggedIn() ?createApolloClient(auth.getAccessToken()): null;
	$: client = auth.isLoggedIn() ? getClient() : null;
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

		try {
			const result = await fetch(PUBLIC_MAINTENANCE_URL, {
				method: 'GET'
			});
			if (result.ok) {
				const data = await result.json();
				if (data.Maintenance) {
					console.log('maintenance mode');
					await goto('/maintenance');
				}

			}
		} catch (e) {
			await logout();
			return;
		}
		if (!auth.isLoggedIn()) {
			await goto('/login');
		}
		if (auth.isLoggedIn()) {
			const accessToken = auth.getAccessToken();
			if (!accessToken) {
				await logout();
				return;
			}
			setAccessToken(accessToken);
			try {
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
						await logout();
					}
				}
			} catch (e) {
				console.log(e);
				auth.logout();
				await goto('/login');
			}
			// we rechecking also for verify if user is logged in this case required for logged in user
			await fetchSystemMaintenanceMode();
			if ($applicationStore.maintenanceMode) {
				await goto('/maintenance');
				return;
			}
			await setupInitialUserData();
		}
	});

	async function fetchSystemMaintenanceMode() {
		applicationStore.setLoading(true);
		try {
			const res = await client?.query({
				query: GetMaintenanceMode
			});
			applicationStore.setMaintenanceMode(res?.data?.general?.maintenanceMode || true);

		} catch (e) {
			console.log(e);
			if (e.message.includes('Network error')) {
				applicationStore.setError('Network error');
				applicationStore.setMaintenanceMode(true);
			}
			if (e.message.includes('Access Denied to field')) {
				console.log('Login invalid');

				auth.logout();
				await goto('/login');
			}
		}
		applicationStore.setLoading(false);
	}

	async function setupInitialUserData() {
		applicationStore.setLoading(true);
		try {
			if (isEmpty($userStore?.profile) && isEmpty($userStore?.userViews) && isEmpty($applicationStore?.countries)) {
				await fetchInitialUserData();
				
			}
		} catch (e) {
			applicationStore.setLoading(false);
			return;
		}
		applicationStore.setLoading(false);
	}

	async function fetchInitialUserData() {
		try {
			const res = await client?.query({
				query: GetInitialUserData
			});
			if (res?.data?.user?.getMyProfile && res?.data?.general?.getViews) {
				console.log('res', res);
				userStore.updateUserProfile(res.data?.user?.getMyProfile);
				userStore.updateViews(res.data?.general?.getViews);

				console.log('userStore', userStore);
				console.log('profile', $userStore.profile);
				console.log('userViews', $userStore.userViews);
			}

		} catch (e) {
			console.log(e);
			applicationStore.setError(e.message);
		}
	}

	async function logout() {
		auth.logout();
		await goto('/login');
	}
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
	{#if loggedUser}
		
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

							{#if $userStore.loading}
								<div class="flex justify-center items-center h-screen">
									<div class="flex flex-col items-center">
										<div class="loader ease-linear rounded-full border-8 border-t-8 border-gray-200 h-32 w-32"></div>
										<h2 class="text-center text-white text-xl font-semibold">Loading...</h2>
										<p class="w-1/3 text-center text-white">This may take a few seconds, please don't close this
											page.</p>
									</div>
								</div>
							{:else}
								<slot />
							{/if}
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

	{/if}
</div>
	