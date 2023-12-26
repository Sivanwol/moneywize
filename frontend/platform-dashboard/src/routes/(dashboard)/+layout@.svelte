<script>
	import {
		Sidebar,
		SidebarDropdownItem,
		SidebarDropdownWrapper,
		SidebarGroup,
		SidebarItem,
		SidebarWrapper
	} from 'flowbite-svelte';
	import {
		ArrowRightToBracketSolid,
		ChartPieSolid,
		FileEditSolid,
		GridSolid,
		MailBoxSolid,
		ShoppingCartSolid,
		UserSolid
	} from 'flowbite-svelte-icons';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();
	let spanClass = 'flex-1 ms-3 whitespace-nowrap';
</script>
<style lang="postcss">
    /* Mobile devices */
    @media only screen and (max-width: 600px) {
        .responsive-grid {
            height: 70vh;
        }
    }

    /* Desktop devices */
    @media only screen and (min-width: 601px) {
        .responsive-grid {
            height: 90vh;
        }
    }
</style>
<div class="grid grid-rows-6 gap-4 text-white responsive-grid">
	<!-- Header -->
	<div class="col-span-12 row-span-1 flex items-center justify-between dark:bg-gray-800 p-4">
		<!-- Logo -->
		<div>
			<img alt="Logo" src="/img/logo/logo.svg" width="100 " />
		</div>
		<!-- Search Bar -->
		<div class="mx-4 flex-grow">
			<input class="w-full rounded border border-gray-600 bg-gray-700 p-2 placeholder-gray-400" placeholder="Search..."
						 type="text" />
		</div>
		<!-- Notifications and User Icon -->
		<div class="flex space-x-4">
			<!-- Notifications -->
			<div class="relative">
				<button class="focus:outline-none">
					<svg class="h-6 w-6 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"
							 xmlns="http://www.w3.org/2000/svg">
						<path d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158l-1.493-1.493c-.27-.27-.621-.434-1-.466V11a3 3 0 00-6 0v1.192l-1.447 1.447A2.068 2.068 0 017 17H2" stroke-linecap="round" stroke-linejoin="round"
									stroke-width="2"></path>
					</svg>
				</button>
				<span
					class="absolute right-0 top-0 inline-flex -translate-y-1/2 translate-x-1/2 transform items-center justify-center rounded-full bg-red-600 px-2 py-1 text-xs font-bold leading-none text-red-100">3</span>
			</div>
			<!-- User Icon -->
			<div>
				<img alt="User Icon" class="h-8 w-8 rounded-full" src="user_icon.png" />
			</div>
		</div>
	</div>
	<div class="col-span-12 row-span-4 flex h-full w-full m-4 mb-0">
		<!-- Sidebar -->
		<div class="w-1/5 bg-gray-700 p-4">
			<Sidebar>
				<SidebarWrapper>
					<SidebarGroup>
						<SidebarItem label="Dashboard">
							<svelte:fragment slot="icon">
								<ChartPieSolid
									class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white" />
							</svelte:fragment>
						</SidebarItem>
						<SidebarDropdownWrapper label="E-commerce">
							<svelte:fragment slot="icon">
								<ShoppingCartSolid
									class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white" />
							</svelte:fragment>
							<SidebarDropdownItem label="Products" />
							<SidebarDropdownItem label="Billing" />
							<SidebarDropdownItem label="Invoice" />
						</SidebarDropdownWrapper>
						<SidebarItem label="Kanban" on:click={() => (dispatch('propDrawerToggle'))} {spanClass}>
							<svelte:fragment slot="icon">
								<GridSolid
									class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white" />
							</svelte:fragment>
							<svelte:fragment slot="subtext">
								<span
									class="inline-flex justify-center items-center px-2 ms-3 text-sm font-medium text-gray-800 bg-gray-200 rounded-full dark:bg-gray-700 dark:text-gray-300"> Pro </span>
							</svelte:fragment>
						</SidebarItem>
						<SidebarItem label="Inbox" {spanClass}>
							<svelte:fragment slot="icon">
								<MailBoxSolid
									class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white" />
							</svelte:fragment>
							<svelte:fragment slot="subtext">
								<span
									class="inline-flex justify-center items-center p-3 ms-3 w-3 h-3 text-sm font-medium text-primary-600 bg-primary-200 rounded-full dark:bg-primary-900 dark:text-primary-200"> 3 </span>
							</svelte:fragment>
						</SidebarItem>
						<SidebarItem label="Users">
							<svelte:fragment slot="icon">
								<UserSolid
									class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white" />
							</svelte:fragment>
						</SidebarItem>
						<SidebarItem label="Update Profile">
							<svelte:fragment slot="icon">
								<ArrowRightToBracketSolid
									class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white" />
							</svelte:fragment>
						</SidebarItem>
						<SidebarItem label="Logout">
							<svelte:fragment slot="icon">
								<FileEditSolid
									class="w-5 h-5 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900 dark:group-hover:text-white" />
							</svelte:fragment>
						</SidebarItem>
					</SidebarGroup>
				</SidebarWrapper>
			</Sidebar>
		</div>
		<slot />
	</div>

	<div class="col-span-12 row-span-1 gap-0 flex h-full w-full m-4 mb-10 bg-gray-700 ml-4 mr-2">&nbsp;</div>
</div>