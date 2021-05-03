using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using reporting.Entity;
using StackExchange.Redis;

namespace reporting.Data
{
    public class LogActivityRepository : ILogActivityRepository
    {
        private readonly AppSettings c_appSettings;
        private readonly ConnectionMultiplexer c_redisConnection;
        private readonly IDatabase c_database;


        public LogActivityRepository(
            AppSettings appSettings)
        {
            this.c_appSettings = appSettings;
            this.c_redisConnection = ConnectionMultiplexer.Connect($"{this.c_appSettings.DatabaseConnectionString}, allowAdmin = true");
            this.c_database = this.c_redisConnection.GetDatabase();
        }

        public void Log(
            LogActivity logActivity)
        {
            var _key = $"activity:{logActivity.Id}";
            var _value = JsonSerializer.Serialize(logActivity);
            this.c_database.StringSet(_key, _value);
        }

        public IEnumerable<LogActivity> GetRecentLogs()
        {
            var _redisKeys = this.c_redisConnection.GetServer(this.c_appSettings.DatabaseConnectionString).Keys();
            var _keys = _redisKeys.Select(redisKey => (string)redisKey).ToArray();
            var _values = _keys.Select(key => this.c_database.StringGet(key)).ToArray();
            var _logs = _values.Select(value => JsonSerializer.Deserialize<LogActivity>(value));
            var _orderedAndFilteredLogs =  _logs.OrderByDescending(log => log.Timestamp).Take(15);
            return _orderedAndFilteredLogs;
        }
    }
}
