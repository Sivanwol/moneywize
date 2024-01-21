<script>
	import { InfoCircleSolid } from 'flowbite-svelte-icons';
	import { Grid } from 'gridjs';
	import { Breadcrumb, BreadcrumbItem, Button, ButtonGroup, Card, ListPlaceholder } from 'flowbite-svelte';
	import PropDrawer from '@components/common/PropDrawer.svelte';
	import { createEventDispatcher, onMount } from 'svelte';
	import { userStore } from '@stores/userStore';
	import { PUBLIC_VIEWS_USER_ACTIVITY } from '$env/static/public';
	import { _ } from 'svelte-i18n';
	import { systemMessagesStore } from '@stores/systemMessagesStore';
	import { ErrorSystemMessage } from '@models/ErrorSystemMessage.js';
	import { getClient } from 'svelte-apollo';
	import { GetUserActivities } from '@api/queries/user.query.js';
	import { PAGE_SIZE } from '@api/queries/utils/PageSize.js';

	$: client = getClient();
	$: currentPage = 1;

	let currentViewKey = null || PUBLIC_VIEWS_USER_ACTIVITY;
	let currentView = null;
	let enabledDrawer = true;
	let hiddenDrawer = true;
	let loading = false;
	let columns = [];
	const dispatch = createEventDispatcher();
	function handlePropDrawerToggle() {
		if (enabledDrawer) {
			hiddenDrawer = !hiddenDrawer;
		}
	}

	let data = [];

	function onLoadingRequest(event) {
		console.log(event.detail);
		loading = !loading;
	}

	onMount(async () => {
		currentView = $userStore.userViews.find(view => view.clientKey === currentViewKey);
		if (currentView == null) {
			systemMessagesStore.addMessage(ErrorSystemMessage.fromJson({
				message: _('userActivity.errors.viewNotFound'),
				severity: 'critical',
				type: 'error'
			}));
			data = [];
		}
		parseColumnsFromView(currentView);
		console.log(currentView);
	});

	function parseColumnsFromView(view) {
		let tColumns = view.columns;
		tColumns.sort((a, b) => a.order - b.order);
		tColumns.forEach(column => {
			columns.push({
				id: column.columnName,
				field: column.fieldAlias,
				sort: column.isSortable || false,
				hidden: column.isHidden,
				data: column,
				width: '30%'
				// formatter: (cell, row) => {
				// 	return column.formatter(cell, row);
				// }
			});
		});
		console.log(columns);
	}

	async function fetchData() {
		return new Promise(async (resolve) => {
			const res = await client.query({
				query: GetUserActivities(currentView.columns, []),
				variables: {
					pageSize: PAGE_SIZE.TWENTY.gql,
					page: 1
				}
			});
			if (res?.data?.user?.getUserActivities) {
				console.log(res?.data?.user?.getUserActivities);
				return resolve(res?.data?.user?.getUserActivities);
			}
			console.log('No data');
			return resolve([]);
		});
	}

</script>

<svelte:head>
	<title>{$_('userActivity.title')}</title>
</svelte:head>
<div
	class="px-[25px] text-dark dark:text-title-dark font-medium text-[17px] flex flex-wrap items-center justify-between max-sm:flex-col max-sm:h-auto border-b border-regular dark:border-white/10">
	<Breadcrumb aria-label="Default breadcrumb example">
		<BreadcrumbItem home href="/platform/main">Home</BreadcrumbItem>
		<BreadcrumbItem>User Activity</BreadcrumbItem>
	</Breadcrumb>
</div>
<div class="p-[25px] bg-dark rounded-10 dark:bg-box-dark ">
	<div class="ml-4 mr-4 w-full p-4 ">
		<Card on:loadingRequest={onLoadingRequest} size="xl">
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
						<Button on:click={handlePropDrawerToggle} outline>Change Filters</Button>
						<Button on:click={handlePropDrawerToggle} outline>Change View</Button>
					</ButtonGroup>
				</div>
				<div class="p-4 ag-theme-alpine">
					<Grid columns={columns} data={fetchData} />
				</div>
			{/if}
		</Card>
	</div>
</div>
<!-- Right Bar -->
<PropDrawer enabled={enabledDrawer} hidden={hiddenDrawer}>
	<div slot="propBarContent">
		<h3>Change</h3>
	</div>
	<div slot="propBarHeader">
		<h5 class="inline-flex items-center mb-4 text-base font-semibold text-gray-500 dark:text-gray-400"
				id="drawer-label">
			<InfoCircleSolid class="w-4 h-4 me-2.5" />
			Info
		</h5>
	</div>
</PropDrawer>
