using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LO30.Data.Access
{
    public interface IAccessO30Repository
    {
        IQueryable<AccessO30Standing> GetStandings();
    }
}
