using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamBook.Application.Dreams
{
    public interface IDreamService : IEntityService<Dream, DreamResponseModel>
    {
        Task<IEnumerable<DreamShortInfoResponseModel>> GetAllShortInfo();
        Task<DreamResponseModel> Create(CreateDreamRequestModel requestModel);
        Task Update(UpdateDreamRequestModel requestModel);
    }
}
