<script>
	import {
		maintenanceElapsed,
		maintenanceTime,
		requestMaintenanceStatus,
		resetMaintenanceStart
	} from '@stores/maintenanceTimerStorage';
	import { onMount } from 'svelte';
	import { _ } from 'svelte-i18n';
	import { PUBLIC_MAINTENANCE_URL } from '$env/static/public';
	import { goto } from '$app/navigation';
	/** @type {number} */
	$: timerIns = 0;
	$: checkerIns = 0;
	onMount(async () => {
		if ($requestMaintenanceStatus) {
			if (timerIns) {
				clearTimeout(timerIns);
			}
			timerIns = setTimeout(async () => {
				await refresh();
				resetMaintenanceStart();
			}, 1000);
			requestMaintenanceStatus.set(false);
		}

	});

	if (checkerIns) {
		clearInterval(checkerIns);
	}
	checkerIns = setInterval(async () => {
		if ($maintenanceElapsed > 60) {
			requestMaintenanceStatus.set(true);
		}
	}, 1000);
	async function refresh() {
		console.log('Maintenance is Check Status');
		const result = await fetch(PUBLIC_MAINTENANCE_URL, {
			method: 'GET'
		});
		if (result.ok) {
			const data = await result.json();
			console.log(data);
			if (!data?.Maintenance) {
				console.log('Maintenance is over');
				clearInterval(checkerIns);
				await goto('/platform/main');
			} else {
				console.log('Maintenance is still on');
				console.log('sad we still on Maintenance');
			}
		}
	}
</script>
<div class="mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[350px]">
	<div class="flex flex-col space-y-2 text-center">
		<h1 class="text-2xl font-semibold tracking-tight">{$_('common.welcomeTo')} <span
			class="text-blue-500">{$_('projectName')}</span></h1>
		<p class="text-gray-500">{$_('common.maintenance.content')}</p>
	</div>
	<slot />

	<div class="px-8 text-center text-sm text-muted-foreground">
		{$_('common.maintenance.info', {
			values: {
				'maintenanceTime': $maintenanceTime,
				'maintenanceElapsed': $maintenanceElapsed
			}
		})} <br>
	</div>
</div>