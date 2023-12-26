<script>
	import { CloseButton, Drawer } from 'flowbite-svelte';
	import { sineIn } from 'svelte/easing';

	export let enabled = false;
	let hidden = true;
	let transitionParams = {
		x: -320,
		duration: 200,
		easing: sineIn
	};

	function handlePropDrawerToggle() {
		if (!enabled) return;
		hidden = !hidden;
	}

	function handleToggle() {
		if (!enabled) return;
		hidden = !hidden;
	}
</script>
<Drawer bind:hidden={hidden} on:propDrawerToggle={handlePropDrawerToggle} {transitionParams} transitionType="fly">
	<div class="flex items-center">
		<slot name="propBarHeader" />
		<CloseButton class="mb-4 dark:text-white" on:click={() => handleToggle} />
	</div>
	<slot name="propBarContent" />
</Drawer>