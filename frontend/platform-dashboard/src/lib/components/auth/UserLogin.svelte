<script>
	import { FloatingLabelInput, GradientButton, Helper } from 'flowbite-svelte';
	import { createForm } from 'svelte-forms-lib';
	import * as yup from 'yup';
	import { createEventDispatcher } from 'svelte';
	import { requestLogin } from '@api/auth.js';

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
				console.log(values);
				const response = await requestLogin(values.email, values.password);

				if (response.status !== 200) {
					dispatch('login', {
						error: 'Invalid email or password',
						otpToken: null,
						userToken: null,
						otpExpire: null
					});
					return;
				}
				if (!response.data.status) {
					dispatch('login', {
						error: response.data.message,
						otpToken: null,
						userToken: null,
						otpExpire: null
					});
					return;
				}
				const { otpToken, userToken, otpExpire } = response.data.data;
				dispatch('login', {
					error: null,
					otpToken: otpToken,
					userToken: userToken,
					otpExpire: otpExpire
				});

			} catch (e) {
				dispatch('login', {
					error: e.message,
					otpToken: null,
					userToken: null,
					otpExpire: null
				});
			}
		}
	});
</script>
<form method="POST" on:submit={handleSubmit}>
	<div class="grid gap-2">
		<div class="grid gap-1">
			<FloatingLabelInput bind:value={$form.email} id="email" name="email" style="{inputStyle}" type="text">
				E-mail
			</FloatingLabelInput>
		</div>
		<div class="grid gap-1">
			<FloatingLabelInput bind:value={$form.password} id="password" name="password" style="{inputStyle}"
													type="password">
				Password
			</FloatingLabelInput>
		</div>
		{#if $errors.email !== '' || $errors.password !== ''}
			<Helper color="red" name="email">
				<span class="font-medium">Oh, snapp!</span>
				Invalid email or password.
			</Helper>
		{/if}
		<GradientButton color="blue" shadow type="submit">Login</GradientButton>
		<div class="grid gap-1">
			<p class="text-center text-sm text-gray-600">
				Forgot your password? <a class="underline" href="/forgot-password">Reset it here</a>
			</p>
		</div>
	</div>
</form>