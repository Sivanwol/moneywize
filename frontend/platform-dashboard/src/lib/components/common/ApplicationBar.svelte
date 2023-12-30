<script>
	import { UilAlignCenter } from 'svelte-unicons';

	import { createEventDispatcher } from 'svelte';
	import Notifications from '@components/common/Menus/Notifications.svelte';
	import UserMessagesHeader from '@components/User/UserMessagesHeader.svelte';
	import UserNotificationsHeader from '@components/User/UserNotificationsHeader.svelte';

	const dispatch = createEventDispatcher();


	function handleToggleSidebar() {
		resetWindowStates();
		dispatch('toggleSidebar', {});
	}

	let showMessageWindow = false;
	let showNotificationWindow = false;
	let showSectionWindow = false;
	let showLanguageWindow = false;
	let showUserSettingsWindow = false;


	async function handleToggleSectionWindow() {
		let currentState = !showSectionWindow;
		resetWindowStates();
		showSectionWindow = currentState;
	}

	async function handleToggleLanguageWindow() {
		let currentState = !showLanguageWindow;
		resetWindowStates();
		showLanguageWindow = currentState;
	}

	async function handleToggleUserSettingsWindow() {
		let currentState = !showUserSettingsWindow;
		resetWindowStates();
		showUserSettingsWindow = currentState;
	}

	function resetWindowStates() {
		showMessageWindow = false;
		showNotificationWindow = false;
		showSectionWindow = false;
		showLanguageWindow = false;
		showUserSettingsWindow = false;
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

					<div class="relative" data-te-dropdown-ref>
						<button aria-expanded="{showSectionWindow}"
										class="flex items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark relative min-h-[40px] group"
										data-te-dropdown-show={showSectionWindow || null}
										data-te-dropdown-toggle-ref id="settings"
										on:click={handleToggleSectionWindow}
										on:keydown={(evt) => evt.key === 'Enter' && handleToggleSectionWindow()}
										type="button">
							<i class="uil uil-setting"></i>
						</button>
						<Notifications labelBy="settings" openNotification="{showSectionWindow}">
							<ul
								class="flex flex-wrap items-center lg:[&>li]:flex-[50%] max-lg:[&>li]:flex-[100%] max-h-[251px] relative overflow-x-hidden overflow-y-auto scrollbar scrollbar">

								<li class="w-full">
									<figure
										class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
										<img alt="audio" class="h-fit me-4" src="/img/settings/audio.png">
										<figcaption>
											<h1
												class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
												Diagram Maker</h1>
											<p class="mb-0 text-body dark:text-subtitle-dark text-start">Plan user flows & test
												scenarios </p>
										</figcaption>
									</figure>
								</li>
								<li class="w-full">
									<figure
										class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
										<img alt="payment" class="h-fit me-4" src="/img/settings/payment.png">
										<figcaption>
											<h1
												class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
												payments</h1>
											<p class="mb-0 text-body dark:text-subtitle-dark text-start">We handle billions of
												dollars </p>
										</figcaption>
									</figure>
								</li>
								<li class="w-full">
									<figure
										class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
										<img alt="feature" class="h-fit me-4" src="/img/settings/feature.png">
										<figcaption>
											<h1
												class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
												All Features</h1>
											<p class="mb-0 text-body dark:text-subtitle-dark text-start">Introducing Increment
												subscriptions </p>
										</figcaption>
									</figure>
								</li>
								<li class="w-full">
									<figure
										class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
										<img alt="theme" class="h-fit me-4" src="/img/settings/theme.png">
										<figcaption>
											<h1
												class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
												Themes</h1>
											<p class="mb-0 text-body dark:text-subtitle-dark text-start">Third party themes that are
												compatible
											</p>
										</figcaption>
									</figure>
								</li>
								<li class="w-full">
									<figure
										class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
										<img alt="payment" class="h-fit me-4" src="/img/settings/payment.png">
										<figcaption>
											<h1
												class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
												payments</h1>
											<p class="mb-0 text-body dark:text-subtitle-dark text-start">We handle billions of
												dollars </p>
										</figcaption>
									</figure>
								</li>
								<li class="w-full">
									<figure
										class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
										<img alt="design" class="h-fit me-4" src="/img/settings/design.png">
										<figcaption>
											<h1
												class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
												Design Mockups</h1>
											<p class="mb-0 text-body dark:text-subtitle-dark text-start">Share planning visuals with
												clients </p>
										</figcaption>
									</figure>
								</li>
								<li class="w-full">
									<figure
										class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
										<img alt="file" class="h-fit me-4" src="/img/settings/file.png">
										<figcaption>
											<h1
												class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
												Content Planner</h1>
											<p class="mb-0 text-body dark:text-subtitle-dark text-start">Centralize content gathering
												and editing
											</p>
										</figcaption>
									</figure>
								</li>
								<li class="w-full">
									<figure
										class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
										<img alt="audio" class="h-fit me-4" src="/img/settings/audio.png">
										<figcaption>
											<h1
												class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
												Diagram Maker</h1>
											<p class="mb-0 text-body dark:text-subtitle-dark text-start">Plan user flows & test
												scenarios </p>
										</figcaption>
									</figure>
								</li>
							</ul>
						</Notifications>
					</div>
				</li>
				<li>

					<div class="relative" data-te-dropdown-ref>
						<button aria-expanded="{showLanguageWindow}"
										class="flex items-center"
										data-te-dropdown-show={showLanguageWindow || null}
										data-te-dropdown-toggle-ref id="flags"
										on:click={handleToggleLanguageWindow}
										on:keydown={(evt) => evt.key === 'Enter' && handleToggleLanguageWindow()}
										type="button">
							<img alt="flags" class="min-w-[20px] min-h-[20px]" src="/img/flags/english.png" />
						</button>
						<Notifications continerLimit=""
													 labelBy="flags"
													 openNotification="{showLanguageWindow}"
													 overrideClasses="absolute z-[1000] ltr:float-left rtl:float-right m-0 min-w-max list-none overflow-hidden rounded-lg border-none bg-dark bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark dark:bg-box-dark-down  [&[data-te-dropdown-show]]:block">
							<ul
								class="block bg-dark dark:bg-box-dark-down w-[100px] shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4 overflow-hidden">
								<li class="bg-dark dark:bg-box-dark-down hover:bg-primary/10 dark:hover:bg-primary/10">
									<button class="flex items-center px-3 py-1.5 text-sm text-dark dark:text-subtitle-dark">
										<img alt="english" class="w-3.5 h-3.5 me-2" src="/img/flags/english.png">
										<span>English</span>
									</button>
								</li>
								<li class="bg-dark dark:bg-box-dark-down hover:bg-primary/10 dark:hover:bg-primary/10">
									<button class="flex items-center px-3 py-1.5 text-sm text-dark dark:text-subtitle-dark">
										<img alt="spanish" class="w-3.5 h-3.5 me-2" src="/img/flags/spanish.png">
										<span>Spanish</span>
									</button>
								</li>
							</ul>
						</Notifications>
					</div>

				</li>
				<li>
					<div class="relative" data-te-dropdown-ref>
						<button aria-expanded="{showUserSettingsWindow}"
										class="flex items-center"
										data-te-dropdown-show={showUserSettingsWindow || null}
										data-te-dropdown-toggle-ref id="user-settings"
										on:click={handleToggleUserSettingsWindow}
										on:keydown={(evt) => evt.key === 'Enter' && handleToggleUserSettingsWindow()}
										type="button">
							<span class="sr-only">Open user menu</span>
							<img alt="user photo" class="min-w-[32px] w-8 h-8 rounded-full xl:me-2"
									 src="/img/avatars/thumbs.png" />
							<span class="hidden xl:block">Sivan Wolberg</span>
							<i class="uil uil-angle-down text-light dark:text-subtitle-dark text-[18px] hidden xl:block"></i>
						</button>

						<!-- Dropdown menu -->
						<Notifications continerLimit="min-w-[310px] max-sm:min-w-full"
													 labelBy="user-settings"
													 openNotification="{showUserSettingsWindow}">
							<div
								class=" pt-4 px-[15px] py-[12px] bg-dark dark:bg-box-dark shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4">
								<figure
									class="flex items-center text-sm rounded-[8px] bg-section dark:bg-box-dark-up py-[20px] px-[25px] mb-[12px] gap-[15px]">
									<img alt="user" class="w-8 h-8 rounded-full bg-regular" src="images/avatars/thumbs.png">
									<figcaption>
										<div class="text-dark dark:text-title-dark mb-0.5 text-sm">Sivan Wolberg</div>
										<div class="mb-0 text-xs text-body dark:text-subtitle-dark">Software Engineer</div>
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
											<button
												class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
												<i class="text-[16px] uil uil-users-alt"></i>
												Activity
											</button>
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
									href="log-in.html">
									<i class="uil uil-sign-out-alt"></i> Sign Out
								</button>
							</div>
						</Notifications>
					</div>
				</li>
			</ul>
		</div>

	</div>
	<!-- End: Navigation -->
</header>