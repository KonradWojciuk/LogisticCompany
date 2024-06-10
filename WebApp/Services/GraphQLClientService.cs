using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using Domain.Entities;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using HotChocolate.Language;
namespace WebApp.Services
{
    public class GraphQLClientService
    {
        private readonly IGraphQLClient _client;

        public GraphQLClientService()
        {
            _client = new GraphQLHttpClient("https://example.com/graphql", new NewtonsoftJsonSerializer());
        }

        public async Task<List<Shipment>> GetShipments()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                {
                    shipments {
                        id
                        loadDateTime
                        unLoadDateTime
                        status
                    }
                }"
            };

            try
            {
                var response = await _client.SendQueryAsync<DataResponse<List<Shipment>>>(query);
                return response.Data.shipments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving shipments: {ex.Message}");
                throw;
            }
        }
    }

    public class DataResponse<T>
    {
        public T shipments { get; set; }
    }
}