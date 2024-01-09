import { gql } from '@apollo/client';
import { GetList } from '@api/queries/utils/common.js';

export const GetMyProfileQuery = gql`
query GetMyProfile {
	user {
		getMyProfile {
			id,email
		}
	}
}`;

export const GetUserProfile = gql`
query getUserProfile($userId: Guid!) {
	user {
		getUserProfile(userId: $userId) {
			id,email
		}
	}
}`;

export const GetUserActivities = () => {
	const filters = [];
	const columns = [
		{ columnName: 'id' },
		{ columnName: `user { id, email }` },
		{ columnName: 'entityId' },
		{ columnName: 'entityType' },
		{ columnName: 'activity' },
		{ columnName: 'details' },
		{ columnName: 'status' },
		{ columnName: 'userAgent' },
		{ columnName: 'ipAddress' }

	];
	return GetList('user', 'getUserActivities', filters, columns);
};
	