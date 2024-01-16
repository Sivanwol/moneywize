<script>
	import { createEventDispatcher } from 'svelte';
	import SideBarSubMenuItem from '@components/common/sidebar/SideBarSubMenuItem.svelte';
	import { allowAccess } from '@utility/permission.js';
	import * as _ from 'lodash';
	import { goto } from '$app/navigation';

	const dispatch = createEventDispatcher();
	/** @type {string} */
	export let key;
	/** @type {string} */
	export let title;
	export let hasIcon = false;
	/** @type {string} */
	export let icon;
	/** @type {string} */
	export let link;
	export let hasSubTitle = false;
	/** @type {string} */
	export let subTitle;
	export let hasSectionTitle = false;
	/** @type {string} */
	export let sectionTitle;
	export let sidebarCollapsed = false;
	/** @type { {key: string, title: string, link: string, permissions: string[]}[] } */
	export let items = [];
	/** @type {string[]} */
	export let userRoles = [];
	/** @type {string[]} */
	export let permissions = [];

	let hasSidebarItems = _.isArray(items) && items.length > 0;

	function handleSideBarSubTitleChange(event) {
		const { id, enabled, title } = event.detail;
		if (key === id) {
			hasSubTitle = enabled;
			subTitle = title;
		}
	}

	let visibleSideBarSubItems = false;

	function handleSideBarSubItemsDropdownToggle(event) {
		visibleSideBarSubItems = !visibleSideBarSubItems;
	}

	async function handleSideBarClick() {
		if (hasSidebarItems && !sidebarCollapsed) {
			return;
		}
		await goto(link);
	}
</script>
{#if allowAccess(permissions, userRoles)}
	<li class="relative sub-item-wrapper" id="{key}">
		{#if hasSectionTitle && !sidebarCollapsed}
			<span class="slug dark:text-white/40 mb-[10px] mt-[30px] block px-6 text-[12px] font-medium uppercase text-light">
				{sectionTitle}
			</span>
		{/if}
		<a tabindex="0" href="javascript:void(0)" on:click={handleSideBarClick} on:keydown={handleSideBarClick} class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex 
						h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition 
						duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary 
						active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark motion-reduce:transition-none dark:hover:bg-box-dark-up 
						dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize" aria-label="{title}">
							<span
								class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current 
								group-focus:text-current">
								{#if hasIcon}
								 <i class="uil uil-apps"></i>
								{/if}
							</span>
			<span class="title {sidebarCollapsed? 'hidden': ''}">{title}</span>
			{#if hasSubTitle}
				<div
					class="{sidebarCollapsed? 'hidden': ''} arrow-down absolute end-0 me-[0.8rem] ms-auto inline-flex items-center whitespace-nowrap rounded-full bg-success px-[0.65em] pb-[0.25em] pt-[0.30em] text-center align-baseline text-[10px] font-bold leading-none text-white group-[&.active]:hidden">
					{subTitle}
				</div>
			{/if}

			{#if hasSidebarItems && !sidebarCollapsed}
				<span id="{key}_dropdown" role="link" tabindex="0" on:click={handleSideBarSubItemsDropdownToggle}
							on:keydown={handleSideBarSubItemsDropdownToggle}
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none group-hover:text-current">
					 <i class="uil uil-angle-down"></i>
				</span>
			{/if}
		</a>
	</li>
	{#if hasSidebarItems && !sidebarCollapsed}
		<SideBarSubMenuItem items={items} visibleSideBarSubItems={visibleSideBarSubItems}
												sidebarCollapsed={sidebarCollapsed} userRoles={userRoles} />
	{/if}
{/if}