<script>
	import { InfoCircleSolid } from 'flowbite-svelte-icons';
	import { Button, Card } from 'flowbite-svelte';
	import PropDrawer from '@components/common/PropDrawer.svelte';
	import { createEventDispatcher } from 'svelte';

	let enabledDrawer = true;
	let hiddenDrawer = true;
	const dispatch = createEventDispatcher();

	function handlePropDrawerToggle() {
		if (enabledDrawer) {
			hiddenDrawer = !hiddenDrawer;
		}
	}
</script>

<!-- Content -->
<div class="ml-4 mr-4 w-full bg-gray-700 p-4">
	<div class="flex flex-col bg-gray-800 text-white">
		<div>Hello world</div>
		<div class="flex-grow">
			<Button color="dark" on:click={handlePropDrawerToggle} outline>Toggle Prop Draw</Button>
		</div>
	</div>
</div>
<!-- Right Bar -->
<PropDrawer enabled={enabledDrawer} hidden={hiddenDrawer}>
	<div slot="propBarContent">
		<Card>
			<div class="p-4">
				<h5 class="mb-2 text-base font-semibold text-gray-500 dark:text-gray-400">Info</h5>
				<p class="text-sm text-gray-400 dark:text-gray-500">
					You can use the <code>PropDrawer</code> component to display properties of a component. This
					component is used in the <code>PropBar</code> component.
				</p>
			</div>
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
