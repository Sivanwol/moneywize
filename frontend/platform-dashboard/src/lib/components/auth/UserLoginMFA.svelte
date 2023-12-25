<script>
	import { createEventDispatcher, onMount } from 'svelte';
	import { FloatingLabelInput, GradientButton, Helper } from 'flowbite-svelte';
	import { createForm } from 'svelte-forms-lib';
	import * as yup from 'yup';

	const dispatch = createEventDispatcher();
	const inputStyle = 'outlined';
	export let userToken = '';
	export let otpToken = '';
	const { form, errors, state, handleSubmit } = createForm({
		validationSchema: yup.object().shape({
			code: yup.string().min(7).max(255).required()
		}),
		initialValues: {
			code: ''
		},
		onSubmit: async (values) => {
			try {
				console.log({ values, userToken, otpToken });
				dispatch('loginMfa', {
					error: null,
					tokens: 'token'
				});

			} catch (e) {
				dispatch('login', {
					error: e.message,
					tokens: null
				});
			}
		}
	});
	onMount(() => {
		console.log({ userToken, otpToken });
		if (!userToken || !otpToken) {
			dispatch('loginMfa', {
				error: 'Missing user token or otp token',
				tokens: null
			});
		}
	});
</script>

<form method="POST" on:submit|preventDefault={handleSubmit}>
	<div class="grid gap-2">
		<div class="grid gap-1">
			<FloatingLabelInput bind:value={$form.code} id="code" name="code" style="{inputStyle}" type="text">
				Code
			</FloatingLabelInput>
		</div>
		{#if $errors.code}
			<Helper color="red" name="email">
				<span class="font-medium">Oh, snapp!</span>
				{ $errors.code }
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