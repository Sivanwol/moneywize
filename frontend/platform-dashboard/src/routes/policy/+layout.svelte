<style lang="postcss">

</style>
<div class="container relative">
	<section>
		<div class=" rounded-[0.5rem] bg-background shadow-md md:shadow-xl">
			<div class="md:hidden">
				<div class="relative h-min-[400px] flex-col bg-muted p-280 text-white">

					<div class="inset-0 flex items-center justify-center text-white">
						<div class="flex flex-col items-center">
							<img alt="Wolberg Pro LTD" class="w-32 h-32" src="img/logo/logo.svg" />
							<h1 class="text-3xl font-medium">Wolberg Pro LTD</h1>
						</div>
					</div>

					<div class="space-y-4"></div>
					<slot />
				</div>
			</div>

			<div
				class="container relative h-[750px] flex-col items-center justify-center md:grid lg:max-w-none lg:grid-cols-2 lg:px-0">
				<div class=" relative hidden h-full flex-col bg-muted p-10 text-white lg:flex dark:border-r">
					<div class="absolute inset-0 bg-zinc-900" style="z-index: -1">
						<img alt="Wolberg Pro LTD" class="w-full object-cover" src="img/term.png" width="200" />
					</div>
					<div class="relative z-20 flex items-center text-lg font-medium">
						<img alt="Wolberg Pro LTD" class="w-32 h-32" src="img/logo/logo.svg" />
						Wolberg Pro LTD
					</div>
				</div>
				<div class="lg:p-8">
					<slot />
				</div>
			</div>
		</div>
	</section>
</div>