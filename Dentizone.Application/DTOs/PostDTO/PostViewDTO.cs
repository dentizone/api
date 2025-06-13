using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.User;
using Dentizone.Domain.Enums;

namespace Dentizone.Application.DTOs.PostDTO
{
    public class PostViewDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public PostItemCondition Condition { get; set; }
        public PostStatus Status { get; set; }
        public UserView Seller { get; set; }
        public List<PostAssetDto> Assets { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}