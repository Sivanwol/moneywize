<script>
	import Notifications from '@components/common/Menus/Notifications.svelte';
	import { createEventDispatcher, onMount } from 'svelte';

	const dispatch = createEventDispatcher();

	export let openWindow = false;

	function handleToggleMessageWindow() {
		openWindow = !openWindow;
		dispatch('statusSiteSelectionWindow', {
			status: openWindow
		});
	}

	onMount(() => {
		dispatch('statusSiteSelectionWindow', {
			status: openWindow
		});
	});
</script>
<div class="relative" data-te-dropdown-ref>
	<button aria-expanded="{openWindow}"
					class="flex items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark relative min-h-[40px] group"
					data-te-dropdown-show={openWindow || null}
					data-te-dropdown-toggle-ref id="settings"
					on:click={handleToggleMessageWindow}
					on:keydown={(evt) => evt.key === 'Enter' && handleToggleMessageWindow()}
					type="button">
		<i class="uil uil-setting"></i>
	</button>
	<Notifications labelBy="settings" openNotification="{openWindow}">
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