using Dentizone.Application.DTOs.Post;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Interfaces;
using Dentizone.Domain.Interfaces.Repositories;
using Dentizone.Infrastructure.ApiClient;
using Microsoft.Extensions.DependencyInjection;

namespace Dentizone.Application.Jobs
{
    public class MointerJob(IAILayer _ailayer,IQaService _qaService,IReviewService _review, IPostService _post) : IMoitorJob
    {
        public async Task ReviewAnswerAsync(string AId, string input)
        {
            var response = await _ailayer.ScanContactToxic(input);
            if (response.Content.ContactInfo.Emails.Count != 0 && response.Content.ContactInfo.PhoneNumbers.Count != 0 && response.Content.ContactInfo.Addresses.Count != 0 && response.Content.IsInsult != false)
            {
              await _qaService.DeleteAnswerAsync(AId);

            }

        }

        public async Task ReviewPostAsync(string PId, string input)
        {
           var response = await _ailayer.ScanContactToxic(input);
            if (response.Content.ContactInfo.Emails.Count != 0 && response.Content.ContactInfo.PhoneNumbers.Count != 0 && response.Content.ContactInfo.Addresses.Count != 0 && response.Content.IsInsult != false)
            {
                
              await _post.DeletePost(PId);
            }
        }

        public async Task ReviewQuestionAsync(string QId, string input)
        {
         var response = await _ailayer.ScanContactToxic(input);
            if (response.Content.ContactInfo.Emails.Count != 0 && response.Content.ContactInfo.PhoneNumbers.Count != 0 && response.Content.ContactInfo.Addresses.Count != 0 && response.Content.IsInsult != false)
            {
                await _qaService.DeleteQuestionAsync(QId);
            }
            
        }

        public  async Task ReviewTheReviewtAsync(string RId, string input)
        {
            var response = await _ailayer.ScanContactToxic(input);
            if (response.Content.ContactInfo.Emails.Count != 0 && response.Content.ContactInfo.PhoneNumbers.Count != 0 && response.Content.ContactInfo.Addresses.Count != 0 && response.Content.IsInsult != false)
            {
                await _review.DeleteReviewAsync(RId);
            }
            

        }

        

       
    }
}