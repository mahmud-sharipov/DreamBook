using DreamBook.Application.Abstraction;
using DreamBook.Domain.Entities;
using System.Threading.Tasks;

namespace DreamBook.Application.DreamTypes
{
    public interface IDreamTypeService : ITranslatableEntityService<DreamType, DreamTypeTranslation, DreamTypeResponseModel, DreamTypeWithTranslationsResponseModel>
    {
        Task<DreamTypeWithTranslationsResponseModel> Create(CreateDreamTypeRequestModel requestModel);
        Task Update(UpdateDreamTypeRequestModel requestModel);
    }
}
