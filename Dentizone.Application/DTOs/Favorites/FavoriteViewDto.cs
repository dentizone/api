using Dentizone.Application.DTOs.Post;

namespace Dentizone.Application.DTOs.Favorites
{
    public class FavoriteViewDto
    {
        public string Id { get; set; } = string.Empty;
        public PostViewDto Post { get; set; } = new();
    }
}