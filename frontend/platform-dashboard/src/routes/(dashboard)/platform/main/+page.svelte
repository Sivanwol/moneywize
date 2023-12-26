<script>
	import { InfoCircleSolid } from 'flowbite-svelte-icons';
	import { Card } from 'flowbite-svelte';
	import PropDrawer from '@components/common/PropDrawer.svelte';
</script>

<!-- Content -->
<div class="ml-4 mr-4 w-1/2 bg-gray-700 p-4">
	<div class="h-screen flex overflow-hidden bg-gray-800 text-white">
		Hello world
	</div>
</div>
<!-- Right Bar -->
<div class="mr-2 w-1/4 bg-gray-700 p-4">
	<PropDrawer enabled={true}>
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
</div>