using System.Collections.Generic;

namespace DreamBook.Application.Dreams
{
    public interface IDreamService
    {
        Task<DreamResponseModel> GetById(Guid id);
        Task<IEnumerable<DreamResponseModel>> GetAll();
        Task<IEnumerable<DreamShortInfoResponseModel>> GetAllShortInfo();
        Task<IEnumerable<DreamResponseModel>> GetAllFromRecycleBin();
        Task<IPagedList<DreamResponseModel>> GetAllShared(DreamPagedListRequestModel requestModel);
        Task<DreamResponseModel> GetSharedById(Guid guid);
        Task<IPagedList<DreamResponseModel>> GetPagedList(DreamPagedListRequestModel requestModel);

        Task<DreamResponseModel> Create(CreateDreamRequestModel requestModel);
        Task Update(UpdateDreamRequestModel requestModel);
        Task AddWords(Guid guid, IEnumerable<Guid> wordGuids);
        Task RemoveWords(Guid guid, IEnumerable<Guid> wordGuids);
        Task MoveToRecycleBin(Guid dreamGuid);
        Task RestoreFromRecycleBin(Guid dreamGuid);
        
        Task Delete(Guid id);
    }
}
