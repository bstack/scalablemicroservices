using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace binlookup.Data
{
    public interface IBINRepository
    {
        IEnumerable<Entity.BIN> GetAllBins();
    }
}
