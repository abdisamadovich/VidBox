using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Videos;
using VidBox.Service.Dtos.Videos;

namespace VidBox.Service.Interfaces.Videos;

public interface IVideoService
{
    public Task<IList<Video>> GetAllAsync(PaginationParams @params);
    public Task<bool> CreateAsync(VideoCreateDto dto);
    public Task<bool> UpdateAsync(long videoId, VideoUpdateDto dto);
    public Task<bool> DeleteAsync(long videoId);
    public Task<long> CountAsync();
    public Task<IList<Video>> SearchAsync(string search);
    public Task<Video?> GetByIdAsync(long id);

}
