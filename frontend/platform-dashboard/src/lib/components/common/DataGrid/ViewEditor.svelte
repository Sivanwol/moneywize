<script>
	import {
		Alert,
		Checkbox,
		Spinner,
		TabItem,
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell,
		Tabs
	} from 'flowbite-svelte';
	import { AdjustmentsVerticalSolid, GridSolid } from 'flowbite-svelte-icons';
	import { onMount } from 'svelte';
	import { GetAvailableViewColumns } from '@api/queries/view.query.js';
	import { getClient } from 'svelte-apollo';
	
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
		const { data, error } = await client.query({
			query: GetAvailableViewColumns,
			variables: {
				entityType: entityType
			}
		});
		loading = false;
		if (error) {
			showErrorMessage = true;
		}
		if (!data?.general?.availableColumns || data?.general?.availableColumns?.length === 0) {
			showErrorMessage = true;
		}
		data?.general?.availableColumns.forEach((column) => {
			column.checked = true;
			columns.push(column);
		});
		
		// console.log('currentView', currentView);
		// console.log('entityType', entityType);
		
	});
	const checkAllColumns = (e) => {
		columns.forEach((column) => {
			column.checked = e.target.checked;
		});
	};
	const checkColumn = (e, column) => {
		column.checked = e.target.checked;
	};
</script>

<Tabs style="underline">
	<TabItem open title="Profile">
		<div class="flex items-center gap-2" slot="title">
			<AdjustmentsVerticalSolid size="sm" />
			Settings
		</div>
		<p class="text-sm text-gray-500 dark:text-gray-400">
			update view/ select view
		</p>
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
				<Table>
					<TableHead>
						<TableHeadCell class="!p-4">
							<Checkbox color="purple" on:click={} />
						</TableHeadCell>
						<TableHeadCell>Column Name</TableHeadCell>
						<TableHeadCell>Is FilterAble</TableHeadCell>
						<TableHeadCell>Is Sortable</TableHeadCell>
						<TableHeadCell>Is Fixed</TableHeadCell>
					</TableHead>
					<TableBody class="divide-y dark:divide-gray-700">
						{#each columns as column}
							<TableBodyRow>
								<TableBodyCell>
									<Checkbox bind:checked={column.checked} color="blue"
														on:click={(event) =>checkColumn(event, column)} />
								</TableBodyCell>
								<TableBodyCell>{column.displayName}</TableBodyCell>
								<TableBodyCell>{column.isFilterAble ? 'Yes' : 'No'}</TableBodyCell>
								<TableBodyCell>{column.isSortable ? 'Yes' : 'No'}</TableBodyCell>
								<TableBodyCell>{column.isFixed ? 'Yes' : 'No'}</TableBodyCell>
							</TableBodyRow>
						{/each}
					</TableBody>
				</Table>
			{/if}
		{/if}
	</TabItem>
</Tabs>