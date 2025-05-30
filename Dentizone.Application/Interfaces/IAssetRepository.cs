using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IAssetRepository: IBaseRepo<Asset>
    {
        Task<Asset> UpdateAsync(Asset entity);
    }
}
