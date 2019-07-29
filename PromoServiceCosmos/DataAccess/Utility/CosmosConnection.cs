using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PromoServiceCosmos.DataAccess.Utility
{
    public class CosmosConnection : ICosmosConnection
    {

        protected string DatabaseId { get; }


        /// </summary>
        protected string CollectionId { get; set; }



        private DocumentClient _client;
        private readonly string _endpointUrl;
        private readonly string _authKey;
        private readonly Random _random = new Random();
        private const string _partitionKey = "tenant";
        private readonly ILogger<ICosmosConnection> _logger;
        /// <summary>
        /// Config-based constructor.
        /// </summary>
        /// <param name="config">Config file is used to access DatabaseId, AccountURL, and AuthKey</param>
        /// /// <param name="logger">Logger for logging</param>
        public CosmosConnection(IConfiguration config, ILogger<ICosmosConnection> logger)
        {
            DatabaseId = config.GetValue<string>("Cosmos:DatabaseId");
            _endpointUrl = config.GetValue<string>("Cosmos:AccountURL");
            _authKey = config.GetValue<string>("Cosmos:AuthKey");
            _logger = logger;
        }





        public async Task<DocumentClient> InitializeAsync(string collectionId)
        {
            CollectionId = collectionId;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var connectionPolicy = new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Gateway,
                ConnectionProtocol = Protocol.Https
            };

            if (_client == null)
                _client = new DocumentClient(
                    new Uri(_endpointUrl), _authKey, connectionPolicy);

            await VerifyDatabaseCreated();
            await VerifyCollectionCreated();
            return _client;
        }

       
        private async Task<bool> VerifyDatabaseCreated()
        {
            var database = await _client.CreateDatabaseIfNotExistsAsync(
                new Database
                {
                    Id = DatabaseId
                }
                );

            if (database.StatusCode == HttpStatusCode.Created)
            {
                _logger.LogInformation($"Created DocumentDB database: {DatabaseId}");
                return true;
            }
            else if (database.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation($"DocumentDB database already exists: {DatabaseId}");
                return true;
            }
            return false;
        }


        private async Task<bool> VerifyCollectionCreated()
        {
            if (string.IsNullOrEmpty(CollectionId))
            {
                throw new Exception("No collection id was set before accessing the CosmosConnection's Initialize method");
            }

            var databaseUri = UriFactory.CreateDatabaseUri(DatabaseId);
            var collection = await _client.CreateDocumentCollectionIfNotExistsAsync(
                databaseUri, new DocumentCollection
                {
                    Id = CollectionId,
                    PartitionKey = new PartitionKeyDefinition
                    {
                        Paths = new Collection<string> { $"/{_partitionKey}" }
                    }
                });

            if (collection.StatusCode == HttpStatusCode.Created)
            {
                _logger.LogInformation($"Created DocumentDB collection: {CollectionId}");
                return true;
            }
            else if (collection.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation($"DocumentDB collection already exists: {CollectionId}");
                return true;
            }

            return false;

        }
    }


    }

