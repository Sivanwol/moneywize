<script>
	import { createEventDispatcher, onMount } from 'svelte';
	import { FloatingLabelInput, GradientButton, Helper, Spinner } from 'flowbite-svelte';
	import { createForm } from 'svelte-forms-lib';
	import * as yup from 'yup';
	import { _ } from 'svelte-i18n';
	import { requestLoginConfirm } from '@api/auth.js';

	let loaded = false;
	const dispatch = createEventDispatcher();
	const inputStyle = 'outlined';
	export let userToken = '';
	export let otpToken = '';
	let errorOnConfirm = false;
	const { form, errors, state, handleSubmit } = createForm({
		validationSchema: yup.object().shape({
			code: yup.string().min(7).max(255).required()
		}),
		initialValues: {
			code: ''
		},
		onSubmit: async (values) => {
			try {
				errorOnConfirm = false;
				const response = await requestLoginConfirm(userToken, otpToken, values.code);
				if (!response.isValidRequest()) {
					errorOnConfirm = true;

					dispatch('loginMfa', {
						error: response.errors[0],
						tokens: null,
						userId: null
					});
					return;
				}
				const { tokens, userId } = response.data;
				dispatch('loginMfa', {
					error: null,
					tokens,
					userId
				});

			} catch (e) {
				errorOnConfirm = true;
			}
		}
	});
	onMount(() => {
		console.log({ userToken, otpToken });
		if (!userToken || userToken === '' || !otpToken || otpToken === '') {
			dispatch('loginMfa', {
				error: $_('login.error_confirm'),
				tokens: null
			});
		}
	});
</script>

<form method="POST" on:submit|preventDefault={handleSubmit}>
	<div class="grid gap-2">
		<div class="grid gap-1">
			<FloatingLabelInput bind:value={$form.code} id="code" name="code" style="{inputStyle}" type="text">
				{$_('login.code')}
			</FloatingLabelInput>
		</div>
		{#if errorOnConfirm}
			<Helper color="red" name="code">
				<span class="font-medium">{$_('common.alerts.snap')}</span>
				{ $_('login.error_confirm') }
			</Helper>
		{/if}
		{#if !loaded}
			<GradientButton color="blue" shadow type="submit">{$_('login.submit_confirm')}</GradientButton>
		{:else}
			<GradientButton color="blue" shadow type="submit" disabled>
				<Spinner class="me-3" size="4" color="white" />
				{$_('common.loading')}
			</GradientButton>
		{/if}
	</div>
</form>