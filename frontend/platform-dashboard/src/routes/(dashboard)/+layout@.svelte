<script>
	import { Footer, FooterBrand, FooterCopyright, FooterLink, FooterLinkGroup } from 'flowbite-svelte';
	import { _ } from 'svelte-i18n';
	import ApplicationBar from '@components/common/ApplicationBar.svelte';

	function handleToggleSidebar(event) {
		conslone.log(event.detail);
	}
</script>
<style lang="postcss">
    /* Mobile devices */
    @media only screen and (max-width: 600px) {
        .responsive-grid {
            height: 70vh;
        }
    }

    /* Desktop devices */
    @media only screen and (min-width: 601px) {
        .responsive-grid {
            height: 90vh;
        }
    }
</style>

<div>
	<!-- Header -->
	<aside class="asidebar dark:bg-box-dark fixed start-0 top-0 z-[1035] h-screen overflow-hidden bg-dark xl:!w-[280px] xl:[&.collapsed]:!w-[80px] [&.collapsed]:!w-[250px] !transition-all !duration-[0.2s] ease-linear delay-[0s] !w-0 xl:[&.collapsed>.logo-wrapper]:w-[80px]"
				 id="asideBar">
		<div class="flex w-[280px] border-e border-[#edf2f9] dark:border-box-dark-up logo-wrapper items-center h-[71px]">
			<a class="block text-center" href="/platform/main">
				<div class="logo-full">
					<img alt="Logo" class="ps-[27px]" src="/img/logo/logo.svg" width="100">
				</div>
				<div class="hidden logo-fold">
					<img alt="Logo" class="p-[27px] max-w-[80px]" src="/img/logo/logo.svg" width="80">
				</div>
			</a>
		</div>
		<nav class="navBar dark:bg-box-dark static start-0 top-[70px] z-[1035] h-full overflow-y-auto bg-dark xl:!w-[280px] xl:[&.collapsed]:!w-[80px] [&.collapsed]:!w-[250px] !transition-all !duration-[0.2s] ease-linear delay-[0s] !w-0 pb-[100px] scrollbar [&.collapsed]:ps-[7px]"
				 id="navBar">
			<ul class="relative m-0 list-none px-[0.2rem] ">
				<li class="relative sub-item-wrapper">
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-apps"></i>
                  </span>
						<span class="title">Dashboard</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none group-hover:text-current">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] text-gray-600 hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up "
								 href="index.html">
								<span>Demo One</span></a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] text-gray-600 hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up "
								 href="demo-two.html">Demo
								Two</a>
						</li>
					</ul>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="change-log.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-arrow-growth"></i>
                  </span>
						<span class="title">Change log</span>
						<span
							class=" arrow-down absolute end-0 me-[0.8rem] ms-auto inline-flex items-center whitespace-nowrap rounded-full bg-info/10 px-[0.65em] pb-[0.25em] pt-[0.30em] text-center align-baseline text-[10px] font-bold leading-none text-info group-[&.active]:hidden">
                     1.0.0
                  </span>
					</a>
				</li>
				<li class="relative sub-item-wrapper ">
					<span
						class="slug dark:text-white/40 mb-[10px] mt-[30px] block px-6 text-[12px] font-medium uppercase text-light">Application</span>
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-apps"></i>
                  </span>
						<span class="title">Email</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none group-hover:text-current">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="inbox.html">Inbox</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="email.html">Read
								Email</a>
						</li>
					</ul>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="chat.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-chat"></i>
                  </span>
						<span class="title">Chat</span>
						<span
							class=" arrow-down absolute end-0 me-[0.8rem] ms-auto inline-flex items-center whitespace-nowrap rounded-full bg-success px-[0.65em] pb-[0.25em] pt-[0.30em] text-center align-baseline text-[10px] font-bold leading-none text-white group-[&.active]:hidden">
                     3
                  </span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="calendar.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-calendar-alt"></i>
                  </span>
						<span class="title">Calendar</span>
					</a>
				</li>
				<li class="relative sub-item-wrapper ">
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark   motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra  group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-exchange"></i>
                  </span>
						<span class="title">Import & Export</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none group-hover:text-current">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="import.html">Import</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="export.html">Export</a>
						</li>
					</ul>
				</li>
				<li class="relative sub-item-wrapper ">
               <span
								 class="slug dark:text-white/40 mb-[10px] mt-[30px] block px-6 text-[12px] font-medium uppercase text-light">Ui
                  Elements</span>
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark   motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-layer-group"></i>
                  </span>
						<span class="title">Ui elements</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none group-hover:text-current">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="alerts.html">Alerts</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="avatars.html">avaters</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="badges.html">badges</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="buttons.html">Buttons</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="breadcrumb.html">breadcrumb</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="cards.html">Cards</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="carousel.html">Carousel</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="checkbox.html">Checkbox</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="collapse.html">collapse</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="typography.html">Typography</a>
						</li>
					</ul>
				</li>
				<li class="relative sub-item-wrapper ">
					<span
						class="slug dark:text-white/40 mb-[10px] mt-[30px] block px-6 text-[12px] font-medium uppercase text-light">Features</span>
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark   motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-chart"></i>
                  </span>
						<span class="title">Charts</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="apex-chart.html">Apex
								chart</a>
						</li>
					</ul>
				</li>
				<li class="relative sub-item-wrapper ">
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-keyhole-circle"></i>
                  </span>
						<span class="title">Forms</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="form.html">Basics</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="form-elements.html">Elements</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="form-components.html">Components</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="form-layouts.html">Layouts</a>
						</li>
					</ul>
				</li>
				<li class="relative sub-item-wrapper ">
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark   motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-square"></i>
                  </span>
						<span class="title">Wizard</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="create-account.html">Wizard
								One</a>
						</li>
					</ul>
				</li>
				<li class="relative sub-item-wrapper ">
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark   motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-icons"></i>
                  </span>
						<span class="title">Icons</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="unicons.html">Unions</a>
						</li>
					</ul>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="editor.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-edit"></i>
                  </span>
						<span class="title">Editor</span>
					</a>
				</li>
				<li class="relative sub-item-wrapper ">
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark   motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-map"></i>
                  </span>
						<span class="title">Maps</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="vector-map.html">Vector
								Map</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="google-map.html">Google
								Map</a>
						</li>
					</ul>
				</li>
				<li class="relative">
					<span
						class="slug dark:text-white/40 mb-[10px] mt-[30px] block px-6 text-[12px] font-medium uppercase text-light">Pages</span>
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="gallery.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-images"></i>
                  </span>
						<span class="title">Gallery</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="pricing.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-bill"></i>
                  </span>
						<span class="title">Pricing</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="faq.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-question-circle"></i>
                  </span>
						<span class="title">Faq</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize  active"
						 href="blank.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-circle"></i>
                  </span>
						<span class="title">Blank page</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="knowledge-base.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-window"></i>
                  </span>
						<span class="title">Knowledge-base</span>
					</a>
				</li>
				<li class="relative sub-item-wrapper ">
					<a
						class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark   motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize ">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-images"></i>
                  </span>
						<span class="title">Blogs</span>
						<span
							class=" arrow-down text-gray-600 dark:text-subtitle-dark absolute end-0 me-[0.8rem] ms-auto text-[18px] transition-transform duration-300 ease-linear motion-reduce:transition-none">
                     <i class="uil uil-angle-down"></i>
                  </span>
					</a>
					<ul class="sub-item !visible m-0 hidden list-none p-0 [&.show]:block scrollbar overflow-y-scroll ">
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="blog.html">Blogs</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="blog-two.html">Blogs
								Two</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="blog-three.html">Blogs
								Three</a>
						</li>
						<li class="relative">
							<a class="rounded-e-[20px] hover:bg-primary/10 hover:text-primary focus:bg-primary/10 focus:text-inherit active:bg-primary/10 active:text-inherit [&.active]:text-primary dark:[&.active]:text-title-dark dark:text-subtitle-dark flex cursor-pointer items-center truncate py-[10px] pe-6 ps-[60px] text-[14px] text-body outline-none transition duration-300 ease-linear hover:outline-none focus:outline-none active:outline-none  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up capitalize "
								 href="blog-details.html">Blogs
								Details</a>
						</li>
					</ul>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="terms.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-question-circle"></i>
                  </span>
						<span class="title">Terms-conditions</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="maintenance.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-airplay"></i>
                  </span>
						<span class="title">Maintenance</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="404.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-exclamation-triangle"></i>
                  </span>
						<span class="title">404</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="coming-soon.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-sync"></i>
                  </span>
						<span class="title">Coming Soon</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="log-in.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-signin"></i>
                  </span>
						<span class="title">Log in</span>
					</a>
				</li>
				<li class="relative">
					<a class="rounded-e-[20px] hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="sign-up.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-sign-out-alt"></i>
                  </span>
						<span class="title">Sign Up</span>
					</a>
				</li>
				<li class="relative">
					<a class="hover:bg-primary/10 focus:bg-primary/10 active:bg-primary/10 dark:text-subtitle-dark flex h-12 cursor-pointer items-center gap-[16px] truncate px-6 py-4 text-[14px] font-medium text-body outline-none transition duration-300 ease-linear hover:text-primary hover:outline-none focus:text-primary focus:outline-none active:text-primary active:outline-none [&.active]:text-primary dark:[&.active]:text-title-dark  motion-reduce:transition-none dark:hover:bg-box-dark-up dark:focus:bg-box-dark-up dark:active:bg-box-dark-up group capitalize "
						 href="reset.html">
                  <span
										class="nav-icon dark:text-subtitle-dark text-[18px] text-light-extra group-hover:text-current group-[&.active]:text-current group-focus:text-current">
                     <i class="uil uil-key-skeleton"></i>
                  </span>
						<span class="title">Forget password</span>
					</a>
				</li>
			</ul>
		</nav>
	</aside>

	<div
		class="relative flex flex-col flex-1 xl:ps-[280px] xl:[&.expanded]:ps-[80px] !transition-all !duration-[0.2s] ease-linear delay-[0s] bg-normalBG dark:bg-main-dark">
		<ApplicationBar on:toggleSidebar={handleToggleSidebar} />

		<main class="bg-normalBG dark:bg-main-dark  ">
			<div class=" mx-[30px] -z-[1] min-h-[calc(100vh-195px)] mb-[30px] ssm:mt-[30px] mt-[15px]">

				<div class="grid grid-cols-12 gap-5">
					<div class="col-span-12">
						<div class="p-[25px] bg-dark rounded-10 dark:bg-box-dark">
							<slot />
						</div>
					</div>
				</div>
			</div>
			<Footer class="dark bg-background px-[25px] dark:bg-box-dark w-full" footerType="logo">
				<div class="sm:flex sm:items-center sm:justify-between">
					<FooterBrand alt="{$_('footer.logo')}" href="https://flowbite.com" name="{$_('footer.logo')}"
											 src="/img/logo/logo.svg" />
					<FooterLinkGroup
						ulClass="flex flex-wrap items-center mb-6 text-sm text-gray-500 sm:mb-0 dark:text-gray-400">
						<FooterLink href="/terms">{$_("navigation.terms")}</FooterLink>
						<FooterLink href="/policy">{$_("navigation.privacy")}</FooterLink>
					</FooterLinkGroup>
				</div>
				<hr class="my-6 border-gray-200 dark:border-gray-700 lg:my-8" />
				<FooterCopyright by="{$_('footer.credit')}" href="/platform/main" />
			</Footer>
		</main>
	</div>
</div>
	