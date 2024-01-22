<script>
	import { _ } from 'svelte-i18n';
	import { Alert } from 'flowbite-svelte';
</script>
<svelte:head>
	<title>{$_('common.maintenance.title')}</title>
</svelte:head>
<div class="grid gap-6">
	<Alert color="red">
		<span class="font-medium">{$_('common.alerts.snap')}</span>
		{$_('common.maintenance.alert')}
	</Alert>
</div>