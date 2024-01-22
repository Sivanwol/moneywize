<script>
	import { UilAlignCenter } from 'svelte-unicons';
	import { createEventDispatcher } from 'svelte';
	import UserMessagesHeader from '@components/user/UserMessagesHeader.svelte';
	import UserNotificationsHeader from '@components/user/UserNotificationsHeader.svelte';
	import SiteSectionSelectionHeader from '@components/user/SiteSectionSelectionHeader.svelte';
	import UserSelectionLanguageHeader from '@components/user/UserSelectionLanguageHeader.svelte';
	import UserMenuHeader from '@components/user/UserMenuHeader.svelte';
	import { userStore } from '@stores/userStore.js';
	import { Language } from '@models/Language.js';
	import { UserProfile } from '@models/UserProfile.js';
	import { getClient } from 'svelte-apollo';
	import { isEmpty } from 'lodash-es';

	const dispatch = createEventDispatcher();
	$: client = getClient();

	const languages = [
		Language.fromJson({ id: 1, name: 'English', code: 'en', flag: '/img/flags/english.png' }),
		Language.fromJson({ id: 2, name: 'Spanish', code: 'es', flag: '/img/flags/spanish.png' })
	];
	let selectedLanguage = languages[0];
	$: userProfile = UserProfile.fromJson($userStore.profile);

	let showMessageWindow = false;
	let showNotificationWindow = false;
	let showSiteSectionWindow = false;
	let showLanguageWindow = false;
	let showUserSettingsWindow = false;

	function handleToggleSidebar() {
		resetWindowStates();
		dispatch('toggleSidebar', {});
	}

	function resetWindowStates() {
		showMessageWindow = false;
		showNotificationWindow = false;
		showSiteSectionWindow = false;
		showLanguageWindow = false;
		showUserSettingsWindow = false;
	}

	function handleSiteSectionWindowEvent(event) {
		const { status } = event.detail;
		resetWindowStates();
		showSiteSectionWindow = status;
	}
	
	function handleMessagesWindowEvent(event) {
		const { status } = event.detail;
		resetWindowStates();
		showMessageWindow = status;
	}

	function handleNotificationWindowEvent(event) {
		const { status } = event.detail;
		resetWindowStates();
		showNotificationWindow = status;
	}

	function handleSelectionLanguageWindow(event) {
		const { status } = event.detail;
		resetWindowStates();
		showLanguageWindow = status;
	}

	function handleUserSettingsWindow(event) {
		const { status } = event.detail;
		resetWindowStates();
		showUserSettingsWindow = status;
	}

	function onLanguageSelected(event) {
		const { selected } = event.detail;
		resetWindowStates();
		const lang = languages.find(l => l.id === selected);
		if (lang) {
			selectedLanguage = lang;
			console.log(`Language selected: ${selectedLanguage.name}`);
		}
	}
	
	function handleMouseLeave() {
		resetWindowStates();
	}
</script>
<style>
    .header-content {
        background-color: #121212;
        z-index: 999999;
    }
</style>
<header
	class="sticky header-content top-0 flex w-full bg-dark z-[10] drop-shadow-1 dark:bg-box-dark dark:drop-shadow-none min-h-[70px]"
	on:mouseleave={handleMouseLeave} role="presentation">
	<!-- Navigation -->
	<div class="flex flex-1 nav-wrap md:ps-[20px] ps-[30px] pe-[30px]">
		<!-- Header left menu -->

		<ul class="flex items-center mb-0 list-none nav-left ps-0 gap-x-[14px] gap-y-[9px]">
			<!-- Navigation Items -->
			<li>
				<button
					aria-label="Toggle SideBar"
					class="flex items-center justify-center min-w-[40px] w-[40px] h-[40px] rounded-full bg-transparent hover:bg-primary/10 hover:text-primary group transition duration-200 ease-in-out text-[#525768] dark:text-subtitle-dark max-md:dark:hover:bg-box-dark-up text-[20px] cursor-pointer xl:[&.hide]:hidden max-md:bg-normalBG max-md:dark:bg-box-dark-up"
					on:click={handleToggleSidebar}>
					<UilAlignCenter />
				</button>
			</li>
		</ul>
		<!-- Header right menu -->

		<div class="flex items-center ms-auto py-[15px] sm:gap-x-[25px] max-sm:gap-x-[15px] gap-y-[15px] relative">
			<div class="relative hidden">
				<button
					class="transition-all theme-dropdown-trigger text-[20px] text-light dark:text-subtitle-dark [&.close>.uil-search]:hidden [&.close>.uil-multiply]:block"
					type="button">
					<i class="uil uil-search "></i>
					<i class="hidden uil uil-multiply "></i>
				</button>
				<input
					class="theme-dropdown absolute end-0 transition-[opacity,margin] duration [&.visible]:opacity-100 [&.visible]:block opacity-0 hidden min-w-[15rem] mt-2 dark:bg-box-dark-down p-1.5 h-[48px] px-[20px] dark:shadow-none border-1 border-regular dark:border-box-dark-up rounded-6 capitalize bg-dark text-body dark:text-title-dark placeholder:text-body placeholder:text-subtitle-dark text-ellipsis outline-none"
					id="searchInput" name="search" placeholder="search here"
					type="text">
			</div>

			<button class="flex xl:hidden items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark min-h-[40px]"
							id="author-dropdown"
							type="button">
				<i class="uil uil-ellipsis-v text-[18px]"></i>
			</button>
			<ul
				class="xl:flex items-center justify-end flex-auto mb-0 list-none ps-0 sm:gap-x-[25px] max-sm:gap-x-[15px] gap-y-[15px] max-xl:absolute max-xl:z-[1000] max-xl:m-0 max-xl:rounded-lg max-xl:border-none max-xl:bg-dark max-xl:bg-clip-padding max-xl:text-left max-xl:shadow-lg max-xl:dark:bg-neutral-700 max-xl:[&.active]:flex max-xl:end-0 max-xl:px-[20px] max-sm:px-[15px] max-xl:py-[10px] max-xl:top-[70px]"
				id="right-ellipsis-trigger">
				<li>
					<UserMessagesHeader messages={[]} on:statusMessageWindow={handleMessagesWindowEvent} openWindow={showMessageWindow}
															unreadMessages={0} />
				</li>
				<li>
					<UserNotificationsHeader messages={[]} on:statusNotificationWindow={handleNotificationWindowEvent}
																	 openWindow={showNotificationWindow}
																	 unreadMessages={0} />
				</li>
				<li>
					<SiteSectionSelectionHeader on:statusSiteSelectionWindow={handleSiteSectionWindowEvent}
																			openWindow={showSiteSectionWindow} />
				</li>
				<li>
					<UserSelectionLanguageHeader languages={languages} on:selectedLanguage={onLanguageSelected}
																			 on:statusLanguageWindow={handleSelectionLanguageWindow}
																			 openWindow={showLanguageWindow} selectLanguage={selectedLanguage} />
				</li>
				<li>
					{#if !isEmpty($userStore.profile)}
						<UserMenuHeader on:statusUserSettingsWindow={handleUserSettingsWindow} openWindow={showUserSettingsWindow}
														userProfile={userProfile} />
					{/if}
				</li>
			</ul>
		</div>

	</div>
	<!-- End: Navigation -->
</header>