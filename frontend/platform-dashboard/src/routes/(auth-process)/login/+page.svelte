<script>
	import { FloatingLabelInput, GradientButton } from 'flowbite-svelte';
	import { invalidateAll } from '$app/navigation';
	import { applyAction, deserialize } from '$app/forms';

	const inputStyle = 'outlined';

	/** @type {import('./$types').ActionData} */
	export let form;

	/** @type {any} */
	let error;

	/** @param {{ currentTarget: EventTarget & HTMLFormElement}} event */
	async function handleSubmit(event) {
		const data = new FormData(event.currentTarget);

		const response = await fetch(event.currentTarget.action, {
			method: 'POST',
			body: data
		});

		/** @type {import('@sveltejs/kit').ActionResult} */
		const result = deserialize(await response.text());

		if (result.type === 'success') {
			// rerun all `load` functions, following the successful update
			await invalidateAll();
		}

		applyAction(result);
	}
</script>
<div class="grid gap-6">
	<form method="POST" on:submit|preventDefault={handleSubmit}>
		<div class="grid gap-2">
			<div class="grid gap-1">
				<FloatingLabelInput id="email" name="email" style="{inputStyle}" type="text">
					E-mail
				</FloatingLabelInput>
			</div>
			<div class="grid gap-1">
				<FloatingLabelInput id="password" name="password" style="{inputStyle}" type="text">
					Password
				</FloatingLabelInput>
			</div>
			<GradientButton color="blue" shadow>Login</GradientButton>
		</div>
	</form>

</div>