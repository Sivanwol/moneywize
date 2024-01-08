<script>
	import { FloatingLabelInput, GradientButton, Helper, Spinner } from 'flowbite-svelte';
	import { createForm } from 'svelte-forms-lib';
	import * as yup from 'yup';
	import { createEventDispatcher } from 'svelte';
	import { _ } from 'svelte-i18n';
	import { requestLogin } from '@api/auth.js';

	let loaded = false;
	const dispatch = createEventDispatcher();
	const inputStyle = 'outlined';
	const { form, errors, state, handleSubmit } = createForm({
		validationSchema: yup.object().shape({
			email: yup.string().email().required(),
			password: yup.string().min(6).max(50).required()
		}),
		initialValues: {
			email: '',
			password: ''
		},
		onSubmit: async (values) => {
			try {
				const response = await requestLogin(values.email, values.password);
				loaded = true;
				if (!response.isValidRequest()) {
					dispatch('login', {
						error: response.errors[0],
						otpToken: null,
						userToken: null,
						otpExpire: null
					});
					loaded = false;
					return;
				}
				const { OTPToken, UserToken, OTPExpired } = response.data;
				dispatch('login', {
					error: null,
					otpToken: OTPToken,
					userToken: UserToken,
					otpExpire: OTPExpired
				});

			} catch (e) {
				dispatch('login', {
					error: e.message,
					otpToken: null,
					userToken: null,
					otpExpire: null
				});
			}
			loaded = false;
		}
	});
</script>
<form method="POST" on:submit={handleSubmit}>
	<div class="grid gap-2">
		<div class="grid gap-1">
			<FloatingLabelInput bind:value={$form.email} name="email" style="{inputStyle}" type="text">
				{$_('login.email')}
			</FloatingLabelInput>
		</div>
		<div class="grid gap-1">
			<FloatingLabelInput bind:value={$form.password} name="password" style="{inputStyle}"
													type="password">
				{$_('login.password')}
			</FloatingLabelInput>
		</div>
		{#if $errors.email !== '' || $errors.password !== ''}
			<Helper color="red" name="email">
				<span class="font-medium">{$_('common.alerts.snap')}</span>
				{$_('login.error')}
			</Helper>
		{/if}

		{#if !loaded}
			<GradientButton color="blue" shadow type="submit">{$_('login.submit')}</GradientButton>
		{:else}
			<GradientButton color="blue" shadow type="submit" disabled>
				<Spinner class="me-3" size="4" color="white" />
				{$_('common.loading')}
			</GradientButton>
		{/if}
		<div class="grid gap-1">
			<p class="text-center text-sm text-gray-600">
				{$_('login.forget')} <a class="underline" href="/forgot-password">{$_('login.forget_here')}</a>
			</p>
		</div>
	</div>
</form>