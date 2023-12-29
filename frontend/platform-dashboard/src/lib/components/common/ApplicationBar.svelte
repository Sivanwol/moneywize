<script>
	import { UilAlignCenter } from 'svelte-unicons';

	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	function handleToggleSidebar() {
		console.log('toggle sidebar');
		dispatch('toggleSidebar', {});
	}
</script>
<style>
    .header-content {
        background-color: #121212;
        z-index: 999999;
    }
</style>
<header
	class="sticky header-content top-0 flex w-full bg-dark z-[10] drop-shadow-1 dark:bg-box-dark dark:drop-shadow-none min-h-[70px]">
	<!-- Navigation -->
	<div class="flex flex-1 nav-wrap md:ps-[20px] ps-[30px] pe-[30px]">
		<!-- Header left menu -->

		<ul class="flex items-center mb-0 list-none nav-left ps-0 gap-x-[14px] gap-y-[9px]">
			<!-- Navigation Items -->
			<li>
				<button
					aria-label="Toggle SideBar"
					class="flex items-center justify-center min-w-[40px] w-[40px] h-[40px] rounded-full bg-transparent hover:bg-primary/10 hover:text-primary group transition duration-200 ease-in-out text-[#525768] dark:text-subtitle-dark max-md:dark:hover:bg-box-dark-up text-[20px] cursor-pointer xl:[&.hide]:hidden max-md:bg-normalBG max-md:dark:bg-box-dark-up"
					on:click={handleToggleSidebar}>
					<UilAlignCenter />
				</button>
			</li>
		</ul>

		<!-- Header Center menu -->

		<div class="relative ps-[30px] hexadash-top-menu hidden xl:[&.flex]:flex">
			<ul class="flex flex-wrap items-center gap-y-[15px] gap-x-[34px]">
				<li class="has-subMenu">
					<a class="" href="#">Dashboard</a>
					<ul class="subMenu">
						<li class="">
							<a href="index.html">Demo 1</a>
						</li>
						<li class="">
							<a href="demo-two.html">Demo 2</a>
						</li>
					</ul>
				</li>
				<li class="has-subMenu">
					<a class="" href="#">Apps</a>
					<ul class="subMenu">
						<li>
							<ul>
								<li class="has-subMenu-left">
									<a class="" href="#">
										<span class="nav-icon uil uil-envelope"></span>
										<span class="menu-text">Email</span>
									</a>
									<ul class="subMenu">
										<li>
											<a class="" href="inbox.html">Inbox</a>
										</li>
										<li>
											<a class="" href="email.html">Read
												Email</a>
										</li>
									</ul>
								</li>
								<li>
									<a class="" href="chat.html">
										<span class="nav-icon uil uil-message"></span>
										<span class="menu-text">Chat</span>
										<span
											class="text-white bg-success absolute -translate-y-2/4 text-[10px] font-bold min-w-[18px] h-[18px] flex items-center justify-center leading-none rounded-full end-[52px] top-2/4">3</span>
									</a>
								</li>
								<li>
									<a class="" href="calendar.html">
										<span class="nav-icon uil uil-calender"></span>
										<span class="menu-text">Calendar</span>
									</a>
								</li>
								<li class="has-subMenu-left">
									<a class="" href="#">
										<span class="nav-icon uil uil-exchange"></span>
										<span class="menu-text">Import & export</span>
									</a>
									<ul class="subMenu">
										<li>
											<a class="" href="contact-1.html">Import</a>
										</li>
										<li>
											<a class="" href="contact-2.html">Export</a>
										</li>
									</ul>
								</li>
							</ul>
						</li>
					</ul>
				</li>
				<li class="has-subMenu">
					<a class="" href="#">Features</a>
					<ul class="subMenu">
						<li class="has-subMenu-left">
							<a class="" href="#">
								<span class="nav-icon uil uil-chart"></span>
								<span class="menu-text">Charts</span>
							</a>
							<ul class="subMenu">
								<li>
									<a class="" href="charts.html">Apex Chart</a>
								</li>
							</ul>
						</li>
						<li class="has-subMenu-left">
							<a class="" href="#">
								<span class="nav-icon uil uil-document-layout-left"></span>
								<span class="menu-text">Froms</span>
							</a>
							<ul class="subMenu">
								<li>
									<a class="" href="form.html">Basics</a>
								</li>
								<li>
									<a class="" href="form-elements.html">Elements</a>
								</li>
								<li>
									<a class="" href="form-layouts.html">Layouts</a>
								</li>
								<li>
									<a class="" href="form-components.html">Components</a>
								</li>
							</ul>
						</li>
						<li class="has-subMenu-left">
							<a class="" href="#">
								<span class="nav-icon uil uil-square-shape"></span>
								<span class="menu-text">Wizards</span>
							</a>
							<ul class="subMenu">
								<li>
									<a class="" href="create-account.html">Wizard
										1</a>
								</li>
							</ul>
						</li>
						<li class="has-subMenu-left">
							<a class="" href="#">
								<span class="nav-icon uil uil-icons"></span>
								<span class="menu-text">Icons</span>
							</a>
							<ul class="subMenu">
								<li>
									<a class="" href="unicon-icons.html">Unicon
										Icons</a>
								</li>
							</ul>
						</li>
						<li>
							<a class="" href="editors.html">
								<span class="nav-icon uil uil-font"></span>
								<span class="menu-text">Editors</span>
							</a>
						</li>
						<li class="has-subMenu-left">
							<a class="" href="#">
								<span class="nav-icon uil uil-map"></span>
								<span class="menu-text">Maps</span>
							</a>
							<ul class="subMenu">
								<li>
									<a class="" href="vector-map.html">Vector
										Maps</a>
									<a class="" href="google-map.html">Google
										Maps</a>
								</li>
							</ul>
						</li>
					</ul>
				</li>
				<li class="mega-item has-subMenu">
					<a class="active" href="#">Pages</a>
					<ul class="megaMenu-wrapper megaMenu-small">
						<li>
							<ul>
								<li>
									<a class="" href="change-log.html">
										<span class="menu-text">Changelog</span>
										<span
											class="text-white bg-success absolute -translate-y-2/4 text-[10px] font-bold min-w-[18px] h-[18px] flex items-center justify-center leading-none rounded-[20px] end-[52px] top-2/4 px-[6px]">1.0.0</span>
									</a>
								</li>
								<li>
									<a class="" href="gallery.html">
										<span class="menu-text">Gallery 1</span>
									</a>
								</li>
								<li>
									<a class="" href="pricing.html">
										<span class="menu-text">Pricing</span>
									</a>
								</li>
								<li>
									<a class="" href="faq.html">
										<span class="menu-text">FAQ's</span>
									</a>
								</li>
								<li>
									<a class=" active" href="blank.html">
										<span class="menu-text">Blank Page</span>
									</a>
								</li>
								<li>
									<a class="" href="knowledge-base.html">knowledge base</a>
								</li>
								<li>
									<a class="" href="blog.html">Blog</a>
								</li>
							</ul>
						</li>
						<li>
							<ul>
								<li>
									<a class="" href="blog-two.html">Blog two</a>
								</li>
								<li>
									<a class="" href="blog-three.html">Blog three</a>
								</li>
								<li>
									<a class="" href="blog-details.html">Blog details</a>
								</li>
								<li>
									<a class="" href="terms.html">
										<span class="menu-text">Terms & Conditions</span>
									</a>
								</li>
								<li>
									<a class="" href="maintenance.html">
										<span class="menu-text">Maintenance</span>
									</a>
								</li>
								<li>
									<a class="" href="404.html">
										<span class="menu-text">404</span>
									</a>
								</li>
								<li>
									<a class="" href="coming-soon.html">
										<span class="menu-text">Coming Soon</span>
									</a>
								</li>
								<li>
									<a class="" href="log-in.html">
										<span class="menu-text">Log In</span>
									</a>
								</li>
								<li>
									<a class="" href="sign-up.html">
										<span class="menu-text">Sign Up</span>
									</a>
								</li>
								<li>
									<a class="" href="reset.html">
										<span class="menu-text">Forget password</span>
									</a>
								</li>
							</ul>
						</li>
					</ul>
				</li>
				<li class="mega-item has-subMenu">
					<a class="" href="#">Components</a>
					<ul class="megaMenu-wrapper megaMenu-wide">
						<li>
							<span class="mega-title">Components</span>
							<ul>
								<li>
									<a class="" href="alerts.html">Alert</a>
								</li>
								<li>
									<a class="" href="avatars.html">Avatar</a>
								</li>
								<li>
									<a class="" href="badges.html">Badge</a>
								</li>
							</ul>
						</li>
						<li>
							<span class="mega-title">Components</span>
							<ul>
								<li>
									<a class="" href="buttons.html">Button</a>
								</li>
								<li>
									<a class="" href="cards.html">Cards</a>
								</li>
								<li>
									<a class="" href="breadcrumbs.html">Breadcrumb</a>
								</li>
							</ul>
						</li>
						<li>
							<span class="mega-title">Components</span>
							<ul>
								<li>
									<a class="" href="carousel.html">Carousel</a>
								</li>
								<li>
									<a class="" href="checkbox.html">Checkbox</a>
								</li>
							</ul>
						</li>
						<li>
							<span class="mega-title">Components</span>
							<ul>
								<li>
									<a class="" href="collapse.html">Collapse</a>
								</li>
								<li>
									<a class="" href="comments.html">typography</a>
								</li>
							</ul>
						</li>
					</ul>
				</li>
			</ul>
		</div>

		<!-- Header right menu -->

		<div class="flex items-center ms-auto py-[15px] sm:gap-x-[25px] max-sm:gap-x-[15px] gap-y-[15px] relative">

			<div class="relative">
				<button
					class="transition-all theme-dropdown-trigger text-[20px] text-light dark:text-subtitle-dark [&.close>.uil-search]:hidden [&.close>.uil-multiply]:block"
					type="button">
					<i class="uil uil-search "></i>
					<i class="hidden uil uil-multiply "></i>
				</button>
				<input
					class="theme-dropdown absolute end-0 transition-[opacity,margin] duration [&.visible]:opacity-100 [&.visible]:block opacity-0 hidden min-w-[15rem] mt-2 dark:bg-box-dark-down p-1.5 h-[48px] px-[20px] dark:shadow-none border-1 border-regular dark:border-box-dark-up rounded-6 capitalize bg-dark text-body dark:text-title-dark placeholder:text-body placeholder:text-subtitle-dark text-ellipsis outline-none"
					id="searchInput" name="search" placeholder="search here"
					type="text">
			</div>

			<button class="flex xl:hidden items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark min-h-[40px]"
							id="author-dropdown"
							type="button">
				<i class="uil uil-ellipsis-v text-[18px]"></i>
			</button>
			<ul
				class="xl:flex hidden items-center justify-end flex-auto mb-0 list-none ps-0 sm:gap-x-[25px] max-sm:gap-x-[15px] gap-y-[15px] max-xl:absolute max-xl:z-[1000] max-xl:m-0 max-xl:rounded-lg max-xl:border-none max-xl:bg-dark max-xl:bg-clip-padding max-xl:text-left max-xl:shadow-lg max-xl:dark:bg-neutral-700 max-xl:[&.active]:flex max-xl:end-0 max-xl:px-[20px] max-sm:px-[15px] max-xl:py-[10px] max-xl:top-[70px]"
				id="right-ellipsis-trigger">
				<li>

					<div class="relative" data-te-dropdown-ref>
						<button aria-expanded="false"
										class="flex items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark relative min-h-[40px] group"
										data-te-dropdown-toggle-ref id="message"
										type="button">
							<i class="uil uil-envelope"></i>
							<span
								class="absolute flex w-1.5 h-1.5 translate-x-2/4 -translate-y-2/4 origin-[100%_0%] end-[3px] top-[8px] group-[[data-te-dropdown-show]]:hidden">
                              <span
																class="absolute inline-flex w-full h-full rounded-full opacity-75 animate-ping bg-success/20"></span>
                              <span class="relative inline-flex w-1.5 h-1.5 rounded-full bg-success"></span>
                           </span>
						</button>
						<div aria-labelledby="message"
								 class="absolute z-[1000] ltr:float-left rtl:float-right m-0 hidden min-w-max list-none overflow-hidden rounded-lg border-none bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark [&[data-te-dropdown-show]]:block"
								 data-te-dropdown-menu-ref>
							<div
								class="bg-dark dark:bg-box-dark shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4 px-[15px] py-[12px] md:min-w-[380px] sm:w-[300px] max-sm:w-[230px]">
								<h1
									class="flex items-center justify-center text-sm rounded-md bg-section dark:bg-box-dark-up h-[50px] p-[10px] text-dark dark:text-title-dark font-semibold">
                                 <span class="title-text">
                                    Messages
                                    <span
																			class="inline-flex items-center justify-center w-5 h-5 text-xs text-white rounded-full ms-[8px] bg-success dark:text-title-dark">
                                       3
                                    </span>
                                 </span>
								</h1>
								<ul class="p-0 max-h-[250px] relative overflow-x-hidden overflow-y-auto scrollbar">

									<li class="w-full">
										<div
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark transition-[0.3s] hover:text-primary hover:bg-dark dark:hover:bg-dark/[.06] hover:shadow-custom dark:shadow-none dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.20)] dark:rounded-4">
											<figure
												class="inline-flex w-full mb-0 align-top sm:gap-x-[16px] gap-y-[8px] max-sm:flex-wrap">
												<div class="text-light w-[40px] min-w-[40px] h-[40px] rounded-full relative">
															<span
																class="bg-current absolute content-[''] w-[12px] h-[12px] rounded-full border-2 border-white right-0 bottom-0"
															></span>
													<img class="object-cover w-[40px] h-[40px] bg-light-extra rounded-full"
															 src="images/messages/app-developer.png">
												</div>
												<figcaption class="w-full -mt-1 text-start">
													<h1 class="flex items-center justify-between mb-0.5 text-sm text-current font-semibold">
														App developer
														<span class="text-xs font-normal text-text-light">2.5 hrs ago</span>
													</h1>
													<div class="flex items-center gap-[30px] text-start">
                                                <span class="ps-0 min-w-[216px] text-body dark:text-subtitle-dark">Lorem ipsum
                                                   dolor amet cosec...</span>
														<span
															class="inline-flex items-center justify-center w-4 h-4 text-white rounded-full bg-light dark:text-white/[.87 text-10">3</span>
													</div>
												</figcaption>
											</figure>
										</div>
									</li>
									<li class="w-full">
										<div
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark transition-[0.3s] hover:text-primary hover:bg-dark dark:hover:bg-dark/[.06] hover:shadow-custom dark:shadow-none dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.20)] dark:rounded-4">
											<figure
												class="inline-flex w-full mb-0 align-top sm:gap-x-[16px] gap-y-[8px] max-sm:flex-wrap">
												<div class="text-success w-[40px] min-w-[40px] h-[40px] rounded-full relative">
															<span
																class="bg-current absolute content-[''] w-[12px] h-[12px] rounded-full border-2 border-white right-0 bottom-0"
															></span>
													<img class="object-cover w-[40px] h-[40px] bg-light-extra rounded-full"
															 src="images/messages/product.png">
												</div>
												<figcaption class="w-full -mt-1 text-start">
													<h1 class="flex items-center justify-between mb-0.5 text-sm text-current font-semibold">
														Product manager
														<span class="text-xs font-normal text-text-success">2.5 hrs ago</span>
													</h1>
													<div class="flex items-center gap-[30px] text-start">
                                                <span class="ps-0 min-w-[216px] text-body dark:text-subtitle-dark">Lorem ipsum
                                                   dolor amet cosec...</span>
														<span
															class="inline-flex items-center justify-center w-4 h-4 text-white rounded-full bg-success dark:text-white/[.87 text-10">3</span>
													</div>
												</figcaption>
											</figure>
										</div>
									</li>
									<li class="w-full">
										<div
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark transition-[0.3s] hover:text-primary hover:bg-dark dark:hover:bg-dark/[.06] hover:shadow-custom dark:shadow-none dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.20)] dark:rounded-4">
											<figure
												class="inline-flex w-full mb-0 align-top sm:gap-x-[16px] gap-y-[8px] max-sm:flex-wrap">
												<div class="text-success w-[40px] min-w-[40px] h-[40px] rounded-full relative">
															<span
																class="bg-current absolute content-[''] w-[12px] h-[12px] rounded-full border-2 border-white right-0 bottom-0"
															></span>
													<img class="object-cover w-[40px] h-[40px] bg-light-extra rounded-full"
															 src="images/messages/ui-ux-design.png">
												</div>
												<figcaption class="w-full -mt-1 text-start">
													<h1 class="flex items-center justify-between mb-0.5 text-sm text-current font-semibold">
														UI UX Designing
														<span class="text-xs font-normal text-text-success">6 hrs ago</span>
													</h1>
													<div class="flex items-center gap-[30px] text-start">
                                                <span class="ps-0 min-w-[216px] text-body dark:text-subtitle-dark">Lorem ipsum
                                                   dolor amet cosec...</span>
														<span
															class="inline-flex items-center justify-center w-4 h-4 text-white rounded-full bg-success dark:text-white/[.87 text-10">3</span>
													</div>
												</figcaption>
											</figure>
										</div>
									</li>
									<li class="w-full">
										<div
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark transition-[0.3s] hover:text-primary hover:bg-dark dark:hover:bg-dark/[.06] hover:shadow-custom dark:shadow-none dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.20)] dark:rounded-4">
											<figure
												class="inline-flex w-full mb-0 align-top sm:gap-x-[16px] gap-y-[8px] max-sm:flex-wrap">
												<div class="text-warning w-[40px] min-w-[40px] h-[40px] rounded-full relative">
															<span
																class="bg-current absolute content-[''] w-[12px] h-[12px] rounded-full border-2 border-white right-0 bottom-0"
															></span>
													<img class="object-cover w-[40px] h-[40px] bg-light-extra rounded-full"
															 src="images/messages/web-design-software-engineering.png">
												</div>
												<figcaption class="w-full -mt-1 text-start">
													<h1 class="flex items-center justify-between mb-0.5 text-sm text-current font-semibold">
														Software Development
														<span class="text-xs font-normal text-text-warning">3 hrs ago</span>
													</h1>
													<div class="flex items-center gap-[30px] text-start">
                                                <span class="ps-0 min-w-[216px] text-body dark:text-subtitle-dark">Lorem ipsum
                                                   dolor amet cosec...</span>
														<span
															class="inline-flex items-center justify-center w-4 h-4 text-white rounded-full bg-warning dark:text-white/[.87 text-10">3</span>
													</div>
												</figcaption>
											</figure>
										</div>
									</li>
									<li class="w-full">
										<div
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark transition-[0.3s] hover:text-primary hover:bg-dark dark:hover:bg-dark/[.06] hover:shadow-custom dark:shadow-none dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.20)] dark:rounded-4">
											<figure
												class="inline-flex w-full mb-0 align-top sm:gap-x-[16px] gap-y-[8px] max-sm:flex-wrap">
												<div class="text-success w-[40px] min-w-[40px] h-[40px] rounded-full relative">
															<span
																class="bg-current absolute content-[''] w-[12px] h-[12px] rounded-full border-2 border-white right-0 bottom-0"
															></span>
													<img class="object-cover w-[40px] h-[40px] bg-light-extra rounded-full"
															 src="images/messages/firecircle-icon-graphic-branding-graphic-design-large-white.png">
												</div>
												<figcaption class="w-full -mt-1 text-start">
													<h1 class="flex items-center justify-between mb-0.5 text-sm text-current font-semibold">
														Designing Services
														<span class="text-xs font-normal text-text-success">1.30 hrs ago</span>
													</h1>
													<div class="flex items-center gap-[30px] text-start">
                                                <span class="ps-0 min-w-[216px] text-body dark:text-subtitle-dark">Lorem ipsum
                                                   dolor amet cosec...</span>
														<span
															class="inline-flex items-center justify-center w-4 h-4 text-white rounded-full bg-success dark:text-white/[.87 text-10">2</span>
													</div>
												</figcaption>
											</figure>
										</div>
									</li>
									<li class="w-full">
										<div
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark transition-[0.3s] hover:text-primary hover:bg-dark dark:hover:bg-dark/[.06] hover:shadow-custom dark:shadow-none dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.20)] dark:rounded-4">
											<figure
												class="inline-flex w-full mb-0 align-top sm:gap-x-[16px] gap-y-[8px] max-sm:flex-wrap">
												<div class="text-warning w-[40px] min-w-[40px] h-[40px] rounded-full relative">
															<span
																class="bg-current absolute content-[''] w-[12px] h-[12px] rounded-full border-2 border-white right-0 bottom-0"
															></span>
													<img class="object-cover w-[40px] h-[40px] bg-light-extra rounded-full"
															 src="images/messages/app.png">
												</div>
												<figcaption class="w-full -mt-1 text-start">
													<h1 class="flex items-center justify-between mb-0.5 text-sm text-current font-semibold">
														App Development
														<span class="text-xs font-normal text-text-warning">2 hrs ago</span>
													</h1>
													<div class="flex items-center gap-[30px] text-start">
                                                <span class="ps-0 min-w-[216px] text-body dark:text-subtitle-dark">Lorem ipsum
                                                   dolor amet cosec...</span>
														<span
															class="inline-flex items-center justify-center w-4 h-4 text-white rounded-full bg-warning dark:text-white/[.87 text-10">1</span>
													</div>
												</figcaption>
											</figure>
										</div>
									</li>
								</ul>
								<a
									class="flex items-center justify-center text-[13px] font-medium bg-normalBG dark:bg-box-dark-up h-[50px] text-light hover:text-primary dark:hover:text-subtitle-dark dark:text-title-dark mx-[-15px] mb-[-15px] rounded-b-6"
									href="#">
									See all messages
								</a>
							</div>
						</div>
					</div>

				</li>
				<li>

					<div class="relative" data-te-dropdown-ref>
						<button aria-expanded="false"
										class="flex items-center hs-dropdown-toggle text-[23px] text-[#a0a0a0] dark:text-subtitle-dark relative min-h-[40px] group"
										data-te-dropdown-toggle-ref id="notification"
										type="button">
							<i class="uil uil-bell"></i>
							<span
								class="absolute flex w-1.5 h-1.5 translate-x-2/4 -translate-y-2/4 origin-[100%_0%] end-[3px] top-[8px] group-[[data-te-dropdown-show]]:hidden">
                              <span
																class="absolute inline-flex w-full h-full rounded-full opacity-75 animate-ping bg-warning/20"></span>
                              <span class="relative inline-flex w-1.5 h-1.5 rounded-full bg-warning"></span>
                           </span>
						</button>
						<div aria-labelledby="notification"
								 class="absolute z-[1000] ltr:float-left rtl:float-right m-0 hidden min-w-max list-none overflow-hidden rounded-lg border-none bg-dark bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark dark:bg-box-dark-down  [&[data-te-dropdown-show]]:block"
								 data-te-dropdown-menu-ref>
							<div
								class="bg-dark dark:bg-box-dark shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4 px-[15px] py-[12px] md:min-w-[400px] sm:w-[300px] max-sm:w-[230px]">
								<h1
									class="flex items-center justify-center text-sm rounded-md bg-section dark:bg-box-dark-up h-[50px] p-[10px] text-dark dark:text-title-dark font-semibold">
											<span class="title-text me-2.5">Notifications<span
												class="inline-flex items-center justify-center w-5 h-5 text-xs text-white rounded-full bg-warning ms-3 dark:text-title-dark">3</span></span>
								</h1>
								<ul class="p-0 max-h-[250px] relative overflow-x-hidden overflow-y-auto scrollbar">
									<li class="w-full sm:mb-0 mb-[10px]">
										<button
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark hover:bg-dark dark:hover:bg-box-dark-up hover:shadow-custom dark:shadow-none dark:rounded-4">
											<div class="flex items-start gap-x-[15px] gap-y-[5px] flex-wrap max-xs:flex-col">
												<div
													class="flex items-center justify-center rounded-full w-[30px] h-[30px] bg-warning/10 text-warning">
													<i class="text-current uil uil-heart text-[16px]"></i>
												</div>
												<div class="flex items-center justify-between flex-1">
													<div class="text-start">
														<h1
															class="flex items-center justify-between mb-0.5 text-[#5a5f7d] dark:text-title-dark text-sm font-normal flex-wrap">
															<span class="text-primary me-1.5 font-medium">Ibrahim Riaz</span>
															sent you a message
														</h1>
														<p class="mb-0 text-xs text-body dark:text-subtitle-dark">
															3 hours ago
														</p>
													</div>
													<div><span
														class="inline-flex items-center justify-center bg-warning w-1.5 h-1.5 ms-3 rounded-full"></span>
													</div>
												</div>
											</div>
										</button>
									</li>
									<li class="w-full sm:mb-0 mb-[10px]">
										<button
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark hover:bg-dark dark:hover:bg-box-dark-up hover:shadow-custom dark:shadow-none dark:rounded-4">
											<div class="flex items-start gap-x-[15px] gap-y-[5px] flex-wrap max-xs:flex-col">
												<div
													class="flex items-center justify-center rounded-full w-[30px] h-[30px] bg-primary/10 text-primary">
													<i class="text-current uil uil-inbox text-[16px]"></i>
												</div>
												<div class="flex items-center justify-between flex-1">
													<div class="text-start">
														<h1
															class="flex items-center justify-between mb-0.5 text-[#5a5f7d] dark:text-title-dark text-sm font-normal flex-wrap">
															<span class="text-primary me-1.5 font-medium">Shamim Ahmed</span>
															sent you a message
														</h1>
														<p class="mb-0 text-xs text-body dark:text-subtitle-dark">
															3 hours ago
														</p>
													</div>
													<div><span
														class="inline-flex items-center justify-center bg-primary w-1.5 h-1.5 ms-3 rounded-full"></span>
													</div>
												</div>
											</div>
										</button>
									</li>
									<li class="w-full sm:mb-0 mb-[10px]">
										<button
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark hover:bg-dark dark:hover:bg-box-dark-up hover:shadow-custom dark:shadow-none dark:rounded-4">
											<div class="flex items-start gap-x-[15px] gap-y-[5px] flex-wrap max-xs:flex-col">
												<div
													class="flex items-center justify-center rounded-full w-[30px] h-[30px] bg-secondary/10 text-secondary">
													<i class="text-current uil uil-upload text-[16px]"></i>
												</div>
												<div class="flex items-center justify-between flex-1">
													<div class="text-start">
														<h1
															class="flex items-center justify-between mb-0.5 text-[#5a5f7d] dark:text-title-dark text-sm font-normal flex-wrap">
															<span class="text-primary me-1.5 font-medium">Tanjim Ahmed</span>
															sent you a message
														</h1>
														<p class="mb-0 text-xs text-body dark:text-subtitle-dark">
															2 hours ago
														</p>
													</div>
													<div><span
														class="inline-flex items-center justify-center bg-secondary w-1.5 h-1.5 ms-3 rounded-full"></span>
													</div>
												</div>
											</div>
										</button>
									</li>
									<li class="w-full sm:mb-0 mb-[10px]">
										<button
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark hover:bg-dark dark:hover:bg-box-dark-up hover:shadow-custom dark:shadow-none dark:rounded-4">
											<div class="flex items-start gap-x-[15px] gap-y-[5px] flex-wrap max-xs:flex-col">
												<div
													class="flex items-center justify-center rounded-full w-[30px] h-[30px] bg-info/10 text-info">
													<i class="text-current uil uil-signout text-[16px]"></i>
												</div>
												<div class="flex items-center justify-between flex-1">
													<div class="text-start">
														<h1
															class="flex items-center justify-between mb-0.5 text-[#5a5f7d] dark:text-title-dark text-sm font-normal flex-wrap">
															<span class="text-primary me-1.5 font-medium">Masud Rana</span>
															sent you a message
														</h1>
														<p class="mb-0 text-xs text-body dark:text-subtitle-dark">
															9 hours ago
														</p>
													</div>
													<div><span
														class="inline-flex items-center justify-center bg-info w-1.5 h-1.5 ms-3 rounded-full"></span>
													</div>
												</div>
											</div>
										</button>
									</li>
									<li class="w-full sm:mb-0 mb-[10px]">
										<button
											class="group relative block w-full px-3 sm:py-3.5 max-sm:py-1.5 text-body dark:text-subtitle-dark hover:bg-dark dark:hover:bg-box-dark-up hover:shadow-custom dark:shadow-none dark:rounded-4">
											<div class="flex items-start gap-x-[15px] gap-y-[5px] flex-wrap max-xs:flex-col">
												<div
													class="flex items-center justify-center rounded-full w-[30px] h-[30px] bg-danger/10 text-danger">
													<i class="text-current uil uil-at text-[16px]"></i>
												</div>
												<div class="flex items-center justify-between flex-1">
													<div class="text-start">
														<h1
															class="flex items-center justify-between mb-0.5 text-[#5a5f7d] dark:text-title-dark text-sm font-normal flex-wrap">
															<span class="text-primary me-1.5 font-medium">Abdur Rahim</span>
															sent you a message
														</h1>
														<p class="mb-0 text-xs text-body dark:text-subtitle-dark">
															1 min ago
														</p>
													</div>
													<div><span
														class="inline-flex items-center justify-center bg-danger w-1.5 h-1.5 ms-3 rounded-full"></span>
													</div>
												</div>
											</div>
										</button>
									</li>
								</ul>
								<a
									class="flex items-center justify-center text-[13px] font-medium bg-normalBG dark:bg-box-dark-up h-[50px] text-light hover:text-primary dark:hover:text-subtitle-dark dark:text-title-dark mx-[-15px] mb-[-15px] rounded-b-6"
									href="#">See all incoming activity</a>
							</div>
						</div>
					</div>

				</li>
				<li>

					<div class="relative" data-te-dropdown-ref>
						<button aria-expanded="false"
										class="flex items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark min-h-[40px]"
										data-te-dropdown-toggle-ref id="settings"
										type="button">
							<i class="uil uil-setting"></i>
						</button>
						<div
							aria-labelledby="settings"
							class="absolute z-[1000] ltr:float-left rtl:float-right m-0 hidden min-w-max list-none overflow-hidden rounded-lg border-none bg-dark bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark dark:bg-box-dark-down  [&[data-te-dropdown-show]]:block"
							data-te-dropdown-menu-ref>
							<div
								class="lg:w-[700px] md:w-[300px] max-md:w-[230px] px-[15px] py-[15px] bg-dark dark:bg-box-dark shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4">
								<ul
									class="flex flex-wrap items-center lg:[&>li]:flex-[50%] max-lg:[&>li]:flex-[100%] max-h-[251px] relative overflow-x-hidden overflow-y-auto scrollbar scrollbar">

									<li class="w-full">
										<figure
											class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
											<img alt="audio" class="h-fit me-4" src="images/settings/audio.png">
											<figcaption>
												<h1
													class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
													Diagram Maker</h1>
												<p class="mb-0 text-body dark:text-subtitle-dark text-start">Plan user flows & test
													scenarios </p>
											</figcaption>
										</figure>
									</li>
									<li class="w-full">
										<figure
											class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
											<img alt="payment" class="h-fit me-4" src="images/settings/payment.png">
											<figcaption>
												<h1
													class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
													payments</h1>
												<p class="mb-0 text-body dark:text-subtitle-dark text-start">We handle billions of
													dollars </p>
											</figcaption>
										</figure>
									</li>
									<li class="w-full">
										<figure
											class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
											<img alt="feature" class="h-fit me-4" src="images/settings/feature.png">
											<figcaption>
												<h1
													class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
													All Features</h1>
												<p class="mb-0 text-body dark:text-subtitle-dark text-start">Introducing Increment
													subscriptions </p>
											</figcaption>
										</figure>
									</li>
									<li class="w-full">
										<figure
											class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
											<img alt="theme" class="h-fit me-4" src="images/settings/theme.png">
											<figcaption>
												<h1
													class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
													Themes</h1>
												<p class="mb-0 text-body dark:text-subtitle-dark text-start">Third party themes that are
													compatible
												</p>
											</figcaption>
										</figure>
									</li>
									<li class="w-full">
										<figure
											class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
											<img alt="payment" class="h-fit me-4" src="images/settings/payment.png">
											<figcaption>
												<h1
													class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
													payments</h1>
												<p class="mb-0 text-body dark:text-subtitle-dark text-start">We handle billions of
													dollars </p>
											</figcaption>
										</figure>
									</li>
									<li class="w-full">
										<figure
											class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
											<img alt="design" class="h-fit me-4" src="images/settings/design.png">
											<figcaption>
												<h1
													class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
													Design Mockups</h1>
												<p class="mb-0 text-body dark:text-subtitle-dark text-start">Share planning visuals with
													clients </p>
											</figcaption>
										</figure>
									</li>
									<li class="w-full">
										<figure
											class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
											<img alt="file" class="h-fit me-4" src="images/settings/file.png">
											<figcaption>
												<h1
													class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
													Content Planner</h1>
												<p class="mb-0 text-body dark:text-subtitle-dark text-start">Centralize content gathering
													and editing
												</p>
											</figcaption>
										</figure>
									</li>
									<li class="w-full">
										<figure
											class="flex items-start px-4 py-3 mb-0 hover:shadow-action dark:hover:shadow-[0_5px_30px_rgba(1,4,19,.60)]">
											<img alt="audio" class="h-fit me-4" src="images/settings/audio.png">
											<figcaption>
												<h1
													class="mb-0.5 -mt-1 text-[15px] font-medium capitalize text-dark dark:text-title-dark text-start">
													Diagram Maker</h1>
												<p class="mb-0 text-body dark:text-subtitle-dark text-start">Plan user flows & test
													scenarios </p>
											</figcaption>
										</figure>
									</li>
								</ul>
							</div>
						</div>
					</div>

				</li>
				<li>

					<div class="relative" data-te-dropdown-ref>
						<button aria-expanded="false" class="flex items-center" data-te-dropdown-toggle-ref id="flags"
										type="button">
							<img alt="flags" class="min-w-[20px] min-h-[20px]" src="images/flags/english.png">
						</button>
						<div
							aria-labelledby="flags"
							class="absolute z-[1000] ltr:float-left rtl:float-right m-0 hidden min-w-max list-none overflow-hidden rounded-lg border-none bg-dark bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark dark:bg-box-dark-down  [&[data-te-dropdown-show]]:block"
							data-te-dropdown-menu-ref>
							<ul
								class="block bg-dark dark:bg-box-dark-down w-[100px] shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4 overflow-hidden">
								<li class="bg-dark dark:bg-box-dark-down hover:bg-primary/10 dark:hover:bg-primary/10">
									<button class="flex items-center px-3 py-1.5 text-sm text-dark dark:text-subtitle-dark">
										<img alt="english" class="w-3.5 h-3.5 me-2" src="images/flags/english.png">
										<span>English</span>
									</button>
								</li>
								<li class="bg-dark dark:bg-box-dark-down hover:bg-primary/10 dark:hover:bg-primary/10">
									<button class="flex items-center px-3 py-1.5 text-sm text-dark dark:text-subtitle-dark">
										<img alt="spanish" class="w-3.5 h-3.5 me-2" src="images/flags/spanish.png">
										<span>Spanish</span>
									</button>
								</li>
								<li class="bg-dark dark:bg-box-dark-down hover:bg-primary/10 dark:hover:bg-primary/10">
									<button class="flex items-center px-3 py-1.5 text-sm text-dark dark:text-subtitle-dark">
										<img alt="arabic" class="w-3.5 h-3.5 me-2" src="images/flags/arabic.png">
										<span>Arabic</span>
									</button>
								</li>
							</ul>
						</div>
					</div>

				</li>
				<li>

					<div class="relative" data-te-dropdown-ref>
						<button aria-expanded="false"
										class="flex items-center me-1.5 text-body dark:text-subtitle-dark text-sm font-medium capitalize rounded-full md:me-0 group whitespace-nowrap"
										data-te-dropdown-toggle-ref id="author-dropdown"
										type="button">
							<span class="sr-only">Open user menu</span>
							<img alt="user photo" class="min-w-[32px] w-8 h-8 rounded-full xl:me-2"
									 src="images/avatars/thumbs.png">
							<span class="hidden xl:block">Shamim Ahmed</span>
							<i class="uil uil-angle-down text-light dark:text-subtitle-dark text-[18px] hidden xl:block"></i>
						</button>

						<!-- Dropdown menu -->
						<div
							aria-labelledby="author-dropdown"
							class="absolute z-[1000] ltr:float-left rtl:float-right m-0 hidden min-w-max list-none overflow-hidden rounded-lg border-none bg-dark bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark dark:bg-box-dark-down  [&[data-te-dropdown-show]]:block"
							data-te-dropdown-menu-ref>
							<div
								class="min-w-[310px] max-sm:min-w-full pt-4 px-[15px] py-[12px] bg-dark dark:bg-box-dark shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4">
								<figure
									class="flex items-center text-sm rounded-[8px] bg-section dark:bg-box-dark-up py-[20px] px-[25px] mb-[12px] gap-[15px]">
									<img alt="user" class="w-8 h-8 rounded-full bg-regular" src="images/avatars/thumbs.png">
									<figcaption>
										<div class="text-dark dark:text-title-dark mb-0.5 text-sm">Shamim Ahmed</div>
										<div class="mb-0 text-xs text-body dark:text-subtitle-dark">Software Engineer</div>
									</figcaption>
								</figure>
								<ul class="m-0 pb-[10px] overflow-x-hidden overflow-y-auto scrollbar bg-transparent max-h-[230px]">

									<li class="w-full">
										<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
											<button
												class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
												<i class="text-[16px] uil uil-user"></i>
												Profile
											</button>
										</div>
									</li>
									<li class="w-full">
										<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
											<button
												class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
												<i class="text-[16px] uil uil-setting"></i>
												Settings
											</button>
										</div>
									</li>
									<li class="w-full">
										<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
											<button
												class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
												<i class="text-[16px] uil uil-key-skeleton"></i>
												Billing
											</button>
										</div>
									</li>
									<li class="w-full">
										<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
											<button
												class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
												<i class="text-[16px] uil uil-users-alt"></i>
												Activity
											</button>
										</div>
									</li>
									<li class="w-full">
										<div class="p-0 dark:hover:text-white hover:bg-primary/10 dark:hover:bg-box-dark-up rounded-4">
											<button
												class="inline-flex items-center text-light dark:text-subtitle-dark hover:text-primary hover:ps-6 w-full px-2.5 py-3 text-sm transition-all ease-in-out delay-150 gap-[10px]">
												<i class="text-[16px] uil uil-bell"></i>
												Help
											</button>
										</div>
									</li>
								</ul>
								<a
									class="flex items-center justify-center text-sm font-medium bg-normalBG dark:bg-box-dark-up h-[50px] text-light hover:text-primary dark:hover:text-subtitle-dark dark:text-title-dark mx-[-15px] mb-[-15px] rounded-b-6 gap-[6px]"
									href="log-in.html">
									<i class="uil uil-sign-out-alt"></i> Sign Out</a>
							</div>
						</div>
					</div>


				</li>
			</ul>
		</div>

	</div>
	<!-- End: Navigation -->
</header>