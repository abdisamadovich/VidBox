using VidBox.DataAccess.Utils;

namespace VidBox.DataAccess.Common.Interfaces
{
    public interface ISearchable<TViewModel>
    {
        public Task<IList<TViewModel>> SearchAsync(string search);
    }
}
