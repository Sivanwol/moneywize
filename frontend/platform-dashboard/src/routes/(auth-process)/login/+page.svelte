<script>
	import { Alert } from 'flowbite-svelte';
	import { fade } from 'svelte/transition';
	import UserLogin from '@components/auth/UserLogin.svelte';
	import UserLoginMFA from '@components/auth/UserLoginMFA.svelte';
	import { _ } from 'svelte-i18n';

	const inputStyle = 'outlined';
	/** @type {any} */
	let error;
	let formState = {
		userToken: '',
		otpToken: '',
		error: ''
	};

	let visibleConfirmForm = false;

	function handleLoginMFA(event) {
		console.log(event);
		formState.error = event.detail.error;
	}

	function handleLogin(event) {
		console.log(event);
		formState.userToken = event.detail.userToken;
		formState.otpToken = event.detail.otpToken;
		formState.error = event.detail.error;
		visibleConfirmForm = true;
		
	}
</script>
<svelte:head>
	<title>Login</title>
</svelte:head>
<div class="grid gap-6">
	{#if visibleConfirmForm}
		<div in:fade={{ duration: 2000 }} out:fade={{ duration: 500 }}>
			<UserLoginMFA on:loginMfa={handleLoginMFA} userToken="{formState.userToken}" otpToken="{formState.otpToken}" />
		</div>
	{:else}
		<div in:fade={{ duration: 2000 }} out:fade={{ duration: 500 }}>
			<UserLogin on:login={handleLogin} />
		</div>
	{/if}
	{#if formState.error}
		<Alert color="red">
			<span class="font-medium">{$_('common.alerts.error')}</span>
			{formState.error}
		</Alert>
	{/if}
</div>