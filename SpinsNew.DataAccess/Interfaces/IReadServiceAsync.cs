﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.Business.Interfaces
{
    public interface IReadServiceAsync<TEntity, TDto> 
        where TEntity : class 
        where TDto: class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(int id);
    }
}
