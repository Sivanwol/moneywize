import { gql } from '@apollo/client/core';

export const GetAvailableViewColumns = gql`query getAvailableColumns(
    $entityType: EntityTypes!
) {
    general {
        getAvailableColumnsForView(
            entityType: $entityType
        ) {
            id,
            columnName,
            description,
            displayName,
            dataType,
            filterOperationType,
            isFilterable,
            isSortable,
            isHidden,
            isFixed,
            order
        }
    }
}
`;