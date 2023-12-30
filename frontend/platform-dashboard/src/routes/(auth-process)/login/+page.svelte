<script>
	import { Alert } from 'flowbite-svelte';
	import { fade } from 'svelte/transition';
	import UserLogin from '@components/auth/UserLogin.svelte';
	import UserLoginMFA from '@components/auth/UserLoginMFA.svelte';
	import { get } from 'lodash-es';
	import { _ } from 'svelte-i18n';
	import { createAuth } from '@stores/authStore.js';
	import { goto } from '$app/navigation';

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
			return;
		}
		const accessToken = get(tokens, 'accessToken');
		const refreshToken = get(tokens, 'refreshToken.tokenString');
		const expiresAt = get(tokens, 'refreshToken.expireAt');
		auth.login(userId, accessToken, refreshToken, expiresAt);
		// navigate to home
		goto('/platform/main');
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