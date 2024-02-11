<script>
	import {
		Alert,
		Checkbox,
		P,
		Spinner,
		TabItem,
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell,
		Tabs,
		Tooltip
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
		showErrorMessage = false;
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
		if (!data?.general?.getAvailableColumnsForView || data?.general?.getAvailableColumnsForView?.length === 0) {
			showErrorMessage = true;
		}
		columns = [];
		data?.general?.getAvailableColumnsForView.forEach((column) => {
			const obj = { ...column, checked: true };
			columns.push(obj);
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
				<div style="h-[250px]">
					<Table>
						<TableHead>
							<TableHeadCell>
								<Checkbox color="purple" on:click={checkAllColumns} />
							</TableHeadCell>
							<TableHeadCell>Column Name</TableHeadCell>
							<TableHeadCell>Is FilterAble</TableHeadCell>
							<TableHeadCell>Is Sortable</TableHeadCell>
							<TableHeadCell>Is Fixed</TableHeadCell>
						</TableHead>
						<TableBody class="overflow-y-auto">
							{#each columns as column}
								<TableBodyRow>
									<TableBodyCell>
										<Checkbox bind:checked={column.checked} color="blue"
															on:click={(event) =>checkColumn(event, column)} />
									</TableBodyCell>
									<TableBodyCell>
										<P id="column-{column.id}">{column.displayName}</P>
										<Tooltip triggeredBy="#column-{column.id}">
											{column.description}
										</Tooltip>
									</TableBodyCell>
									<TableBodyCell>{column.isFilterAble ? 'Yes' : 'No'}</TableBodyCell>
									<TableBodyCell>{column.isSortable ? 'Yes' : 'No'}</TableBodyCell>
									<TableBodyCell>{column.isFixed ? 'Yes' : 'No'}</TableBodyCell>
								</TableBodyRow>
							{/each}
						</TableBody>
					</Table>
				</div>
			{/if}
		{/if}
	</TabItem>
</Tabs>