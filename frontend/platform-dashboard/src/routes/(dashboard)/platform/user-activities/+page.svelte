<script>
	import { InfoCircleSolid } from 'flowbite-svelte-icons';
	import Grid from 'gridjs-svelte';
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
	import { SortDirection } from '@api/queries/utils/SortDirection.js';
	import { get } from 'lodash-es';

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
		console.log('onMount', $userStore.userViews);
		currentView = $userStore.userViews.find(view => view.clientKey === currentViewKey);
		if (currentView == null) {
			systemMessagesStore.addMessage(ErrorSystemMessage.fromJson({
				message: $_('userActivity.errors.viewNotFound'),
				severity: 'critical',
				type: 'error'
			}));
			data = [];
			columns = [];
		}
		console.log(currentView);
		parseColumnsFromView();
		console.log(currentView);
	});

	function parseColumnsFromView() {
		let tColumns = currentView.columns;
		tColumns.sort((a, b) => a.order - b.order);
		tColumns.forEach(column => {
			columns.push({
				id: column.fieldAlias,
				name: column.displayName,
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
			const res = await client.query({
				query: GetUserActivities([]),
				variables: {
					pageSize: PAGE_SIZE.TWENTY.query,
					sortField: 'CreatedAt',
					userId: $userStore.profile.id,
					viewClientId: currentView.clientKey,
					sortDirection: SortDirection.DESCENDING,
					page: 1
				}
			});

		console.log(res);
			if (res?.data?.user?.getUserActivities) {
				console.log(res?.data?.user?.getUserActivities);
				return res?.data?.user?.getUserActivities;
			}
			console.log('No data');
		return {
			records: [],
			totalRecords: 0,
			totalPages: 0
		};
	}

	const pagination = {
		limit: 20
	};
	const convertRecordsToFieldAlias = (records) => {
		const newRecords = [];
		records.forEach(record => {
			const newRecord = {};
			Object.keys(record).filter(f => f !== '__typename').forEach(key => {
				const column = currentView.columns.find(column => column.fieldAlias === key);
				if (column) {
					newRecord[column.fieldAlias] = get(record[key], column.fieldPath);
				}
			});
			newRecords.push(newRecord);
		});
		return newRecords;
	};
	const server = {
		url: 'https://api.github.com/users',
		data: async (ops) => {
			console.log(ops);
			const dta = await fetchData();
			console.log(dta);
			const data = convertRecordsToFieldAlias(dta.records);
			console.log(data);
			debugger;
			return {
				data,
				total: dta.totalRecords
			};
		}
	};
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
					<Grid columns={columns} server={server} pagination={pagination} />
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
