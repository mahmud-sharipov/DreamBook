using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.DreamTypes;
using DreamBook.Application.Exceptions;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using DreamBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamBook.Application.Dreams
{
    public class DreamService : IDreamService
    {
        Func<ITranslation, bool> LanguagePredicate { get; }
        IContext Context { get; }
        IMapper Mapper { get; }
        AppLanguageManager AppLanguageManager { get; }
        IUserIdentityService IdentityService { get; }
        IUser CurrentUser { get; }

        public DreamService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager, IUserIdentityService identityService)
        {
            Context = context;
            Mapper = mapper;
            AppLanguageManager = appLanguageManager;
            IdentityService = identityService;
            CurrentUser = IdentityService.GetCurrentUser() ?? throw new ArgumentNullException("Unauthenticated user cannot access this service");
            LanguagePredicate = dt => dt.LanguageGuid == AppLanguageManager.CurrentLanguage.Guid;
        }

        #region Mapping
        IEnumerable<DreamResponseModel> MapEntities(IEnumerable<Dream> entities)
        {
            List<DreamResponseModel> responce = new();
            foreach (var entity in entities)
                responce.Add(MapEntity(entity));

            return responce;
        }

        DreamResponseModel MapEntity(Dream entity)
        {
            var dto = Mapper.Map<DreamResponseModel>(entity);
            var type = entity.Type.Translations.SingleOrDefault(LanguagePredicate);
            if (type != null)
                dto.Type = Mapper.Map<DreamTypeResponseModel>(type);

            var wordGuids = entity.Words.Select(w => w.WordGuid).ToArray();
            dto.Words = Context.GetAll<WordTranslation>(wt => wordGuids.Contains(wt.WordGuid))
                .Where(LanguagePredicate)
                .Select(w => ((WordTranslation)w).Name);
            return dto;
        }
        #endregion

        private async Task<Dream> GenEntity(Guid id, bool fromToRecycleBin = false)
        {
            var entity = await Context.GetByIdAsync<Dream>(id);
            if (entity == null || entity.MovedToRecycleBin != fromToRecycleBin || entity.AuthorGuid != CurrentUser.Guid)
                throw new EntityNotFoundException(ModelsLabel.Dream, id);
            return entity;
        }

        public async Task<DreamResponseModel> GetById(Guid id)
        {
            Dream entity = await GenEntity(id);
            return MapEntity(entity);
        }

        public async Task<IEnumerable<DreamResponseModel>> GetAll()
        {
            var entities = await Context.GetAllAsync<Dream>(d => !d.MovedToRecycleBin && d.AuthorGuid == CurrentUser.Guid);
            return MapEntities(entities);
        }

        public async Task<IPagedList<DreamResponseModel>> GetPagedList(DreamPagedListRequestModel requestModel)
        {
            var query = Context.GetAll<Dream>(d => !d.MovedToRecycleBin && d.AuthorGuid == CurrentUser.Guid);
            var entities = requestModel.Filter(query, nameof(Dream.Title), nameof(Dream.CreatedAt));
            var result = new PagedList<Dream, DreamResponseModel>(entities, l => MapEntities(l), requestModel.PageNumber, requestModel.PageSize);
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<DreamShortInfoResponseModel>> GetAllShortInfo()
        {
            var entities = await Context.GetAllAsync<Dream>(d => !d.MovedToRecycleBin && d.AuthorGuid == CurrentUser.Guid);
            return Mapper.Map<IEnumerable<DreamShortInfoResponseModel>>(entities);
        }

        public async Task<IEnumerable<DreamResponseModel>> GetAllFromRecycleBin()
        {
            var entities = await Context.GetAllAsync<Dream>(d => d.MovedToRecycleBin && d.AuthorGuid == CurrentUser.Guid);
            return MapEntities(entities);
        }

        public async Task<IPagedList<DreamResponseModel>> GetAllShared(DreamPagedListRequestModel requestModel)
        {
            var user = IdentityService.GetCurrentUser();
            var query = Context.GetAll<Dream>(d => !d.MovedToRecycleBin && d.CanBeShared && d.AuthorGuid != user.Guid);
            var entities = requestModel.Filter(query, nameof(Dream.Title), nameof(Dream.CreatedAt));
            var result = new PagedList<Dream, DreamResponseModel>(entities, l => MapEntities(l), requestModel.PageNumber, requestModel.PageSize);
            return await Task.FromResult(result);
        }

        public async Task<DreamResponseModel> GetSharedById(Guid guid)
        {
            var entity = await Context.GetFirstOrDefaultAsync<Dream>(d => d.Guid == guid && !d.MovedToRecycleBin && d.CanBeShared);
            if (entity == null)
                throw new EntityNotFoundException(ModelsLabel.Dream, guid);

            return MapEntity(entity);
        }

        public async Task<DreamResponseModel> Create(CreateDreamRequestModel requestModel)
        {
            ValidateType(requestModel);

            var entity = Mapper.Map<Dream>(requestModel);
            entity.AuthorGuid = CurrentUser.Guid;

            foreach (var wordGuid in requestModel.Words)
            {
                entity.Words.Add(new DreamWord() { Word = await GetWord(wordGuid) });
            }

            await Context.AddAsync(entity);
            var result = await Context.SaveChangesAsync();

            if (result == 0)
                throw new Exception(ExceptionMessages.EntityWasNotSavedToDB.Format(typeof(Dream).Name));

            return MapEntity(entity);
        }

        public async Task Update(UpdateDreamRequestModel requestModel)
        {
            ValidateType(requestModel);
            var entity = await GenEntity(requestModel.Guid);
            Mapper.Map(requestModel, entity);
            await Context.SaveChangesAsync();
        }

        private void ValidateType(DreamRequestModel requestModel)
        {
            var type = Context.GetById<DreamType>(requestModel.TypeGuid);
            if (type == null)
                throw new EntityNotFoundException(ModelsLabel.DreamType, requestModel.TypeGuid);
        }

        private async Task<Word> GetWord(Guid guid)
        {
            var entity = await Context.GetByIdAsync<Word>(guid);
            if (entity == null)
                throw new EntityNotFoundException(ModelsLabel.Word, guid);

            return entity;
        }

        public async Task AddWords(Guid guid, IEnumerable<Guid> wordGuids)
        {
            var entity = await GenEntity(guid);
            foreach (var wordGuid in wordGuids)
            {
                if (entity.Words.Any(w => w.Guid == wordGuid)) continue;

                entity.Words.Add(new DreamWord() { Word = await GetWord(wordGuid) });
            }
            await Context.SaveChangesAsync();
        }

        public async Task RemoveWords(Guid guid, IEnumerable<Guid> wordGuids)
        {
            var entity = await GenEntity(guid);
            var wordsToDelete = entity.Words.Where(w => wordGuids.Contains(w.Guid)).ToList();
            await Context.SaveChangesAsync();
        }

        public async Task MoveToRecycleBin(Guid dreamGuid)
        {
            var entity = await GenEntity(dreamGuid);
            entity.MovedToRecycleBin = true;
            await Context.SaveChangesAsync();
        }

        public async Task RestoreFromRecycleBin(Guid dreamGuid)
        {
            var entity = await GenEntity(dreamGuid, true);
            entity.MovedToRecycleBin = false;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await Context.GetByIdAsync<Dream>(id);
            if (entity == null || entity.AuthorGuid != CurrentUser.Guid)
                throw new EntityNotFoundException(ModelsLabel.Dream, id);
            Context.Delete(entity);
            await Context.SaveChangesAsync();
        }
    }
}
