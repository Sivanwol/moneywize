using FluentValidation;
using Infrastructure.Enums;
using Infrastructure.GQL.Inputs.Common;
namespace Infrastructure.Validators.Common;

public class EntityPageValidator : AbstractValidator<EntityPage> {
    public EntityPageValidator() {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0");
        RuleFor(x => x.PageSize)
            .IsInEnum()
            .WithMessage("PageSize is not valid");
        RuleFor(x => x.SortByField)
            .NotEmpty()
            .WithMessage("SortByField is required")
            .MaximumLength(500)
            .WithMessage("SortByField must not exceed 500 characters");
        RuleFor(x => x.SortDirection)
            .IsInEnum()
            .WithMessage("SortDirection is not valid");
        RuleForEach(x => x.Filters)
            .SetValidator(new EntityFilterItemValidator());
    }
}

public class EntityFilterItemValidator : AbstractValidator<EntityFilterItem> {

    public EntityFilterItemValidator() {
        RuleFor(x => x.FieldName)
            .NotEmpty()
            .WithMessage("FieldName is required")
            .MaximumLength(100)
            .WithMessage("FieldName must not exceed 100 characters");
        RuleFor(x => x.OperationType)
            .IsInEnum()
            .WithMessage("OperationType is not valid");
        RuleFor(x => x.FilterOperation)
            .IsInEnum()
            .WithMessage("FilterOperation is not valid");
        When(x => x.FilterOperation == FilterOperations.Between, () => {
            RuleFor(x => x.Values)
                .NotEmpty()
                .WithMessage("Values is required")
                .Must(x => x?.Count == 2)
                .WithMessage("Values must contain 2 items");
        });
        When(x => !IsSingleValueFilter(x.FilterOperation), () => {
            RuleFor(x => x.Values)
                .NotEmpty()
                .WithMessage("Values is required")
                .Must(x => x?.Count == 1)
                .WithMessage("Values must contain 1 item");
        });

        // the single value filter not need be check as null can be happened in some case
    }

    private bool IsSingleValueFilter(FilterOperations filterOperation) {
        return filterOperation == FilterOperations.Equal ||
               filterOperation == FilterOperations.NotEqual ||
               filterOperation == FilterOperations.GreaterThan ||
               filterOperation == FilterOperations.GreaterThanOrEqual ||
               filterOperation == FilterOperations.LessThan ||
               filterOperation == FilterOperations.LessThanOrEqual ||
               filterOperation == FilterOperations.Contains ||
               filterOperation == FilterOperations.NotContains ||
               filterOperation == FilterOperations.StartsWith ||
               filterOperation == FilterOperations.EndsWith;
    }
}