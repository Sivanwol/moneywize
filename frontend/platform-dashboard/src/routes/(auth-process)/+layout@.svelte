<script>
	const logo = 'img/logo/logo.svg';
</script>
<style lang="postcss">
    .signin-statment {
        background: url('img/signin-bg.png') no-repeat center center/cover;

    }
</style>
<div class="container relative">
	<section>
		<div class="overflow-hidden rounded-[0.5rem]  bg-background shadow-md md:shadow-xl">
			<div class="md:hidden">
				<div class="relative h-min-[400px] flex-col bg-muted p-280 text-white">
					<div class="absolute signin-statment inset-0 bg-zinc-900" style="z-index: -1"></div>
					<div class="inset-0 flex items-center justify-center text-white">
						<div class="flex flex-col items-center">
							<img alt="Wolberg Pro LTD" class="w-32 h-32" src="{logo}" />
							<h1 class="text-3xl font-medium">Wolberg Pro LTD</h1>
						</div>
					</div>

					<div class="space-y-4"></div>
					<div>
						<slot />
					</div>
				</div>
			</div>
			<div
				class="container relative hidden h-[800px] flex-col items-center justify-center md:grid lg:max-w-none lg:grid-cols-2 lg:px-0">
				<div class=" relative hidden h-full flex-col bg-muted p-10 text-white lg:flex dark:border-r">
					<div class="signin-statment absolute inset-0 bg-zinc-900" style="z-index: -1"></div>
					<div class="relative z-20 flex items-center text-lg font-medium">
						<img alt="Wolberg Pro LTD" class="w-32 h-32" src="{logo}" />
						Wolberg Pro LTD
					</div>
					<div class="relative z-20 mt-auto">
						<blockquote class="space-y-4">
							<p class="text-2xl font-medium">
								We are a team of professionals who are ready to help you with any task.
							</p>
							<p class="text-lg font-medium">
								We are a team of professionals who are ready to help you with any task.
							</p>
							<footer class="text-sm">
								Sivan Wolberg CTO of Wolberg Pro LTD
							</footer>
						</blockquote>
					</div>
				</div>
				<div class="lg:p-8">
					<slot />
				</div>
			</div>
		</div>
	</section>
</div>