<script>
	import { Breadcrumb, BreadcrumbItem, Card } from 'flowbite-svelte';
	import { userStore } from '@stores/userStore';
	import { PUBLIC_VIEWS_USER_ACTIVITY } from '$env/static/public';
	import { _ } from 'svelte-i18n';
	import { getClient } from 'svelte-apollo';
	import { GetUserActivities } from '@api/queries/user.query.js';
	import { PAGE_SIZE } from '@api/queries/utils/PageSize.js';
	import { SortDirection } from '@api/queries/utils/SortDirection.js';
	import Grid from '@components/common/DataGrid/Grid.svelte';

	$: client = getClient();
	$: currentPage = 1;

	let currentViewKey = null || PUBLIC_VIEWS_USER_ACTIVITY;


	async function fetchData() {
		const res = await client.query({
			query: GetUserActivities([]),
			variables: {
				pageSize: PAGE_SIZE.TWENTY.query,
				sortField: 'CreatedAt',
				userId: $userStore.profile.id,
				viewClientId: currentViewKey,
				sortDirection: SortDirection.DESCENDING,
				page: 1
			}
		});

		console.log(res);
		if (res?.data?.user?.getUserActivities) {
			console.log(res?.data?.user?.getUserActivities);
			return res?.data?.user?.getUserActivities;
		}
		console.log('No data');
		return {
			records: [],
			totalRecords: 0,
			totalPages: 0
		};
	}

	const loadingData = async (ops) => {
		return await fetchData();
	};
</script>

<svelte:head>
	<title>{$_('userActivity.title')}</title>
</svelte:head>
<div
	class="px-[25px] text-dark dark:text-title-dark font-medium text-[17px] flex flex-wrap items-center justify-between max-sm:flex-col max-sm:h-auto border-b border-regular dark:border-white/10">
	<Breadcrumb aria-label="Default breadcrumb example">
		<BreadcrumbItem home href="/platform/main">Home</BreadcrumbItem>
		<BreadcrumbItem>User Activity</BreadcrumbItem>
	</Breadcrumb>
</div>
<div class="p-[25px] bg-dark rounded-10 dark:bg-box-dark ">
	<div class="ml-4 mr-4 w-full p-4 ">
		<Card size="xl">
			<Grid debug={true} onHandlerDataLoading={loadingData} userViewKey={currentViewKey} />
		</Card>
	</div>
</div>
