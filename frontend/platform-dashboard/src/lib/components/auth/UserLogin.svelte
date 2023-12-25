<script>
	import { FloatingLabelInput, GradientButton, Helper } from 'flowbite-svelte';
	import { createForm } from 'svelte-forms-lib';
	import * as yup from 'yup';
	import { createEventDispatcher } from 'svelte';

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
				dispatch('login', {
					error: null,
					otpToken: 'token',
					userToken: 'token',
					otpExpire: 'expire'
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
	console.log({ $errors, $state });
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