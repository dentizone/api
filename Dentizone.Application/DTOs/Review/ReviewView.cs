using Dentizone.Application.DTOs.Order;
using Dentizone.Application.DTOs.User;

namespace Dentizone.Application.DTOs.Review
{
    public class ReviewView : ReviewDto
    {

        public required UserView User { get; set; }

        public required OrderViewDto OrderViewDto
        {
            get;
            set;
        }


    }
}