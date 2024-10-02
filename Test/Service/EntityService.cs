using Test.Service;
using Web.Api.Error;
using Web.Api.Repository;

namespace Web.Api.Service
{
    public class EntityService<T> : IEntityService<T>
    {
        private readonly IRepository<T> _repository;

        public EntityService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task Add(T entity)
        {
           ValidationHelper.ValidateNotNull(entity, nameof(entity));
            await _repository.Add(entity);
        }

        public async Task Delete(int id)
        {
            ValidationHelper.ValidateNonNegative(id, nameof(id));
            await _repository.Delete(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<T> GetById(int id)
        {
            ValidationHelper.ValidateNonNegative(id, nameof(id));
            return await _repository.GetById(id);
        }

        public async Task Update(int id, T entity)
        {
            ValidationHelper.ValidateNonNegative(id, nameof(id));
            ValidationHelper.ValidateNotNull(entity, nameof(entity));
            await _repository.Update(id, entity);
        }
    }
}
