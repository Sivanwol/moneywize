<script>
	import { onMount } from 'svelte';
	import { userStore } from '@stores/userStore.js';
	import { systemMessagesStore } from '@stores/systemMessagesStore.js';
	import { ErrorSystemMessage } from '@models/ErrorSystemMessage.js';
	import { _ } from 'svelte-i18n';
	import { Button, ButtonGroup, ListPlaceholder } from 'flowbite-svelte';
	import Grid from 'gridjs-svelte';
	import { get } from 'lodash-es';

	let columns = [];
	let currentView = null;
	export let data = [];
	export let userViewKey = null;
	let loading = false;
	export let debug = false;
	export let showFilter = true;
	export let showViewChanger = true;
	export let useManualDataLoading = false;
	export let paginationSize = 20;
	export let onHandlerDataLoading = async (ops) => {
	};
	export let onConvertHandlerData = (records) => {
		const newRecords = [];
		records.forEach(record => {
			const newRecord = {};
			currentView.columns.forEach(column => {
				if (column) {
					newRecord[column.fieldAlias] = get(record, column.fieldPath) || '';
				}
			});
			newRecords.push(newRecord);
		});
		return newRecords;
	};

	const style = {
		table: {
			'white-space': 'nowrap'
		}
	};

	const server = {
		url: 'https://api.github.com/users',
		data: async (ops) => {
			const data = await onHandlerDataLoading(ops);
			const convertData = onConvertHandlerData(data.records);
			if (debug) {
				console.log('data', data);
				console.log('convertData', convertData);
			}
			return {
				data: convertData,
				total: data.totalRecords
			};
		}
	};

	onMount(async () => {
		currentView = $userStore.userViews.find(view => view.clientKey === userViewKey);
		if (currentView == null) {
			systemMessagesStore.addMessage(ErrorSystemMessage.fromJson({
				message: $_('userActivity.errors.viewNotFound'),
				severity: 'critical',
				type: 'error'
			}));
			data = [];
			columns = [];
		}
		parseColumnsFromView();

		if (debug) {
			console.log('data', data);
			console.log('userViewKey', userViewKey);
			console.log('currentView', currentView);
			console.log('showFilter', showFilter);
			console.log('showViewChanger', showViewChanger);
			console.log('useManualDataLoading', useManualDataLoading);
			console.log('paginationSize', paginationSize);
		}
	});

	let pagination = {
		limit: paginationSize
	};

	function parseColumnsFromView() {
		let tColumns = currentView.columns;
		tColumns.sort((a, b) => a.order - b.order);
		tColumns.forEach(column => {
			columns.push({
				id: column.fieldAlias,
				name: column.displayName,
				sort: column.isSortable || false,
				hidden: column.isHidden
				// formatter: (cell, row) => {
				// 	return column.formatter(cell, row);
				// }
			});
		});
		if (debug) {
			console.log('Columns', columns);
		}
	}

	const onFilterOpen = (event) => {
		console.log('onFilterOpenClient', event);
	};
	const onChangeViewOpen = (event) => {
		console.log('onChangeViewOpenClient', event);
	};
</script>

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
			<Button on:click={onFilterOpen} outline>Change Filters</Button>
			<Button on:click={onChangeViewOpen} outline>Change View</Button>
		</ButtonGroup>
	</div>
	<div class="p-4 ag-theme-alpine">
		{#if !useManualDataLoading}
			<Grid
				data={data}
				columns={columns}
				server={server}
				style={style}
				pagination={pagination}
			/>
		{:else}
			<Grid
				data={data}
				columns={columns}
				style={style}
				pagination={pagination}
			/>
		{/if}
	</div>
{/if}