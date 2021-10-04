using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.Interface
{
    public interface ISP_Call : IDisposable
    {
        T Single<T>(string procudureName, DynamicParameters param = null);

        void Execute(string procudureName, DynamicParameters param = null);

        T OneRecord<T>(string procudureName, DynamicParameters param = null);

        IEnumerable<T> List<T>(string procudureName, DynamicParameters param = null);

        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procudureName, DynamicParameters param = null);
    }
}
