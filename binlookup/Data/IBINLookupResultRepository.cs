using System;

namespace binlookup.Data
{
    public interface IBINLookupResultRepository
    {
        Entity.BINLookupResult Get(
            string requestId);

        void Save(
            Entity.BINLookupResult BINLookupResult);
    }
}
