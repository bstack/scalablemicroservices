using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using StackExchange.Redis;

namespace binlookup.Data
{
    public class BINRepository : IBINRepository
    {
        private readonly AppSettings c_appSettings;
        private readonly ConnectionMultiplexer c_redisConnection;
        private readonly IDatabase c_database;


        public BINRepository(
            AppSettings appSettings)
        {
            this.c_appSettings = appSettings;
            this.c_redisConnection = ConnectionMultiplexer.Connect($"{this.c_appSettings.DatabaseConnectionString}, allowAdmin = true");
            this.c_database = this.c_redisConnection.GetDatabase();
        }


        public IEnumerable<Entity.BIN> GetAllBins()
        {
            var _redisKeys = this.c_redisConnection.GetServer(this.c_appSettings.DatabaseConnectionString).Keys();
            var _keys = _redisKeys.Select(redisKey => (string)redisKey).ToArray();
            var _values = _keys.Select(key => this.c_database.StringGet(key)).ToArray();
            var _BINs = _values.Select(value => JsonSerializer.Deserialize<Entity.BIN>(value));
            return _BINs;
        }
    }
}
