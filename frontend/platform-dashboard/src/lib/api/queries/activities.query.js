import { GetList } from '@api/utils/common.js';

/**
 * @param {{fieldName: string, filterOperationType: filterOperationTypes, operationType: operationTypes, value: string|bool|number, values: string[]|bool[]|number[] }} filters
 * @returns {DocumentNode}
 * @constructor
 */
export const GetUserActivities = (filters = []) => {
	const columns = [
		{ columnName: 'id' },
		{ columnName: `user { id }` },
		{ columnName: 'entityId' },
		{ columnName: 'entityType' },
		{ columnName: 'fieldAlias' },
		{ columnName: 'fieldPath' },
		{ columnName: 'activity' },
		{ columnName: 'details' },
		{ columnName: 'status' },
		{ columnName: 'userAgent' },
		{ columnName: 'ipAddress' },
		{ columnName: 'createdAt' }

	];
	return GetList('activitiesLogs', 'getActivities', filters, columns);
};