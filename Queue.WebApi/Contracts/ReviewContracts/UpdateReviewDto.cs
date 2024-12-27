namespace Queue.WebApi.Contracts.ReviewContracts;

public record UpdateReviewDto(int ReviewId,
    int? RecordId,
    int? Rating,
    string? Content
    );
