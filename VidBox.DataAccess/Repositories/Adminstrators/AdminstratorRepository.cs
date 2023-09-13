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

            return await _connection.ExecuteAsync(query, entity);
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
            string query = "DELETE FROM adminstrators WHERE id=@Id";
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

    public Task<Adminstrator?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<AdminstratorViewModel?> GetByIdViewModelAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM adminstrator WHERE id = @Id";
            var result = await _connection.QuerySingleAsync<AdminstratorViewModel>(query, new { Id = id });

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

    public async Task<Adminstrator?> GetByPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM adminstrators WHERE phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<Adminstrator>(query, new { PhoneNumber = phoneNumber });

            return data;
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

    public async Task<int> UpdateAsync(long id, Adminstrator entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.adminstrators SET name=@Name, phone_number=@PhoneNumber, " +
                "phone_number_confirmed=@PhoneNumberConfirmed, password_hash=@PasswordHash, salt=@Salt," +
                    " updated_at=@UpdatedAt WHERE id={id};";

            return await _connection.ExecuteAsync(query, entity);
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
