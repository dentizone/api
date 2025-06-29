using Dentizone.Application.DTOs.Post;

namespace Dentizone.Application.DTOs.Favorites
{
    public class FavoriteViewDto
    {
        public string Id { get; set; }
        public PostViewDto Post { get; set; } = new PostViewDto();
    }
}