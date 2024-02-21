<script>
	import { Checkbox, Li, List, P, Toggle, Tooltip } from 'flowbite-svelte';
	import { flip } from 'svelte/animate';
	import { quintOut } from 'svelte/easing';
	
	export let columns = [];
	export let saveColumns = (columns) => {
	};
	let hovering = null;
	$: console.log('columns', columns);
	const checkColumn = (event, column) => {
		column.isHidden = !column.isHidden;
	};
	const drop = (event, target) => {
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
<div class="w-full">

	<List list="none" tag="ul">
		<h3 class="p-1 text-center text-xl font-medium text-gray-900 dark:text-white">User list</h3>
		{#each columns as column , index (column.generatedId)}
			<div
				animate:flip={{duration: 300, easing: quintOut}}
				role="listitem" class="h-[60px]"
				draggable={true}
				on:dragstart={event => dragstart(event, index)}
				on:drop|preventDefault={event => drop(event, index)}
				on:dragenter={() => hovering = index}
				on:dragover={(e) => e.preventDefault()}>
				<Li class="h-[60px]">
					<div class="flex flex-column items-center justify-between gap-2">
						<div class="text-left">
							<Toggle checked={!column.isHidden} color="blue"
											on:click={(event) =>checkColumn(event, column)} />
						</div>
						<div class="w-[55rem]">
							<P id="column-{column.generatedId}-{column.fieldName}">{column.displayName}</P>
							<Tooltip triggeredBy="#column-{column.generatedId}-{column.fieldName}">
								{column.description}
							</Tooltip>
						</div>
						<div class="flex items-center justify-evenly gap-2 flex-col">
							<h2>Sortable</h2>
							<Checkbox bind:checked={column.isSortable} disabled color="blue" />
						</div>
						<div class="flex items-center justify-evenly gap-2 flex-col">
							<h2>Filterable</h2>
							<Checkbox bind:checked={column.isFilterable} disabled color="blue" />
						</div>
						<div class="flex items-center justify-evenly gap-2 flex-col">
							<h2>Fixed</h2>
							<Checkbox bind:checked={column.isFixed} disabled color="blue" />
						</div>

				</Li>
			</div>
		{/each}
	</List>
</div>