<script>
	import SideBarMenuItem from '@components/common/sidebar/SideBarMenuItem.svelte';
	import { allowAccess } from '@utility/permission.js';
	import * as _ from 'lodash';
	import { onMount } from 'svelte';

	const menuData = import('@data/sidebar.json');
	export let sidebarCollapsed = false;
	/** @type {string[]} */
	export let userRoles = [];

	let currentSectionTitle = '';
	/** @type {any} */
	let data = null;
	onMount(async () => {
		data = await menuData;
		data = _.get(data, 'sections', null);
	});

	function checkNewSection(title) {
		console.log(title, currentSectionTitle);
		if (title !== currentSectionTitle) {
			currentSectionTitle = title;
			return true;
		}
		return false;
	}
</script>
{#if data === null}
	<div class="flex flex-col items-center justify-center h-full">
		<div class="text-2xl font-bold text-center text-gray-400">Loading...</div>
	</div>
{:else}
	<nav
		class="navBar dark:bg-box-dark static start-0 top-[70px] z-[1035] h-full overflow-y-auto bg-dark xl:!w-[280px] xl:[&.collapsed]:!w-[80px] [&.collapsed]:!w-[250px] !transition-all !duration-[0.2s] ease-linear delay-[0s] !w-0 pb-[100px] scrollbar [&.collapsed]:ps-[7px] {sidebarCollapsed? 'collapsed': ''}">
		<ul class="relative m-0 list-none px-[0.2rem] ">
			{#each data as menuSection, i}
				{#if allowAccess(menuSection.roles || [], userRoles)}
					{#each menuSection.items as menuItem}
						<SideBarMenuItem {...menuItem}
														 sidebarCollapsed={sidebarCollapsed}
														 userRoles={userRoles}
														 hasSectionTitle={data[i].hasTitle && checkNewSection(data[i].title)}
														 sectionTitle={data[i].title} />
					{/each}
				{/if}
			{/each}
		</ul>
	</nav>
{/if}