using FluentValidation;

namespace Dentizone.Application.DTOs.Favorites
{
    public class FavoriteDto
    {
        public required string PostId { get; set; }
    }

    public class FavoriteDtoValidation : AbstractValidator<FavoriteDto>
    {
        public FavoriteDtoValidation()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("Post ID is required.")
                .NotNull().WithMessage("Post ID cannot be null.");
        }
    }
}