import { gql } from '@apollo/client/core';
import { GetList } from '@api/queries/utils/common.js';

export const GetMyProfileQuery = gql`
query GetMyProfile {
	user {
		getMyProfile {
			id,
			email,
			firstName,
			lastName,
			phoneNumber,
			country { id, countryName },
			address,
			displayLanguage { id, name, flag, code },
			roles,
			registerCompletedAt
		}
	}
}`;

export const GetUserProfile = gql`
query getUserProfile($userId: Guid!) {
	user {
		getUserProfile(userId: $userId) {
			id,
			email,
			firstName,
			lastName,
			phoneNumber,
			country { id, countryName },
			address,
			displayLanguage { id, name, flag, code },
			roles,
			registerCompletedAt
		}
	}
}`;

/**
 * @param {{fieldName: string, filterOperationType: filterOperationTypes, operationType: operationTypes, value: string|bool|number, values: string[]|bool[]|number[] }} filters
 * @returns {DocumentNode}
 * @constructor
 */
export const GetUserActivities = (filters = []) => {
	const columns = [
		{ columnName: 'id' },
		{ columnName: `user { id, firstName, lastName` },
		{ columnName: 'entityId' },
		{ columnName: 'entityType' },
		{ columnName: 'activity' },
		{ columnName: 'details' },
		{ columnName: 'status' },
		{ columnName: 'userAgent' },
		{ columnName: 'ipAddress' },
		{ columnName: 'createdAt' }

	];
	return GetList('user', 'getUserActivities', filters, columns);
};

/**
 * @param {{fieldName: string, filterOperationType: filterOperationTypes, operationType: operationTypes, value: string|bool|number, values: string[]|bool[]|number[] }} filters
 * @returns {DocumentNode}
 * @constructor
 */
export const GetUsers = (filters = []) => {
	const columns = [
		{ columnName: 'id' },
		{ columnName: 'email' },
		{ columnName: 'firstName' },
		{ columnName: 'lastName' },
		{ columnName: 'phoneNumber' },
		{ columnName: 'country { id, countryName }' },
		{ columnName: 'address' },
		{ columnName: 'displayLanguage { id, name, flag, code }' },
		{ columnName: 'registerCompletedAt' }
	];
	return GetList('user', 'getUsers', filters, columns);
};
	