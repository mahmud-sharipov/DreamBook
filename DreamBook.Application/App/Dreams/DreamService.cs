using AutoMapper;
using DreamBook.Application.Abstraction;
using DreamBook.Application.Abstraction.PagedList;
using DreamBook.Application.Abstraction.Service;
using DreamBook.Application.DreamTypes;
using DreamBook.Application.Exceptions;
using DreamBook.Application.LanguageResources;
using DreamBook.Domain.Entities;
using DreamBook.Domain.Interfaces;
using DreamBook.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DreamBook.Application.Dreams
{
    public class DreamService : EntityService<Dream, DreamResponseModel>, IDreamService
    {
        protected Expression<Func<ITranslation, bool>> LanguagePredicate { get; }
        public AppLanguageManager AppLanguageManager { get; }

        public DreamService(IContext context, IMapper mapper, AppLanguageManager appLanguageManager) : base(context, mapper)
        {
            AppLanguageManager = appLanguageManager;
            LanguagePredicate = dt => dt.LanguageGuid == AppLanguageManager.CurrentLanguage.Guid;
        }

        IEnumerable<DreamResponseModel> MapEntities(IEnumerable<Dream> entities)
        {
            List<DreamResponseModel> responce = new();
            foreach (var entity in entities)
            {
                var dto = Mapper.Map<DreamResponseModel>(entity);
                var type = Context.GetAll<DreamTypeTranslation>(dtt => dtt.DreamTypeGuid == entity.TypeGuid).Where(LanguagePredicate).SingleOrDefault();
                if (type != null)
                    dto.Type = Mapper.Map<DreamTypeResponseModel>(type);

                var wordGuids = entity.Words.Select(w => w.WordGuid).ToArray();
                dto.Words = Context.GetAll<WordTranslation>(wt => wordGuids.Contains(wt.WordGuid)).Where(LanguagePredicate).Select(w => ((WordTranslation)w).Name).ToList();
                responce.Add(dto);
            }

            return responce;
        }

        DreamResponseModel MapEntity(Dream entity)
        {
            var dto = Mapper.Map<DreamResponseModel>(entity);
            var type = Context.GetAll<DreamTypeTranslation>(dtt => dtt.DreamTypeGuid == entity.TypeGuid).Where(LanguagePredicate).SingleOrDefault();
            if (type != null)
                dto.Type = Mapper.Map<DreamTypeResponseModel>(type);

            var wordGuids = entity.Words.Select(w => w.Guid).ToArray();
            dto.Words = Context.GetAll<WordTranslation>(wt => wordGuids.Contains(wt.WordGuid)).Where(LanguagePredicate).Select(w => ((WordTranslation)w).Name);
            return dto;
        }

        public override async Task<IEnumerable<DreamResponseModel>> GetAll()
        {
            var entities = await Context.GetAllAsync<Dream>();
            return MapEntities(entities);
        }

        public async Task<IEnumerable<DreamShortInfoResponseModel>> GetAllShortInfo()
        {
            var entities = await Context.GetAllAsync<Dream>();
            return Mapper.Map<IEnumerable<DreamShortInfoResponseModel>>(entities);
        }

        public override async Task<IPagedList<DreamResponseModel>> GetPagedList(IPagedListRequestModel<Dream> requestModel)
        {
            var entities = requestModel.Filter(Context.GetAll<Dream>(), GetDefaultSearchPropertyName(), GetDefaultPropertyNameToOrderBy());
            var result = new PagedList<Dream, DreamResponseModel>(entities, l => MapEntities(l), requestModel.PageNumber, requestModel.PageSize);
            return await Task.FromResult(result);
        }

        public override async Task<DreamResponseModel> GetById(Guid id)
        {
            var entity = await Context.GetByIdAsync<Dream>(id);
            if (entity == null)
                throw new EntityNotFoundException(ModelsLabel.Dream, id);

            return MapEntity(entity);
        }

        public async Task<DreamResponseModel> Create(CreateDreamRequestModel requestModel)
        {
            var entity = Mapper.Map<Dream>(requestModel);
            entity.Author = Context.GetAll<User>().FirstOrDefault();
            foreach (var wordGuid in requestModel.Words)
                entity.Words.Add(new DreamWord() { WordGuid = wordGuid });

            await Context.AddAsync(entity);
            var result = await Context.SaveChangesAsync();
            if (result == 0)
                throw new Exception(ExceptionMessages.EntityWasNotSavedToDB.Format(typeof(Dream).Name));

            return MapEntity(entity);
        }

        public async Task Update(UpdateDreamRequestModel requestModel)
        {
            var entity = await Context.GetByIdAsync<Dream>(requestModel.Guid);
            if (entity == null)
                throw new EntityNotFoundException(ModelsLabel.Dream, requestModel.Guid);

            Mapper.Map(requestModel, entity);

            Context.DeleteRange(entity.Words.Where(w => !requestModel.Words.Contains(w.Guid)));

            foreach (var wordGuid in requestModel.Words.Where(wg => !entity.Words.Any(w => w.Guid == wg)).ToList())
                entity.Words.Add(new DreamWord() { WordGuid = wordGuid });

            await Context.SaveChangesAsync();
        }

        protected override (bool CanBeDeleted, string Reason) CanEntityBeDeleted(Dream entity)
        {
            return (false, ExceptionMessages.EntityCanNotBeDeleted);
        }

        protected override string GetDefaultSearchPropertyName() => nameof(Dream.Title);

        protected override string GetDefaultPropertyNameToOrderBy() => nameof(Dream.CreatedAt);
    }
}
