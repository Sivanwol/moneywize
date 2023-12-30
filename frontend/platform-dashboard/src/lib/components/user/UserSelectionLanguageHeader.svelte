<script>
	import Notifications from '@components/common/Menus/Notifications.svelte';
	import { createEventDispatcher, onMount } from 'svelte';
	import { Language } from '@models/Language.js';

	const dispatch = createEventDispatcher();

	export let openWindow = false;
	/** @type { Language } */
	export let selectLanguage;
	/** @type { Language[] } */
	export let languages = [];

	function handleToggleMessageWindow() {
		openWindow = !openWindow;
		dispatch('statusLanguageWindow', {
			status: openWindow
		});
	}

	onMount(() => {
		dispatch('statusLanguageWindow', {
			status: openWindow
		});
	});

	function onSelectLanguage(id) {
		openWindow = false;
		dispatch('selectedLanguage', {
			selected: id
		});
	}
</script>
<div class="relative" data-te-dropdown-ref>
	<button aria-expanded="{openWindow}"
					class="flex items-center text-[22px] text-[#a0a0a0] dark:text-subtitle-dark relative min-h-[40px] group"
					data-te-dropdown-show={openWindow || null}
					data-te-dropdown-toggle-ref id="flags"
					on:click={handleToggleMessageWindow}
					on:keydown={(evt) => evt.key === 'Enter' && handleToggleMessageWindow()}
					type="button">
		<img alt="flags" class="min-w-[20px] min-h-[20px]" src="{selectLanguage.flag}" />
	</button>
	<Notifications
		continerLimit=""
		labelBy="flags"
		openNotification="{openWindow}"
		overrideClasses="absolute z-[1000] ltr:float-left rtl:float-right m-0 min-w-max list-none overflow-hidden rounded-lg border-none bg-dark bg-clip-padding text-left text-base shadow-lg dark:shadow-boxLargeDark dark:bg-box-dark-down  [&[data-te-dropdown-show]]:block">
		<ul
			class="block bg-dark dark:bg-box-dark-down w-[100px] shadow-[0_2px_8px_rgba(0,0,0,.15)] dark:shadow-[0_5px_30px_rgba(1,4,19,.60)] rounded-4 overflow-hidden">
			{#each languages as language}
				{#if language.id === selectLanguage.id}
					<li class="bg-primary/10 dark:bg-primary/10">
						<button class="flex items-center px-3 py-1.5 text-sm text-primary dark:text-primary-dark">
							<img alt="{language.name}" class="w-3.5 h-3.5 me-2" src="{language.flag}">
							<span>{language.name}</span>
						</button>
					</li>
				{:else}
					<li class="bg-dark dark:bg-box-dark-down hover:bg-primary/10 dark:hover:bg-primary/10"
							on:click={() => onSelectLanguage(language.id)} tabindex="0" role="menuitem"
							on:keydown={() => onSelectLanguage(language.id)}>
						<button class="flex items-center px-3 py-1.5 text-sm text-dark dark:text-subtitle-dark">
							<img alt="{language.name}" class="w-3.5 h-3.5 me-2" src="{language.flag}">
							<span>{language.name}</span>
						</button>
					</li>
				{/if}
			{/each}
		</ul>
	</Notifications>
</div>