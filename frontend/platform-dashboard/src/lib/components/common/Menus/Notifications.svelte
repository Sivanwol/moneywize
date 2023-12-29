<script>
	/** @type {boolean} */
	export let openNotification = false; // boolean
	/** @type {string} */
	export let labelBy; // string
	/** @type {string} */
	export let cssClass = ''; // string
	/** @type {string} */

	/** @type {() => void } */
	export let onOpen = () => {
	};
	/** @type {() => void } */
	export let onClose = () => {
	};


	$: if (openNotification) {
		onOpen?.();
	}

	function handleDialogClose() {
		openNotification = false;
		onClose?.();
	}
</script>
<style>
    .open-notification {
        position: absolute;
        inset: 0 0 auto auto;
        margin: 0;
        transform: translate3d(0px, 42px, 0px);
        background-color: #23241f;
    }
</style>
<div aria-labelledby={labelBy} class="absolute z-[1000] ltr:float-left rtl:float-right m-0 {openNotification?'open-notification':'hidden'} min-w-max list-none overflow-hidden rounded-lg border-none bg-dark bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark dark:bg-box-dark-down  [&[data-te-dropdown-show]]:block"
		 data-te-dropdown-menu-ref>
	<div
		class="bg-dark dark:bg-box-dark shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4 px-[15px] py-[12px] md:min-w-[400px] sm:w-[300px] max-sm:w-[230px]">
		<div class={cssClass}>
			<slot name="header" />
			<slot />
		</div>
	</div>
</div>