using AutoMapper;
using SpinsNew.Business.Interfaces;
using SpinsNew.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;

namespace SpinsNew.Business.Services
{
    public class ReadServiceAsync<TEntity, TDto>: IReadServiceAsync<TEntity, TDto>
        where TEntity: class
        where TDto: class
    {
        private readonly IGenericRepository<TEntity> _genericRepository;
        private readonly IMapper _mapper;

        public ReadServiceAsync(IGenericRepository<TEntity> genericRepository, IMapper mapper) : base()
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            try
            {
                var result = await _genericRepository.GetAllAsync();

                if (result.Any())
                {
                    return _mapper.Map<IEnumerable<TDto>>(result);
                }
                else
                {
                    throw new EntityNotFoundException($"No {typeof(TDto).Name}s were not Found.");
                }

            }
            catch (EntityNotFoundException ex)
            {
                var message = $"Error retrieving all {typeof(TDto).Name}s";

                throw new EntityNotFoundException(message, ex);
            }
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            try
            {
                var result = await _genericRepository.GetByIdAsync(id);

                if(result is null)
                {
                    throw new EntityNotFoundException($"Entity with ID {id} not found.");
                }
                return _mapper.Map<TDto>(result);
            }
            catch (EntityNotFoundException ex)
            {
                var message = $"Error retrieving {typeof(TDto).Name} with Id: {id}";
                throw new EntityNotFoundException(message, ex);
            }
        }

    }
}
