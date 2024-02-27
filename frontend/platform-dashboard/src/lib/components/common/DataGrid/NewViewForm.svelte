<script>
	import { _ } from 'svelte-i18n';
	import {
		Accordion,
		AccordionItem,
		Button,
		ButtonGroup,
		Helper,
		Input,
		Label,
		Select,
		Textarea,
		Toggle,
		Tooltip
	} from 'flowbite-svelte';
	import ColumnSelectorView from '@components/common/DataGrid/ColumnSelectorView.svelte';
	import { PlusOutline } from 'flowbite-svelte-icons';
	import { createForm } from 'svelte-forms-lib';
	import * as yup from 'yup';
	import { toast } from '@zerodevx/svelte-toast';
	import { userStore } from '@stores/userStore.js';
	import uuid from 'uuid';
	
	export let selectedView = '';
	export let onViewChange = () => {
	};
	export let onSaveColumns = () => {
	};
	export let onNewViewDraft = (basedViewClientKey) => {
	};
	export let onViewSave = (values) => {
	};
	
	let selectedViewInfoColumns = [];
	let selectedViewInfo = null;
	
	
	const onViewSelect = (event) => {
		console.log('onViewSelect', event, selectedView);
		selectedViewInfo = $userStore.userViews.find(view => view.clientKey === selectedView);
		selectedViewInfo.columns.sort((a, b) => a.order - b.order);
		let tColumns = [];
		selectedViewInfo.columns.forEach((column, index) => {
			tColumns.push({
				...column,
				generatedId: uuid.v4()
			});
		});
		selectedViewInfoColumns = tColumns;
	};
	
	const validateUserViewChange = (values) => {
		const { viewShareable, viewIsBase } = values;
		if (!viewIsBase && !viewShareable) {
			return false;
		}
		return true;
	};
	
	const { form, errors, state, handleSubmit } = createForm({
		validationSchema: yup.object().shape({
			viewSelect: yup.string().uuid().required(),
			viewName: yup.string().min(4).max(90).required(),
			viewShareable: yup.boolean().required(),
			viewIsBase: yup.boolean(),
			viewDesc: yup.string().min(4).max(255)
		}),
		initialValues: {
			viewSelect: '',
			viewName: '',
			viewShareable: false,
			viewIsBase: false,
			viewDesc: ''
		},
		onSubmit: async (values) => {
			if (!validateUserViewChange(values)) {
				toast.push('The based view is not shareable so you can\'t based new view on it.', { duration: 5000 });
				return;
			}
			console.log('values', values);
			await onViewSave(values);
		}
	});
	
	const onNewViewCreate = async () => {
		const view = views.find((v) => v.clientKey === selectedView);
		if ((view.isShareAble && !view.isPredefined) || view.isPredefined) {
			await onNewView(view.clientKey);
		} else {
			toast.push('The based view is not shareable so you can\'t based new view on it.', { duration: 5000 });
		}
	};
</script>

<form method="POST" on:submit={handleSubmit}>
	<h5 class="font-bold text-gray-900 dark:text-white">Change View</h5>
	<div class="mb-6">
		<Label class="block mb-2" for="viewSelect">Selected View</Label>
		<Select bind:value={selectedView} id="viewSelect" items={views} on:change={onViewSelect}
						placeholder="You can't select anything..." /> Or
		<Button id="create-view" on:click={onNewViewDraft}>
			Create New view
			<PlusOutline class="w-3.5 h-3.5 ms-2" />
		</Button>
		<Tooltip placement='bottom' triggeredBy="[id^='create-view']">
			You can create new view it will base the selected view if shareable. <br />
			if not it be based on the base view
		</Tooltip>
	</div>
	<div class="mb-6">
		<div class="flex items-baseline">
					<span class="flex-shrink-0 rounded-full w-2.5 h-2.5 bg-orange-600 me-1.5"
								style="background-color: '#{currentView.color}'"></span>
			<Label class="block mb-2" for="viewName">Name of the view</Label>
		</div>
		<Input id="viewName" placeholder="Name of view" readonly size="lg" value={selectedViewInfo.name} />
	</div>
	<div class="mb-6">
		<Label class="block mb-2" for="viewIsBase">Is base view</Label>
		<Toggle bind:checked={selectedViewInfo.isPredefined} color="purple" disabled id="viewIsBase" />
	</div>

	<div class="mb-6">
		<Label class="block mb-2" for="viewShareable">Is view shareable</Label>
		<Toggle bind:checked={selectedViewInfo.isShareAble} color="yellow" id="viewShareable" />
	</div>
	<div class="mb-6">
		<Label class="block mb-2" for="viewDesc">Name of the view</Label>
		<Textarea id="viewDesc" placeholder="view description" readonly rows="4"
							value={selectedViewInfo.description} />
	</div>

	<Accordion>
		<AccordionItem>
			<span slot="header">View has total of {selectedViewInfoColumns.length} columns</span>
			<ColumnSelectorView
				columns={selectedViewInfoColumns}
				saveColumns={onSaveColumns} />
		</AccordionItem>
	</Accordion>


	{#if $errors.viewSelect !== '' || $errors.viewName !== ''}
		<Helper color="red" name="email">
			<span class="font-medium">{$_('common.alerts.snap')}</span>
			You didnot select view or enter a name for the view
		</Helper>
	{/if}
	<div class="flex justify-end mt-4">
		<ButtonGroup>
			<Button color="blue" type="submit">Save</Button>
			<Button on:click={onViewChange}>Cancel</Button>
		</ButtonGroup>
	</div>
</form>