using System.ComponentModel.DataAnnotations;

namespace BudgetApi.Dtos.Transactions;

public record UpdateTransactionDto(
    [property: StringLength(200, MinimumLength = 1)]
    string? Description,

    [property: Range(typeof(decimal), "0.01", "1000000",
        ParseLimitsInInvariantCulture = true)]
    decimal? Amount,

    DateOnly? Date
);