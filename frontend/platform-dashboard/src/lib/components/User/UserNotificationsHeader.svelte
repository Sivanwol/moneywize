<script>
	import Notifications from '@components/common/Menus/Notifications.svelte';
	import { UserNotification } from '@models/UserNotification.js';
	import { createEventDispatcher, onMount } from 'svelte';

	const dispatch = createEventDispatcher();

	export let openWindow = false;
	export let unreadMessages = 0;
	/** @type { UserNotification[] } */
	export let messages = [];

	function handleToggleMessageWindow() {
		openWindow = !openWindow;
		dispatch('statusNotificationsWindow', {
			status: openWindow
		});
	}

	onMount(() => {
		dispatch('statusNotificationsWindow', {
			status: openWindow
		});
	});
</script>
<div class="relative" data-te-dropdown-ref>
	<button aria-expanded="{openWindow}"
					class="flex items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark relative min-h-[40px] group"
					data-te-dropdown-show={openWindow || null}
					data-te-dropdown-toggle-ref id="notification"
					on:click={handleToggleMessageWindow}
					on:keydown={(evt) => evt.key === 'Enter' && handleToggleMessageWindow()}
					type="button">
		<i class="uil uil-bell"></i>
		{#if unreadMessages > 0 && messages.length > 0}
			<span
				class="absolute flex w-1.5 h-1.5 translate-x-2/4 -translate-y-2/4 origin-[100%_0%] end-[3px] top-[8px] group-[[data-te-dropdown-show]]:hidden">
				<span class="absolute inline-flex w-full h-full rounded-full opacity-75 animate-ping bg-success/20"></span>
				<span class="relative inline-flex w-1.5 h-1.5 rounded-full bg-success"></span>
			</span>
		{/if}
	</button>
	<Notifications labelBy="notification" openNotification="{openWindow}">
		<h1 class="flex items-center justify-center text-sm rounded-md bg-section dark:bg-box-dark-up h-[50px] p-[10px] text-dark dark:text-title-dark font-semibold"
				slot="header">
                                 <span class="title-text">
                                    Notifications
																	 {#if unreadMessages > 0 && messages.length > 0}
																			<span
																				class="inline-flex items-center justify-center w-5 h-5 text-xs text-white rounded-full ms-[8px] bg-success dark:text-title-dark">
																				 {unreadMessages}
																			</span>
																	 {/if}
                                 </span>
		</h1>
		<div>
			<ul class="p-0 max-h-[250px] relative overflow-x-hidden overflow-y-auto scrollbar">

				{#if messages.length > 0}
					{#each messages as message}
						<li class="w-full sm:mb-0 mb-[10px]">
							<button
								class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark hover:bg-dark dark:hover:bg-box-dark-up hover:shadow-custom dark:shadow-none dark:rounded-4">
								<div class="flex items-start gap-x-[15px] gap-y-[5px] flex-wrap max-xs:flex-col">
									<div
										class="flex items-center justify-center rounded-full w-[30px] h-[30px] {message.cssType}/10 text-primary">
										<i class="text-current uil {message.icon} text-[16px]"></i>
									</div>
									<div class="flex items-center justify-between flex-1">
										<div class="text-start">
											<h1
												class="flex items-center justify-between mb-0.5 text-[#5a5f7d] dark:text-title-dark text-sm font-normal flex-wrap">
												<span class="text-primary me-1.5 font-medium">{message.byUsername}</span>
												{message.message}
											</h1>
											<p class="mb-0 text-xs text-body dark:text-subtitle-dark">
												{message.duration}
											</p>
										</div>
										<div><span
											class="inline-flex items-center justify-center {message.cssType} w-1.5 h-1.5 ms-3 rounded-full"></span>
										</div>
									</div>
								</div>
							</button>
						</li>
					{/each}
				{:else}
					<li class="flex items-center justify-center p-[10px] border-b border-[#eaeaea] dark:border-box-dark-down">
						<span class="text-[13px] text-[#a0a0a0] dark:text-subtitle-dark">No notifications</span>
					</li>
				{/if}
			</ul>
			<a
				class="flex items-center justify-center text-[13px] font-medium bg-normalBG dark:bg-box-dark-up h-[50px] text-light hover:text-primary dark:hover:text-subtitle-dark dark:text-title-dark mx-[-15px] mb-[-15px] rounded-b-6"
				href="#">
				See all incoming activity
			</a>
		</div>
	</Notifications>
</div>