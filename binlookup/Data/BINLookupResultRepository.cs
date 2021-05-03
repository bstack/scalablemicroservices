using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using StackExchange.Redis;

namespace binlookup.Data
{
    public class BINLookupResultRepository : IBINLookupResultRepository
    {
        private readonly ConnectionMultiplexer c_redisConnection;
        private readonly IDatabase c_database;


        public BINLookupResultRepository(
            AppSettings appSettings)
        {
            this.c_redisConnection = ConnectionMultiplexer.Connect($"{appSettings.DatabaseConnectionString}, allowAdmin = true");
            this.c_database = this.c_redisConnection.GetDatabase();
        }


        public Entity.BINLookupResult Get(
            string requestId)
        {
            var _key = $"binlookupresult:{requestId}";
            var _redisValue = this.c_database.StringGet(_key);

            if (_redisValue.HasValue)
                return JsonSerializer.Deserialize<Entity.BINLookupResult>(_redisValue);
            else
                return null;
        }

        public void Save(
            Entity.BINLookupResult BINLookupResult)
        {
            var _key = $"binlookupresult:{BINLookupResult.RequestId}";
            var _value = JsonSerializer.Serialize(BINLookupResult);
            this.c_database.StringSet(_key, _value);
        }
    }
}
