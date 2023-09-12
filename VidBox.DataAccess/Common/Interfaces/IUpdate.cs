using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace VidBox.DataAccess.Common.Interfaces
{
    public interface IUpdate<TEntity>
    {
        public Task<int> UpdateAsync(long id, TEntity entity);
    }
}
