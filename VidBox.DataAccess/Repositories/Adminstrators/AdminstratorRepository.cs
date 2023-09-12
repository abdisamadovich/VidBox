using Dapper;
using VidBox.DataAccess.Interfaces.Adminstrators;
using VidBox.DataAccess.ViewModels.Adminstrator;
using VidBox.Domain.Entities.Adminstrators;

namespace VidBox.DataAccess.Repositories.Adminstrators;

public class AdminstratorRepository : BaseRepository, IAdminstratorRepository
{
    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreateAsync(Adminstrator entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.adminstrators(name, phone_number, phone_number_confirmed, password_hash, " +
                "salt, created_at, updated_at) VALUES (@Name, @PhoneNumber, @PhoneNumberConfirmed, @PasswordHash, " +
                    "@Salt, @CreatedAt, @UpdatedAt);";

            return await _connection.ExecuteAsync(query,entity);            
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

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<AdminstratorViewModel?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
