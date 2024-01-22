<script>
	import { _ } from 'svelte-i18n';
	import { createAuth } from '@stores/authStore.js';
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';

	$: auth = createAuth();
	onMount(async () => {
		if (auth.isLoggedIn()) {
			await goto('/platform/main');
		}
	});
</script>
<div class="mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px]">
	<div class="flex flex-col space-y-2 text-center">
		<h1 class="text-2xl font-semibold tracking-tight">{$_('common.welcomeTo')}<span
			class="text-blue-500">{$_('projectName')}</span></h1>
		<p class="text-gray-500"></p>
	</div>
	<slot />
	<div class="px-8 text-center text-sm text-muted-foreground">
		{$_('login.byClicking')} <a class="text-blue-500" href="/terms">{$_('common.terms')}</a> {$_('common.and')} <a
		class="text-blue-500" href="/privacy">{$_('common.privacy')}</a>.
	</div>
</div>