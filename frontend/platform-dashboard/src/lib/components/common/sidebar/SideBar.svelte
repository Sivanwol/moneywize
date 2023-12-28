<script>
	import SideBarMenuItem from '@components/common/sidebar/SideBarMenuItem.svelte';
	import { allowAccess } from '@utility/permission.js';
	import { onMount } from 'svelte';

	const menuData = import('@data/sidebar.json');
	export let sidebarCollapsed = false;
	export let userRoles = [];


	let data = {};
	onMount(async () => {
		data = await menuData;
		console.log(data);
	});
	console.log(data, menuData);
</script>
{@debug menuData}
<nav
	class="navBar dark:bg-box-dark static start-0 top-[70px] z-[1035] h-full overflow-y-auto bg-dark xl:!w-[280px] xl:[&.collapsed]:!w-[80px] [&.collapsed]:!w-[250px] !transition-all !duration-[0.2s] ease-linear delay-[0s] !w-0 pb-[100px] scrollbar [&.collapsed]:ps-[7px] {sidebarCollapsed? 'collapsed': ''}">
	<ul class="relative m-0 list-none px-[0.2rem] ">
		{#each data.sections as menuSection}
			{#if allowAccess(menuSection.roles || [], userRoles)}
				<SideBarMenuItem {...menuSection} sidebarCollapsed={sidebarCollapsed} userRoles={userRoles} />
			{/if}
		{/each}
	</ul>
</nav>