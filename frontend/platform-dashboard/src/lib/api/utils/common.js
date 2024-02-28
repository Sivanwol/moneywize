/***
 * @param {string} fieldName
 * @param {filterOperationTypes} filterOperationType
 * @param {operationTypes} operationType
 * @param {string} value
 * @param {string[]} values
 * @returns {string}
 */
export const GetListFilterItem = (fieldName, filterOperationType, operationType, value = null, values = null) => `{fieldName: ${fieldName}, filterOperation: ${filterOperationType}, operationType: ${operationType}, value: ${value}, values: ${values} }`;

/**
 *
 * @param nsName
 * @param queryName
 * @param {{fieldName: string, filterOperationType: filterOperationTypes, operationType: operationTypes, value: string|bool|number, values: string[]|bool[]|number[] }} filters
 * @param {{}} columns
 * @returns {string}
 */
export const GetList = (nsName, queryName, filters, columns) => `
query ${queryName} ($viewClientId: Guid!, $userId: Guid!, $page: Int!, $pageSize: PageSize!, $sortField: String!, $sortDirection: SortDirection!) {
	${nsName} {
		${queryName}(
			userId: $userId,
			page: {
				page:$page , 
				pageSize: $pageSize,
				sortByField: $sortField,
				sortDirection: $sortDirection,
				viewClientId: $viewClientId,
				filterCollectionOperation: AND,
				filters: [${filters.map(filter => GetListFilterItem(filter.fieldName, filter.filterOperationType, filter.operationType, filter.value, filter.values)).toString()}]
			}) {
			hasNextPage,
			hasPreviousPage,
			totalPages,
			totalRecords,
			records { 
				${columns.map(column => column.columnName).toString()}	
			}
		}
	}
}`;