using VidBox.DataAccess.Utils;

namespace VidBox.DataAccess.Common.Interfaces;

public interface  IGetAll<TModel>
{
    public Task<IList<TModel>> GetAllAsync(PaginationParams @params);

}
