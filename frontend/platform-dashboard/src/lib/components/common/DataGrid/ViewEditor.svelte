<script>
	import {
		Alert,
		Checkbox,
		Input,
		Label,
		Li,
		List,
		P,
		Spinner,
		TabItem,
		Tabs,
		Textarea,
		Toggle,
		Tooltip
	} from 'flowbite-svelte';
	import { AdjustmentsVerticalSolid, GridSolid } from 'flowbite-svelte-icons';
	import { onMount } from 'svelte';
	import { GetAvailableViewColumns } from '@api/queries/view.query.js';
	import { getClient } from 'svelte-apollo';
	
	$: client = getClient();
	$: loading = false;
	$: showErrorMessage = false;
	$: hasCheckAllColumns = false;
	$: columns = [];
	
	
	let hovering = false;
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
	const checkColumn = (e, column) => {
		column.checked = e.target.checked;
	};
	
	const drop = (event, target) => {
		event.preventDefault();
		event.dataTransfer.dropEffect = 'move';
		const start = parseInt(event.dataTransfer.getData('text/plain'));
		const newTracklist = columns;
		
		if (start < target) {
			newTracklist.splice(target + 1, 0, newTracklist[start]);
			newTracklist.splice(start, 1);
		} else {
			newTracklist.splice(target, 0, newTracklist[start]);
			newTracklist.splice(start + 1, 1);
		}
		let order = 0;
		console.log('newTracklist', newTracklist);
		newTracklist.forEach((column, index) => {
			order++;
			column.order = order;
		});
		console.log('newTracklist', newTracklist);
		columns = newTracklist;
		hovering = null;
	};
	
	const dragstart = (event, i) => {
		event.dataTransfer.effectAllowed = 'move';
		event.dataTransfer.dropEffect = 'move';
		const start = i;
		event.dataTransfer.setData('text/plain', start);
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
				<div style="h-[100px]">

					<List tag="ul" list="none">
						<h3 class="p-1 text-center text-xl font-medium text-gray-900 dark:text-white">User list</h3>
						{#each columns as column , index (column.id)}
							<div
								role="listitem"
								animation:flip
								draggable={true}
								on:dragstart={event => dragstart(event, index)}
								on:drop={event => drop(event, index)}
								on:dragenter={() => hovering = index}
								on:dragover={(e) => e.preventDefault()}>
								<Li class="h-[60px]">
									<div class="flex items-center justify-evenly gap-2">
										<div class="text-left">
											<Toggle checked={!column.isHidden} color="blue"
															on:click={(event) =>checkColumn(event, column)} />
										</div>
										<div>
										<P id="column-{column.id}">{column.displayName}</P>
										<Tooltip triggeredBy="#column-{column.id}">
											{column.description}
										</Tooltip>
										</div>
										<div class="flex items-center justify-evenly gap-2 flex-col">
											<h2>Is Sortable</h2>
											<Checkbox bind:checked={column.isSortable} disabled color="blue" />
										</div>
										<div class="flex items-center justify-evenly gap-2 flex-col">
											<h2>Is Filterable</h2>
											<Checkbox bind:checked={column.isFilterable} disabled color="blue" />
										</div>
										<div class="flex items-center justify-evenly gap-2 flex-col">
											<h2>Is Fixed</h2>
											<Checkbox bind:checked={column.isFixed} disabled color="blue" />
										</div>

								</Li>
							</div>
						{/each}
					</List>
				</div>
			{/if}
		{/if}
	</TabItem>
</Tabs>