<style lang="postcss">
</style>
<div class="grid grid-rows-6 gap-4 text-white h-[700px]">
	<!-- Header -->
	<div class="col-span-12 row-span-1 flex items-center justify-between dark:bg-gray-800 p-4">
		<!-- Logo -->
		<div>
			<img alt="Logo" src="/img/logo/logo.svg" width="100 " />
		</div>
		<!-- Search Bar -->
		<div class="mx-4 flex-grow">
			<input class="w-full rounded border border-gray-600 bg-gray-700 p-2 placeholder-gray-400" placeholder="Search..."
						 type="text" />
		</div>
		<!-- Notifications and User Icon -->
		<div class="flex space-x-4">
			<!-- Notifications -->
			<div class="relative">
				<button class="focus:outline-none">
					<svg class="h-6 w-6 text-gray-500" fill="none" stroke="currentColor" viewBox="0 0 24 24"
							 xmlns="http://www.w3.org/2000/svg">
						<path d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158l-1.493-1.493c-.27-.27-.621-.434-1-.466V11a3 3 0 00-6 0v1.192l-1.447 1.447A2.068 2.068 0 017 17H2" stroke-linecap="round" stroke-linejoin="round"
									stroke-width="2"></path>
					</svg>
				</button>
				<span
					class="absolute right-0 top-0 inline-flex -translate-y-1/2 translate-x-1/2 transform items-center justify-center rounded-full bg-red-600 px-2 py-1 text-xs font-bold leading-none text-red-100">3</span>
			</div>
			<!-- User Icon -->
			<div>
				<img alt="User Icon" class="h-8 w-8 rounded-full" src="user_icon.png" />
			</div>
		</div>
	</div>
	<div class="col-span-12 row-span-5 flex h-full w-full">
		<!-- Sidebar -->
		<div class="w-1/4 bg-gray-700 p-4">Sidebar</div>
		<!-- Content -->
		<div class="ml-4 mr-4 w-1/2 bg-gray-700 p-4">
			<slot name="content" />
		</div>
		<!-- Right Bar -->
		<div class="mr-2 w-1/4 bg-gray-700 p-4">
			<slot name="propBar" />
		</div>
	</div>
</div>