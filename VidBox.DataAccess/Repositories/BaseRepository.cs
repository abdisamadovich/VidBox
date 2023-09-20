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
            this._connection = new NpgsqlConnection("Host=vidbox-do-user-14588306-0.b.db.ondigitalocean.com; Port=25060; Database=vidbox; User Id=doadmin; Password=AVNS_y7zTXGDeMDXMzaLeEX2;");
        }
    }
}
