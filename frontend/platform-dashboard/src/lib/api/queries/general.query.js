import { gql } from '@apollo/client/core';

export const getViews = gql`
query getViews {
	general {
		getViews {
			clientKey, color, name, description,isPredefined, isShareAble, 
			columns {
				entityName,
				columnName,
				dataType,
          fieldAlias,
          fieldPath,
				filterOperationType,
				isFilterable,
				isSortable,
				isHidden,
				isFixed,
				order
			}
		}
	}
}`;

export const GetMaintenanceMode = gql`
query GetMaintenanceMode {
	general {
		maintenanceMode 
	}
}`;
export const getCountries = gql`
query getCountries {
	general {
		getCountries {
			id, 
			countryName, 
			countryCode, 
			countryNumber
		}
	}
}`;