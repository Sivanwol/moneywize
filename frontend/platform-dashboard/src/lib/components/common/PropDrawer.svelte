<script>
	import { CloseButton, Drawer } from 'flowbite-svelte';
	import { sineIn } from 'svelte/easing';

	export let enabled = false;
	export let hidden = true;
	let transitionParams = {
		x: -320,
		duration: 200,
		easing: sineIn
	};

	export function handlePropDrawerToggle() {
		console.log('handlePropDrawerToggle', enabled);
		if (!enabled) return;
		hidden = !hidden;
	}
</script>
<Drawer bind:hidden={hidden} placement="right" {transitionParams} transitionType="fly">
	<div class="flex items-center">
		<slot name="propBarHeader" />
		<CloseButton class="mb-4 dark:text-white" on:click={handlePropDrawerToggle} />
	</div>
	<slot name="propBarContent" />
</Drawer>