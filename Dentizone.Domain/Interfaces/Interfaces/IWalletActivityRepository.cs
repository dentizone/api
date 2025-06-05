using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IWalletActivityRepository : IBaseRepo<WalletActivity>
    {
        Task<ICollection<WalletActivity>> GetAllBy(int page,
            Expression<Func<WalletActivity, bool>>? filter);
    }
}