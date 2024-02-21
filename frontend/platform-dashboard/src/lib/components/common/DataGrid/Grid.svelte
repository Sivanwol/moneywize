<script>
	import { onMount } from 'svelte';
	import { userStore } from '@stores/userStore.js';
	import { systemMessagesStore } from '@stores/systemMessagesStore.js';
	import { ErrorSystemMessage } from '@models/ErrorSystemMessage.js';
	import { _ } from 'svelte-i18n';
	import {
		Accordion,
		AccordionItem,
		Button,
		ButtonGroup,
		Input,
		Label,
		ListPlaceholder,
		Modal,
		Select,
		TabItem,
		Tabs,
		Textarea,
		Toggle,
		Tooltip
	} from 'flowbite-svelte';
	import { fade } from 'svelte/transition';
	import Grid from 'gridjs-svelte';
	import uuid from 'uuid';
	import { capitalize, get } from 'lodash-es';
	import { SortDirection } from '@api/utils/SortDirection.js';
	import { PAGE_SIZE } from '@api/utils/PageSize.js';
	import moment from 'moment';
	import { format } from '@utility/stringExtended.js';
	import { FieldType } from '@api/utils/fieldType.js';
	import { AdjustmentsVerticalSolid, PlusOutline } from 'flowbite-svelte-icons';
	import { html } from 'gridjs';
	import ViewEditor from '@components/common/DataGrid/ViewEditor.svelte';
	import ColumnSelectorView from '@components/common/DataGrid/ColumnSelectorView.svelte';
	
	let columns = [];
	let currentView = null;
	
	export let entityType = '';
	
	export let data = [];
	let visibleViewEditor = false;
	let visibleFilterEditor = false;
	export let userViewKey = null;
	let loading = false;
	export let debug = false;
	export let showFilter = true;
	export let showViewChanger = true;
	export let useManualDataLoading = false;
	export let currentPage = 1;
	export let paginationSize = PAGE_SIZE.TWENTY;
	
	let changeViewModel = false;
	export let onHandlerDataLoading = async (ops, sortByField, sortByDirection, currentPage, paginationSize) => {
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
	$: sortByField = 'CreatedAt';
	$: sortByDirection = SortDirection.DESCENDING;
	$: showCustomzitionBar = false;//entityType !== null || !isMatchingEntityType(entityType);
	const sort = {
		multiColumn: false,
		server: {
			url: (prev, sortColumns) => {
				if (!sortColumns.length) return prev;
				
				const { index, direction } = sortColumns[0];
				const dir = direction === 1 ? SortDirection.ASCENDING : SortDirection.DESCENDING;
				const col = columns[index];
				if (!col.sort) return prev;
				sortByField = capitalize(col.columnName);
				sortByDirection = dir;
				if (debug) {
					console.log('sort', col);
					console.log('sortByField', sortByField);
					console.log('sortByDirection', sortByDirection);
				}
				return `${prev}&sort=${sortByField}&direction=${dir}`;
			}
		}
	};
	const server = {
		url: 'https://api.github.com/users',
		data: async (ops) => {
			const data = await onHandlerDataLoading(ops, sortByField, sortByDirection, currentPage, paginationSize.query);
			const convertData = onConvertHandlerData(data.records);
			if (debug) {
				console.log('data', data);
				console.log('convertData', convertData);
				console.log('sortByField', sortByField);
				console.log('sortByDirection', sortByDirection);
				console.log('currentPage', currentPage);
			}
			return {
				data: convertData,
				total: data.totalRecords
			};
		}
	};
	
	let views = [];
	let selectedView = '';
	let selectedViewInfo = null;
	let selectedViewInfoColumns = [];
	onMount(async () => {
		currentView = $userStore.userViews.find(view => view.clientKey === userViewKey);
		$userStore.userViews.filter(view => view.entityType === currentView.entityType).forEach(view => {
			views.push({
				value: view.clientKey,
				name: view.name
			});
		});
		if (currentView == null) {
			systemMessagesStore.addMessage(ErrorSystemMessage.fromJson({
				message: $_('userActivity.errors.viewNotFound'),
				severity: 'critical',
				type: 'error'
			}));
			data = [];
			columns = [];
		}
		selectedView = currentView.clientKey;
		selectedViewInfo = currentView;
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
		limit: paginationSize.value,
		server: {
			url: (prev, page, limit) => {
				if (debug) {
					console.log('pagination', page, limit);
				}
				currentPage = page + 1;
				return `${prev}&page=${page}&limit=${limit}`;
			}
		}
	};
	
	function parseColumnsFromView() {
		showCustomzitionBar = true;
		let tColumns = currentView.columns;
		tColumns.sort((a, b) => a.order - b.order);
		tColumns.forEach(column => {
			let columnDef = {
				id: column.fieldAlias,
				name: column.displayName,
				sort: column.isSortable || false,
				hidden: column.isHidden,
				columnName: column.columnName,
				// @todo this may impact performance on large datasets but not sure
				formatter: (cell) => {
					let trimCell = cell;
					if (cell.length > 75) {
						trimCell = cell.substring(0, 75) + '...';
					}
					html('');
					if (column.fieldFormatter === null || column.fieldFormatter === '') {
						if (column.dataType === FieldType.STRING) {
							return trimCell;
						} else {
							return;
						}
						return cell;
					}
					if (column.dataType === FieldType.DATE) {
						return moment(cell).format(column.fieldFormatter);
					}
					if (column.dataType === FieldType.NUMBER || column.dataType === FieldType.STRING) {
						return format(column.fieldFormatter, cell);
					}
					return cell;
				}
			};
			columns.push(columnDef);
		});
		if (debug) {
			console.log('Columns', columns);
		}
	}
	
	const onViewToggle = (event) => {
		console.log('onFilterOpenClient', event);
		visibleFilterEditor = false;
		visibleViewEditor = !visibleViewEditor;
	};
	const onFilterToggle = (event) => {
		console.log('onChangeViewOpenClient', event);
		visibleViewEditor = false;
		visibleFilterEditor = !visibleFilterEditor;
	};
	const onViewChange = (event) => {
		changeViewModel = !changeViewModel;
		if (!changeViewModel) return;
		let tColumns = [];
		currentView.columns.forEach((column, index) => {
			tColumns.push({
				...column,
				generatedId: uuid.v4()
			});
		});
		selectedViewInfoColumns = tColumns;
	};
	const onViewSelect = (event) => {
		console.log('onViewSelect', event, selectedView);
		selectedViewInfo = $userStore.userViews.find(view => view.clientKey === selectedView);
		selectedViewInfo.columns.sort((a, b) => a.order - b.order);
		let tColumns = [];
		selectedViewInfo.columns.forEach((column, index) => {
			tColumns.push({
				...column,
				generatedId: uuid.v4()
			});
		});
		selectedViewInfoColumns = tColumns;
	};
	const saveColumns = (columns) => {
		// console.log('selectedColumns', selectedColumns);
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
			<Button on:click={onViewChange} outline>Change View</Button>
			<Button on:click={onViewToggle} outline>{visibleViewEditor ? 'Hide' : 'Open'} View Information</Button>
			<Button on:click={onFilterToggle} outline>{visibleFilterEditor ? 'Hide' : 'Open'} Filters</Button>
		</ButtonGroup>
	</div>

	{#if (visibleViewEditor && showCustomzitionBar)}
		<div role="link" in:fade out:fade>
			<ViewEditor
				debug={debug}
				entityType={currentView.entityType}
				currentView={currentView}
				on:reload={() => visibleViewEditor = false}
			/>
		</div>
	{/if}
	{#if (visibleFilterEditor && showCustomzitionBar)}
		<div in:fade out:fade>
			<Tabs style="underline">
				<TabItem open title="Profile">
					<div slot="title" class="flex items-center gap-2">
						<AdjustmentsVerticalSolid size="sm" />
						Change Filters
					</div>
					<p class="text-sm text-gray-500 dark:text-gray-400">
						update Filters/ select Filters
					</p>
				</TabItem>
			</Tabs>
		</div>
	{/if}
	<div class="p-4 ag-theme-alpine">
		{#if !useManualDataLoading}
			<Grid
				data={data}
				columns={columns}
				server={server}
				style={style}
				sort={sort}
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
		{#if changeViewModel}
			<Modal bind:open={changeViewModel} size="md" class="w-full" placement="center">
				<h5 class="font-bold text-gray-900 dark:text-white">Change View</h5>
				<div class="mb-6">
					<Label for="view-select-input" class="block mb-2">Selected View</Label>
					<Select id="view-select-input" items={views} bind:value={selectedView} on:change={onViewSelect}
									placeholder="You can't select anything..." /> Or
					<Button id="create-view">
						Create New view
						<PlusOutline class="w-3.5 h-3.5 ms-2" />
					</Button>
					<Tooltip triggeredBy="[id^='create-view']" placement='bottom'>
						You can create new view it will base the selected view if shareable. <br />
						if not it be based on the base view
					</Tooltip>
				</div>
				<div class="mb-6">
					<div class="flex items-baseline">
					<span style="background-color: '#{currentView.color}'"
								class="flex-shrink-0 rounded-full w-2.5 h-2.5 bg-orange-600 me-1.5"></span>
						<Label for="view-name-input" class="block mb-2">Name of the view</Label>
					</div>
					<Input id="view-name-input" size="lg" placeholder="Name of view" readonly value={selectedViewInfo.name} />
				</div>
				<div class="mb-6">
					<Label for="view-base-input" class="block mb-2">Is base view</Label>
					<Toggle id="view-base-input" color="purple" bind:checked={selectedViewInfo.isPredefined} disabled />
				</div>

				<div class="mb-6">
					<Label for="view-base-input" class="block mb-2">Is view shareable</Label>
					<Toggle id="view-base-input" color="yellow" bind:checked={selectedViewInfo.isShareAble} disabled />
				</div>
				<div class="mb-6">
					<Label for="view-desc-input" class="block mb-2">Name of the view</Label>
					<Textarea id="view-desc-input" placeholder="view description" rows="4" readonly
										value={selectedViewInfo.description} />
				</div>

				<Accordion>
					<AccordionItem>
						<span slot="header">View has total of {selectedViewInfoColumns.length} columns</span>
						<ColumnSelectorView
							columns={selectedViewInfoColumns}
							{saveColumns} />
					</AccordionItem>
				</Accordion>
				<div class="flex justify-end mt-4">
					<ButtonGroup>
						<Button color="blue">Save</Button>
						<Button on:click={onViewChange}>Cancel</Button>
					</ButtonGroup>
				</div>
			</Modal>
		{/if}
	</div>
{/if}