using VidBox.DataAccess.Utils;

namespace VidBox.Service.Interfaces.Common
{
    public interface IPaginator
    {
        public void Paginate(long itemsCount, PaginationParams @params);
    }
}
