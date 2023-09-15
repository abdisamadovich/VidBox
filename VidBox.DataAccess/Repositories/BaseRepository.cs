using Dapper;
using Npgsql;
using VidBox.DataAccess.Handlers;

namespace VidBox.DataAccess.Repositories
{
    public class BaseRepository
    {
        protected readonly NpgsqlConnection _connection;
        public BaseRepository()
        {
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            this._connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=VidBox-db; User Id=postgres; Password=0693;");
        }
    }
}
