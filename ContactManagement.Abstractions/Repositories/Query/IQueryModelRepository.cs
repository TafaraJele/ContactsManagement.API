using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Veneka.Platform.Common;

namespace ContactManagement.Abstractions.Repositories.Query
{
    public interface IQueryModelRepository<T, TId> where T : Models.IQueryModel
    {
        Task<T> LoadModelAsync(TId modelId);
        Task<IEnumerable<T>> FindModelsAsync(List<SearchParameter> searchParameters);
        Task<TId> SaveModelAsync(T model);
    }
}
