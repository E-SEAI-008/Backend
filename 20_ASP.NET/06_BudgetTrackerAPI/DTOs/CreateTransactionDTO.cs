using System.ComponentModel.DataAnnotations;
using BudgetTrackerApi.Models;

namespace BudgetTrackerApi.Dtos;

public record CreateTransactionDto(
    [property: Required]
    [property: EnumDataType(typeof(TransactionType))]
    TransactionType? Type,

    [property: Required]
    [property: StringLength(200, MinimumLength = 1)]
    string Description,

    [property: Range(typeof(decimal), "0.01", "1000000000",
        ParseLimitsInInvariantCulture = true)]
    decimal Amount,

    [property: Required]
    DateOnly? Date
);