<script>
	import Notifications from '@components/common/Menus/Notifications.svelte';
	import { createEventDispatcher, onMount } from 'svelte';
	import { UserProfile } from '@models/UserProfile';
	import { createAuth } from '@stores/authStore.js';
	import { goto } from '$app/navigation';

	const dispatch = createEventDispatcher();

	export let openWindow = false;

	$: auth = createAuth();
	/** @type { UserProfile } */
	export let userProfile;

	function handleToggleMessageWindow() {
		openWindow = !openWindow;
		dispatch('statusUserSettingsWindow', {
			status: openWindow
		});
	}

	onMount(() => {
		dispatch('statusUserSettingsWindow', {
			status: openWindow
		});
	});

	function handleLogout() {
		auth.logout();
		goto('/login');
	}

</script>
<div class="relative" data-te-dropdown-ref>
	<button aria-expanded="{openWindow}"
					class="flex items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark relative min-h-[40px] group"
					data-te-dropdown-show={openWindow || null}
					data-te-dropdown-toggle-ref id="user-settings"
					on:click={handleToggleMessageWindow}
					on:keydown={(evt) => evt.key === 'Enter' && handleToggleMessageWindow()}
					type="button">
		<span class="sr-only">Open user menu</span>
		<img alt="user photo" class="min-w-[32px] w-8 h-8 rounded-full xl:me-2"
				 src="{userProfile.avatarImage}" />
		<span class="hidden xl:block">{userProfile.firstName}, {userProfile.lastName} </span>
		<i class="uil uil-angle-down text-light dark:text-subtitle-dark text-[18px] hidden xl:block"></i>
	</button>
	<Notifications
		continerLimit=""
		labelBy="user-settings"
		openNotification="{openWindow}"
		overrideClasses="absolute z-[1000] ltr:float-left rtl:float-right m-0 min-w-[250px] list-none overflow-hidden rounded-lg border-none bg-dark bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark dark:bg-box-dark-down  [&[data-te-dropdown-show]]:block">
		<div
			class=" pt-4 px-[15px] py-[12px] bg-dark dark:bg-box-dark shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4">
			<figure
				class="flex items-center text-sm rounded-[8px] bg-section dark:bg-box-dark-up py-[20px] px-[25px] mb-[12px] gap-[15px]">
				<img alt="user" class="w-8 h-8 rounded-full bg-regular" src="{userProfile.avatarImage}">
				<figcaption>
					<div class="text-dark dark:text-title-dark mb-0.5 text-sm">{userProfile.firstName}
						, {userProfile.lastName}</div>
					<div class="mb-0 text-xs text-body dark:text-subtitle-dark">{userProfile.role}</div>
				</figcaption>
			</figure>
			<ul class="m-0 pb-[10px] overflow-x-hidden overflow-y-auto scrollbar bg-transparent max-h-[230px]">

				<li class="w-full">
					<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
						<button
							class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
							<i class="text-[16px] uil uil-user"></i>
							Profile
						</button>
					</div>
				</li>
				<li class="w-full">
					<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
						<button
							class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
							<i class="text-[16px] uil uil-setting"></i>
							Settings
						</button>
					</div>
				</li>
				<li class="w-full">
					<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
						<a href="/platform/user-activities"
							class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
							<i class="text-[16px] uil uil-users-alt"></i>
							Activity
						</a>
					</div>
				</li>
				<li class="w-full">
					<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
						<button
							class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
							<i class="text-[16px] uil uil-bell"></i>
							Help
						</button>
					</div>
				</li>
			</ul>
			<button
				class="flex items-center justify-center w-full text-sm font-medium bg-normalBG dark:bg-box-dark-up h-[50px] text-light hover:text-primary dark:hover:text-subtitle-dark dark:text-title-dark mb-[-15px] rounded-b-6 gap-[6px]"
				on:click={handleLogout}
				on:keydown={(event) => {
					if (event.key === 'Enter') {
						handleLogout();
					}
				}}>
				<i class="uil uil-sign-out-alt"></i> Sign Out
			</button>
		</div>
	</Notifications>
</div>