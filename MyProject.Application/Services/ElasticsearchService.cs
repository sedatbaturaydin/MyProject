using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyProject.Application.DTOs;
using MyProject.Application.Interfaces;

namespace MyProject.Application.Services
{
    public class ElasticsearchService: IElasticsearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly string _indexName;

        public ElasticsearchService(IConfiguration configuration)
        {
            var settings = new ConnectionSettings(new Uri(configuration["Elasticsearch:Url"]))
                .DefaultIndex(configuration["Elasticsearch:Index"]);

            _elasticClient = new ElasticClient(settings);
            _indexName = configuration["Elasticsearch:Index"];
        }

        public async Task<bool> IndexExistsAsync()
        {
            var response = await _elasticClient.Indices.ExistsAsync(_indexName);
            return response.Exists;
        }

        public async Task CreateIndexAsync()
        {
            if (!await IndexExistsAsync())
            {
                var createIndexResponse = await _elasticClient.Indices.CreateAsync(_indexName, c => c
                    .Map<BookDto>(m => m.AutoMap())
                );
            }
        }

        public async Task IndexDocumentAsync(BookDto book)
        {
            await _elasticClient.IndexDocumentAsync(book);
        }

        public async Task EnsureIndexExistsAsync()
        {
            if (!await IndexExistsAsync())
            {
                await CreateIndexAsync();
            }
        }

        public async Task<List<BookDto>> SearchBooksAsync(string query)
        {
            var searchResponse = await _elasticClient.SearchAsync<BookDto>(s => s
                .Index(_indexName)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Title)
                        .Query(query)
                        .Fuzziness(Fuzziness.Auto)
                    )
                )
            );

            return searchResponse.Documents.ToList();
        }
    }
}
