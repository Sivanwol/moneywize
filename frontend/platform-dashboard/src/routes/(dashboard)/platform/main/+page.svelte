<script>
	import { InfoCircleSolid } from 'flowbite-svelte-icons';
	import { Breadcrumb, BreadcrumbItem, Button, Card } from 'flowbite-svelte';
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
<div
	class="px-[25px] text-dark dark:text-title-dark font-medium text-[17px] flex flex-wrap items-center justify-between max-sm:flex-col max-sm:h-auto border-b border-regular dark:border-white/10 mb-10">
	<Breadcrumb aria-label="Default breadcrumb example">
		<BreadcrumbItem home href="/platform/main">Home</BreadcrumbItem>
		<BreadcrumbItem>Dashboard</BreadcrumbItem>
	</Breadcrumb>
</div>
<div class="p-[25px] bg-dark rounded-10 dark:bg-box-dark ">
	<!-- Content -->
	<div class="ml-4 mr-4 w-full bg-gray-700 p-4">
		<div class="flex flex-col text-white">
			<div>Hello world</div>
			<div class="flex-grow">
				<Button color="dark" on:click={handlePropDrawerToggle} outline>Toggle Prop Draw</Button>
			</div>
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
