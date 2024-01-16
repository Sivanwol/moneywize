<script>
	import { InfoCircleSolid } from 'flowbite-svelte-icons';
	import { Breadcrumb, BreadcrumbItem, Button, ButtonGroup, Card, ListPlaceholder } from 'flowbite-svelte';
	import PropDrawer from '@components/common/PropDrawer.svelte';
	import { createEventDispatcher } from 'svelte';
	import { userStore } from '@stores/userStore';
	import Grid from 'gridjs-svelte';
	import { PUBLIC_VIEWS_USER_ACTIVITY } from '$env/static/public';

	let defaultViewKey = PUBLIC_VIEWS_USER_ACTIVITY;
	let currentViewKey = null || defaultViewKey;
	let enabledDrawer = true;
	let hiddenDrawer = true;
	let loading = false;
	const dispatch = createEventDispatcher();
	$: currentView = $userStore.userViews.find(view => view.clientKey === currentViewKey);
	function handlePropDrawerToggle() {
		if (enabledDrawer) {
			hiddenDrawer = !hiddenDrawer;
		}
	}

	const data = [
		{ name: 'John', email: 'john@example.com' },
		{ name: 'Mark', email: 'mark@gmail.com' }
	];

	function onLoadingRequest(event) {
		console.log(event.detail);
		loading = !loading;
	}
</script>

<div
	class="px-[25px] text-dark dark:text-title-dark font-medium text-[17px] flex flex-wrap items-center justify-between max-sm:flex-col max-sm:h-auto border-b border-regular dark:border-white/10">
	<Breadcrumb aria-label="Default breadcrumb example">
		<BreadcrumbItem home href="/platform/main">Home</BreadcrumbItem>
		<BreadcrumbItem>User Activity</BreadcrumbItem>
	</Breadcrumb>
</div>
<div class="p-[25px] bg-dark rounded-10 dark:bg-box-dark ">
	<div class="ml-4 mr-4 w-full bg-gray-700 p-4">
		<div class="flex flex-col bg-gray-800 text-white">
			<div>Hello world</div>
			<div class="flex-grow">
				<Button color="dark" on:click={handlePropDrawerToggle} outline>Toggle Prop Draw</Button>
			</div>
		</div>
	</div>
</div>
<!-- Right Bar -->
<PropDrawer enabled={enabledDrawer} hidden={hiddenDrawer}>
	<div slot="propBarContent">
		<Card on:loadingRequest={onLoadingRequest}>
			{#if loading}
				<div class="flex justify-center items-center h-40">
					<div class="flex items-center space-x-2 text-gray-500 dark:text-gray-400">
						<ListPlaceholder />
					</div>
				</div>
			{:else}
				<div class="flex justify-between items-center mb-4">
					<h5 class="text-xl font-bold leading-none text-gray-900 dark:text-white">User Activity</h5>
					<ButtonGroup>
						<Button>Change Filters</Button>
						<Button>Change View</Button>
					</ButtonGroup>
				</div>
				<div class="p-4">
					<Grid data={data} />
				</div>
			{/if}
		</Card>
	</div>
	<div slot="propBarHeader">
		<h5 class="inline-flex items-center mb-4 text-base font-semibold text-gray-500 dark:text-gray-400"
				id="drawer-label">
			<InfoCircleSolid class="w-4 h-4 me-2.5" />
			Info
		</h5>
	</div>
</PropDrawer>
