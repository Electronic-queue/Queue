namespace Queue.WebApi.Contracts.ReviewContracts;

public record CreateReviewDto(
    int RecordId,
    int Rating,
    string? Content
    );
