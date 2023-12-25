<script>
	import { FloatingLabelInput, GradientButton } from 'flowbite-svelte';

	const inputStyle = 'outlined';
</script>
<div class="grid gap-6">
	<form>
		<div class="grid gap-2">
			<div class="grid gap-1">
				<FloatingLabelInput id="floating_outlined" name="floating_outlined" style="{inputStyle}" type="text">
					E-mail
				</FloatingLabelInput>
			</div>
			<div class="grid gap-1">
				<FloatingLabelInput id="floating_outlined" name="floating_outlined" style="{inputStyle}" type="text">
					Password
				</FloatingLabelInput>
			</div>
			<GradientButton color="blue" shadow>Login</GradientButton>
		</div>
	</form>

</div>