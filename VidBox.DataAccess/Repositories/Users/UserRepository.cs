using Dapper;
using System.Numerics;
using VidBox.DataAccess.Common.Interfaces;
using VidBox.DataAccess.Interfaces;
using VidBox.DataAccess.Interfaces.Users;
using VidBox.DataAccess.Utils;
using VidBox.DataAccess.ViewModels.Users;
using VidBox.Domain.Entities.Users;

namespace VidBox.DataAccess.Repositories.Users;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from users";
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

    public async Task<int> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "INSERT INTO public.users(name, phone_number, phone_number_confirmed, password_hash, salt, " +
                "created_at, updated_at) VALUES (@Name, @PhoneNumber, @PhoneNumberConfirmed, @PasswordHash, @Salt, " +
                    "@CreatedAt, @UpdatedAt);";

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
            string query = $"delete from users where id = {id}";

            return await _connection.ExecuteAsync(query);
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

    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();

            string query = $"select * from users " +
                $"order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<UserViewModel>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<UserViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM users WHERE id=@Id";
            var result = await _connection.QuerySingleAsync<User>(query, new { Id = id });

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

    public async Task<UserViewModel?> GetByIdViewAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM users where id=@Id";
            var result = await _connection.QuerySingleAsync<UserViewModel>(query, new { Id = id });

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

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM users where phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<User>(query, new { PhoneNumber = phoneNumber });

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

    public async Task<IList<UserViewModel>> SearchAsync(string search)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from users " +
                $"where name ilike '%{search}%'";

            var result = (await _connection.QueryAsync<UserViewModel>(query)).ToList();
            return result;
        }
        catch (Exception)
        {
            return new List<UserViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.users SET name=@Name, phone_number=@PhoneNumber," +
                " phone_number_confirmed=@PhoneNumberConfirmed, password_hash=@PasswordHash, salt=@Salt," +
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
