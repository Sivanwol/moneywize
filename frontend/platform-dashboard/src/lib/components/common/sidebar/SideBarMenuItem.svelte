<script>
	import { createEventDispatcher } from 'svelte';
	import SideBarSubMenuItem from '@components/common/sidebar/SideBarSubMenuItem.svelte';
	import { allowAccess } from '@utility/permission.js';

	const dispatch = createEventDispatcher();
	export let key = '';
	export let title = '';
	export let hasIcon = false;
	export let icon = '';
	export let link = '';
	export let hasSubTitle = false;
	export let subTitle;
	export let sidebarCollapsed = false;
	export let items = [];
	export let userRoles = [];
	export let permissions = [];

	function handleSideBarSubTitleChange(event) {
		const { id, enabled, title } = event.detail;
		if (key === id) {
			hasSubTitle = enabled;
			subTitle = title;
		}
	}
</script>
{#if allowAccess(permissions, userRoles)}
	<li class="relative sub-item-wrapper" id="{key}">
		<button class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex 
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
		</button>
	</li>
	{#if items.length > 0}
		<SideBarSubMenuItem items={items} sidebarCollapsed={sidebarCollapsed} userRoles={userRoles} />
	{/if}
{/if}