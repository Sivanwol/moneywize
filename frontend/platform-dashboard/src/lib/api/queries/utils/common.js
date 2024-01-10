import { gql } from '@apollo/client/core';

/***
 * @param {string} fieldName
 * @param {filterOperationTypes} filterOperationType
 * @param {operationTypes} operationType
 * @param {string} value
 * @param {string[]} values
 * @returns {string}
 */
export const GetListFilterItem = (fieldName, filterOperationType, operationType, value = null, values = null) => gql`{fieldName: ${fieldName}, filterOperation: ${filterOperationType}, operationType: ${operationType}, value: ${value}, values: ${values} }`;

/**
 *
 * @param {string} $nsName
 * @param {string} $queryName
 * @param {{fieldName: string, filterOperationType: filterOperationTypes, operationType: operationTypes, value: string|bool|number, values: string[]|bool[]|number[] }} filters
 * @param {{}} columns
 * @returns {DocumentNode}
 */
export const GetList = ($nsName, $queryName, filters, columns) => gql`
query ${queryName}($viewClientId: Guid!, $userId: Guid!, $page: Int!, $pageSize: PageSize!, $sortField: String!, $sortDirection: SortDirection!) {
	${nsName} {
		${queryName}(
			userId: $userId,
			page: {
				page:$page , 
				pageSize: $pageSize,
				sortField: $sortField,
				sortDirection: $sortDirection
				viewClientId: $viewClientId,
				filterCollectionOperation: AND,
				filters: [
					${filters.map(filter => GetListFilterItem(filter.fieldName, filter.filterOperationType, filter.operationType, filter.value, filter.values))}
				]
			}) {
			viewDefinition {
				name,
				description,
				isPredefined,
				isShareAble
				columns {
					entityName,
					columnName,
					dataType,
					filterOperationType,
					isFilterable,
					isSortable,
					isHidden,
					isFixed,
					order
				}
			},
			hasNextPage,
			hasPreviousPage,
			totalPages,
			totalRecords,
			records { 
				${columns.map(column => column.columnName).reduce((accumulator, currentValue) => accumulator + currentValue + ', ', '')}	
		}
	}
}`;