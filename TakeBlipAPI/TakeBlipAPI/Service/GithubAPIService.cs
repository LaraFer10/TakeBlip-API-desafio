using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TakeBlipAPI.Models;

namespace TakeBlipAPI.Service
{
    public class GithubAPIService
    {
        private static string Endpoint;

        public GithubAPIService()
        {
            Endpoint = GetConfiguration().GetSection("APIgithub").Value;
        }

        private static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile(@"appsettings.json", false).Build();
        }

        public static HttpClient ConfigurarAPI()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Endpoint)
            };
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            client.DefaultRequestHeaders.Add("User-Agent", "request");

            return client;
        }

        public async Task<List<Repository>> ConsultarReposAPIAsync()
        {
            List<Repository> retornoRepos = new List<Repository>();
            var client = ConfigurarAPI();
            var result = await client.GetAsync(Endpoint+ "users/takenet/repos?per_page=100");

            if (result.IsSuccessStatusCode)
            {

                var response = result.Content.ReadAsStringAsync().Result.ToString();

                List<dynamic> repositories = JsonConvert.DeserializeObject<List<dynamic>>(response);

                foreach (var item in repositories)
                {
                    var repos = new Repository();
                    if (item.language == "C#")
                    {
                        repos.Id = item.id;
                        repos.Avatar_url = item.owner.avatar_url;
                        repos.Created_at = item.created_at;
                        repos.Description = item.description;
                        repos.Language = item.language;
                        repos.Name = item.name;
                        retornoRepos.Add(repos);
                    }
                }
                return retornoRepos.OrderBy(x=> x.Created_at).Take(5).ToList();
            }
            else
            {
                return retornoRepos;
            }
        }
    }
}
