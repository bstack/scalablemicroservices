using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace binlookup.Data
{
    public interface ITestHelperRepository
    {
        void SeedDatabase();


        void ClearDatabase();
    }
}
