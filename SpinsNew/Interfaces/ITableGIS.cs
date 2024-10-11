using SpinsNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Interfaces
{
    public interface ITableGIS
    {
        Task<List<GisViewModel>> DisplayGisAsync(DateTime startDate, DateTime endDate);
    }
}
