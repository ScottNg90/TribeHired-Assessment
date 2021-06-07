using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TribeHired_Assessment.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(long id);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        IHttpClientFactory _httpClientFactory;
        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var response = await _httpClientFactory.CreateClient().GetAsync("https://jsonplaceholder.typicode.com/" + typeof(T).Name + "s");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public async Task<T> Get(long id)
        {
            var response = await _httpClientFactory.CreateClient().GetAsync("https://jsonplaceholder.typicode.com/" + typeof(T).Name + "s/"+id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
