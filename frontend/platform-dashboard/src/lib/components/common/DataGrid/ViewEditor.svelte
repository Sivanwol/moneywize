<script>
	import { Alert, Input, Label, Spinner, TabItem, Tabs, Textarea, Toggle } from 'flowbite-svelte';
	import uuid from 'uuid';
	import { AdjustmentsVerticalSolid, GridSolid } from 'flowbite-svelte-icons';
	import { onMount } from 'svelte';
	import { GetAvailableViewColumns } from '@api/queries/view.query.js';
	import { getClient } from 'svelte-apollo';
	import ColumnSelectorView from '@components/common/DataGrid/ColumnSelectorView.svelte';
	
	$: client = getClient();
	$: loading = false;
	$: showErrorMessage = false;
	$: columns = [];
	
	
	/**
	 * @param {boolean} debug flag
	 */
	export let debug;
	export let entityType;
	export let currentView;
	onMount(async () => {
		showErrorMessage = false;
		const { data, error } = await client.query({
			query: GetAvailableViewColumns,
			variables: {
				entityType
			}
		});
		loading = false;
		if (error) {
			showErrorMessage = true;
		}
		if (!data?.general?.getAvailableColumnsForView || data?.general?.getAvailableColumnsForView?.length === 0) {
			showErrorMessage = true;
		}
		columns = [];
		data?.general?.getAvailableColumnsForView.forEach((column) => {
			const obj = { ...column, checked: false };
			let hasChecked = currentView.columns.find((c) => c.id === column.id);
			if (hasChecked) {
				obj.checked = true;
			}
			obj.generatedId = uuid.v4();
			columns.push(obj);
		});
		
		columns.filter(column => column.checked).length === columns.length ? hasCheckAllColumns = true : hasCheckAllColumns = false;
		
		// console.log('currentView', currentView);
		// console.log('entityType', entityType);
		
	});
	const checkAllColumns = (e) => {
		columns.forEach((column) => {
			column.checked = e.target.checked;
		});
		columns = [...columns];
	};
	const saveColumns = (columns) => {
		console.log('columns', columns);
	};
</script>
<Tabs style="underline">
	<TabItem open title="Profile">
		<div class="flex items-center gap-2" slot="title">
			<AdjustmentsVerticalSolid size="sm" />
			Settings
		</div>
		<div class="mb-6">
			<div class="flex items-baseline">
				<span class="flex-shrink-0 rounded-full w-2.5 h-2.5 bg-orange-600 me-1.5"
							style="background-color: '#{currentView.color}'"></span>
				<Label class="block mb-2" for="view-name-input">Name of the view</Label>
			</div>
			<Input id="view-name-input" placeholder="Name of view" readonly size="lg" value={currentView.name} />
		</div>
		<div class="mb-6">
			<Label class="block mb-2" for="view-base-input">Is base view</Label>
			<Toggle bind:checked={currentView.isPredefined} disabled id="view-base-input" />
		</div>

		<div class="mb-6">
			<Label class="block mb-2" for="view-base-input">Is view shareable</Label>
			<Toggle bind:checked={currentView.isShareAble} disabled id="view-base-input" />
		</div>
		<div class="mb-6">
			<Label class="block mb-2" for="view-desc-input">Name of the view</Label>
			<Textarea id="view-desc-input" placeholder="view description" readonly rows="4" value={currentView.description} />
		</div>
	</TabItem>
	<TabItem title="Dashboard">
		<div class="flex items-center gap-2" slot="title">
			<GridSolid size="sm" />
			Change Columns
		</div>
		{#if loading && !showErrorMessage}
			<Spinner color="green" />
		{:else}
			{#if showErrorMessage}
				<Alert color="red">
					<span class="font-medium">Unable to load columns for the view!</span>
					<p class="text-red-500 dark:text-red-400">Error loading columns</p>
				</Alert>
			{:else}
				<ColumnSelectorView
					{columns}
					{saveColumns} />

			{/if}
		{/if}
	</TabItem>
</Tabs>