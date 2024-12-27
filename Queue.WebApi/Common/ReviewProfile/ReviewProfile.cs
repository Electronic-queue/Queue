using AutoMapper;
using Queue.Application.Reviews.Commands.CreateReview;
using Queue.Application.Reviews.Commands.UpdateReview;
using Queue.WebApi.Contracts.ReviewContracts;

namespace Queue.WebApi.Common.ReviewProfile;

public class ReviewProfile:Profile
{
    public ReviewProfile()
    {
        CreateMap<CreateReviewDto, CreateReviewCommand>()
             .ForMember(x => x.RecordId,
           opt => opt.MapFrom(y => y.RecordId))
              .ForMember(x => x.Rating,
           opt => opt.MapFrom(y => y.Rating))
               .ForMember(x => x.Content,
           opt => opt.MapFrom(y => y.Content));
        CreateMap<UpdateReviewDto, UpdateReviewCommand>()
             .ForMember(x => x.ReviewId,
           opt => opt.MapFrom(y => y.ReviewId))
              .ForMember(x => x.RecorId,
           opt => opt.MapFrom(y => y.RecordId))
               .ForMember(x => x.Rating,
           opt => opt.MapFrom(y => y.Rating))
                .ForMember(x => x.Content,
           opt => opt.MapFrom(y => y.Content));
    }
}
