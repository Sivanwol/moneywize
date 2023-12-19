namespace Domain.DTO.Filters.Fields;

public enum FilterGuidOperation {
    Equal,
    NotEqual,
    In,
    NotIn
}

public enum FilterNumberOperation {
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Between,
    NotBetween,
    In,
    NotIn
}

public enum FilterStringOperation {
    Equal,
    NotEqual,
    Contains,
    NotContains,
    StartsWith,
    EndsWith,
    In,
    NotIn
}

public enum FilterDateOperation {
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Between,
    NotBetween,
    In,
    NotIn
}

public enum FilterBooleanOperation {
    Equal,
    NotEqual
}