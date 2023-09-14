using Dapper;
using VidBox.DataAccess.Interfaces;
using VidBox.DataAccess.Interfaces.Videos;
using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Videos;

namespace VidBox.DataAccess.Repositories.Videos;

public class VideoRepository : BaseRepository, IVideoRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from videos";
            var result = await _connection.QuerySingleAsync<long>(query);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(Video entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.videos(category_id, name, description, video_path, created_at, updated_at) " +
                "VALUES (@CategoryId, @Name, @Description, @VideoPath, @CreatedAt, @UpdatedAt);";
            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM videos WHERE id=@Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Video>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM public.videos ORDER BY id desc offset {@params.GetSkipCount()} " +
                $"limit {@params.PageSize}";
            var result = (await _connection.QueryAsync<Video>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<Video>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Video?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string qeury = $"SELECT * FROM videos where id=@Id";
            var result = await _connection.QuerySingleAsync<Video>(qeury, new { Id = id });

            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<Video>> SearchAsync(string search)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"SELECT * FROM public.videos WHERE name ILIKE '%{search}%' " +
                $"ORDER BY id DESC ";

            var master = await _connection.QueryAsync<Video>(query);

            return master.ToList();
        }
        catch
        {
            return new List<Video>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Video entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.videos SET category_id=@CategoryId, name=@Name, description=@Description, " +
                "video_path=@VideoPath, updated_at=@UpdatedAt WHERE id = {id};";
            var res = await _connection.ExecuteAsync(query, entity);

            return res;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
