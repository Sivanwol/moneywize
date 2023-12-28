<script>
	import { allowAccess } from '@utility/permission.js';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();
	export let items = [];
	export let sidebarCollapsed = false;
	export let userRoles = [];

	
</script>
<ul
	class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll  {sidebarCollapsed? 'hidden': ''} ">
	{#each items as menuItem (menuItem.key)}
		{#if allowAccess(menuItem.permissions || [], userRoles)}
			<li class="relative">
				<a id={menuItem.key}
					 class="rounded-e-[20px] text-gray-600 hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up "
					 href="{menuItem.link}">
					<span>{menuItem.title}</span></a>
			</li>
		{/if}
	{/each}
</ul>