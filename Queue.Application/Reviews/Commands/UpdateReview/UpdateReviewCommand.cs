﻿using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Reviews.Commands.UpdateReview;

public record UpdateReviewCommand(
    int ReviewId,
    int? RecorId = null,
    int? Rating=null,
    string? Content=null
    ):IRequest<Result>;