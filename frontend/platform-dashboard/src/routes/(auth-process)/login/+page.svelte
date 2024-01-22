<script>
	import { fade } from 'svelte/transition';
	import { _ } from 'svelte-i18n';
	import UserLogin from '@components/auth/UserLogin.svelte';
	import UserLoginMFA from '@components/auth/UserLoginMFA.svelte';
	import { get } from 'lodash-es';
	import { createAuth } from '@stores/authStore.js';
	import { goto } from '$app/navigation';
	import { Alert } from 'flowbite-svelte';

	const inputStyle = 'outlined';
	/** @type {any} */
	let error;
	let formState = {
		userToken: '',
		otpToken: '',
		error: ''
	};

	let visibleConfirmForm = false;

	$: auth = createAuth();
	function handleLoginMFA(event) {
		const { tokens, userId, error } = event.detail;
		if (tokens === null) {
			formState.error = error;
			visibleConfirmForm = false;
			return;
		}
		const accessToken = get(tokens, 'AccessToken');
		const refreshToken = get(tokens, 'RefreshToken.TokenString');
		const expiresAt = get(tokens, 'RefreshToken.ExpireAt');
		auth.login(userId, accessToken, refreshToken, expiresAt);
		// navigate to home
		goto('/platform/main');
	}

	function handleLogin(event) {
		console.log(event.detail);
		formState.error = event.detail.error;
		if (formState.error) {
			return;
		}
		formState.userToken = event.detail.userToken;
		formState.otpToken = event.detail.otpToken;
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