<script>
	import Notifications from '@components/common/Menus/Notifications.svelte';
	import { UserMessage } from '@models/UserMessage.js';
	import { createEventDispatcher, onMount } from 'svelte';

	const dispatch = createEventDispatcher();

	export let openWindow = false;
	export let unreadMessages = 0;
	/** @type { UserMessage[] } */
	export let messages = [];

	function handleToggleMessageWindow() {
		openWindow = !openWindow;
		dispatch('statusMessageWindow', {
			status: openWindow
		});
	}

	onMount(() => {
		dispatch('statusMessageWindow', {
			status: openWindow
		});
	});
</script>
<div class="relative" data-te-dropdown-ref>
	<button aria-expanded="{openWindow}"
					class="flex items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark relative min-h-[40px] group"
					data-te-dropdown-show={openWindow || null}
					data-te-dropdown-toggle-ref id="message"
					on:click={handleToggleMessageWindow}
					on:keydown={(evt) => evt.key === 'Enter' && handleToggleMessageWindow()}
					type="button">
		<i class="uil uil-envelope"></i>
		{#if unreadMessages > 0 && messages.length > 0}
			<span
				class="absolute flex w-1.5 h-1.5 translate-x-2/4 -translate-y-2/4 origin-[100%_0%] end-[3px] top-[8px] group-[[data-te-dropdown-show]]:hidden">
				<span class="absolute inline-flex w-full h-full rounded-full opacity-75 animate-ping bg-success/20"></span>
				<span class="relative inline-flex w-1.5 h-1.5 rounded-full bg-success"></span>
			</span>
		{/if}
	</button>
	<Notifications labelBy="message" openNotification="{openWindow}">
		<h1 class="flex items-center justify-center text-sm rounded-md bg-section dark:bg-box-dark-up h-[50px] p-[10px] text-dark dark:text-title-dark font-semibold"
				slot="header">
                                 <span class="title-text">
                                    Messages
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
						<li class="flex items-center justify-between p-[10px] border-b border-[#eaeaea] dark:border-box-dark-down">
							<div class="flex items-center">
								<img class="w-[40px] h-[40px] rounded-full" src={message.avatarImage} alt="avatar" />
								<div class="flex flex-col ml-[10px]">
									<span class="text-sm font-semibold text-dark dark:text-title-dark">{message.byUsername}</span>
									<span class="text-[13px] text-[#a0a0a0] dark:text-subtitle-dark">{message.message}</span>
								</div>
							</div>
							<span class="text-[13px] text-[#a0a0a0] dark:text-subtitle-dark">{message.duration}</span>
						</li>
					{/each}
				{:else}
					<li class="flex items-center justify-center p-[10px] border-b border-[#eaeaea] dark:border-box-dark-down">
						<span class="text-[13px] text-[#a0a0a0] dark:text-subtitle-dark">No messages</span>
					</li>
				{/if}
			</ul>
			<a
				class="flex items-center justify-center text-[13px] font-medium bg-normalBG dark:bg-box-dark-up h-[50px] text-light hover:text-primary dark:hover:text-subtitle-dark dark:text-title-dark mx-[-15px] mb-[-15px] rounded-b-6"
				href="#">
				See all messages
			</a>
		</div>
	</Notifications>
</div>