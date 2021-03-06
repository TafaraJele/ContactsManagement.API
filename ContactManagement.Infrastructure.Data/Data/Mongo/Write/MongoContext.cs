using ContactManagement.Abstractions.Settings;
using ContactManagement.Core.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Infrastructure.Data.Data.Mongo.Write
{
    public class MongoContext
    {
        public IMongoDatabase Database { get; }

        private readonly string _serverName;
        private readonly string _databaseName;
        private readonly ConventionPack camelConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        private readonly ConventionPack ignoreExtraElementsPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
        private readonly ConventionPack ignoreNullsPack = new ConventionPack { new IgnoreIfNullConvention(true) };
        private readonly MongoClient client;
        public string ServerName => _serverName;
        public string DatabaseName => _databaseName;

        protected MongoContext()
        {
            client = new MongoClient(_serverName);
            Database = client.GetDatabase(_databaseName);
        }

        public MongoContext(IOptions<ApplicationSettings> config)
        {

            _serverName = config.Value.QueryServerName;
            _databaseName = config.Value.QueryDatabaseName;

            ConventionPack pack = new ConventionPack
            {
                new IgnoreIfNullConvention(true),
                 new IgnoreExtraElementsConvention(true),
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register("defaults", pack, t => true);
            client = new MongoClient(_serverName);
            Database = client.GetDatabase(_databaseName);
        }

        public MongoContext(string serverName, string databaseName)
        {
            _serverName = serverName;
            _databaseName = databaseName;
            MongoClient client = new MongoClient(_serverName);
            ConventionRegistry.Register("CamelCaseConvensions", camelConventionPack, t => true);
            ConventionRegistry.Register("IgnoreExtraElements", ignoreExtraElementsPack, t => true);
            ConventionRegistry.Register("Ignore null values", ignoreNullsPack, t => true);
            Database = client.GetDatabase(_databaseName);
        }

        public IMongoCollection<ContactEntity> Contacts => Database.GetCollection<ContactEntity>("Contacts"); 
    }
}
