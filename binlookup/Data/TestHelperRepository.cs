using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using StackExchange.Redis;

namespace binlookup.Data
{
    public class TestHelperRepository : ITestHelperRepository
    {
        private readonly AppSettings c_appSettings;
        private readonly ConnectionMultiplexer c_redisConnection;
        private readonly IDatabase c_database;


        public TestHelperRepository(
            AppSettings appSettings)
        {
            this.c_appSettings = appSettings;
            Console.WriteLine($"Here:fsdfsdfsdffsd:{appSettings.DatabaseConnectionString}");

            this.c_redisConnection = ConnectionMultiplexer.Connect($"{appSettings.DatabaseConnectionString}, allowAdmin = true");
            this.c_database = this.c_redisConnection.GetDatabase();
        }


        public void SeedDatabase()
        {
            var _BINVisaEuro = new Entity.BIN(
                407010,
                407030,
                "VI",
                "IRL",
                "EUR");
            var _BINMasterCardEuro = new Entity.BIN(
                534568,
                534568,
                "MC",
                "IRL",
                "EUR");
            var _BINVisaBritishPound = new Entity.BIN(
                499980,
                499990,
                "VI",
                "GBR",
                "GBP");
            var _BINMasterCardBritishPound = new Entity.BIN(
                599980,
                599990,
                "MC",
                "GBR",
                "GBP");
            var _BINVisaJapaneseYen = new Entity.BIN(
                422222,
                422228,
                "VI",
                "JPN",
                "JPY");
            var _BINMasterCardJapaneseYen = new Entity.BIN(
                522222,
                522228,
                "MC",
                "JPN",
                "JPY");

            var _BINs = new List<Entity.BIN> {
                _BINVisaEuro,
                _BINMasterCardEuro,
                _BINVisaBritishPound,
                _BINMasterCardBritishPound,
                _BINVisaJapaneseYen,
                _BINMasterCardJapaneseYen };

            _BINs.ForEach(bin =>
            {
                var _key = $"bin:{bin.Low}_{bin.High}";
                var _value = JsonSerializer.Serialize<Entity.BIN>(bin);
                this.c_database.StringSet(_key, _value);
            });
        }


        public void ClearDatabase()
        {
            var server = this.c_redisConnection.GetServer(this.c_appSettings.DatabaseConnectionString);
            server.FlushDatabase();
        }
    }
}
