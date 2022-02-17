using CoreLayer.Dtos;
using CORETest.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
    public interface IOwnerService
    {
        Task<OperationResault> Insert(InsertOwnerDto insertOwnerDto);
        Task<OwnerDto> GetActiveProfile();
        Task<List<OwnerDto>> GetallProfile();
    }
}
