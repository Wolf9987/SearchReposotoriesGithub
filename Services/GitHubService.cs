using GithubRepoApi.Models;
using GithubRepoApi.Services.IServices;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.Json;



namespace GithubRepoApi.Services
{
    /// <summary>
    /// Github service that is responsible for github operation(s).
    /// 
    /// </summary>
    public class GitHubService : IGitHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IEnumerable<object>? GitHubBranches { get; set; }
        public GitHubService(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;

        }

        /// <summary>
        /// method that recieves parameter to search and searches it in github repositories, 
        /// and returns relevant repositories.
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public async Task<object> GetRepositoryInfoAsync(string searchName)
        {
            List<dynamic> reposList = new List<dynamic>();
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.github.com/search/repositories?q=" + searchName)
                
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/vnd.github.v3+json" },
                    { HeaderNames.UserAgent, "HttpRequestsSample" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            //if (httpresponsemessage.issuccessstatuscode)
            //{
            //    //using var contentstream =
            //    //    await httpresponsemessage.content.readasstreamasync();

            //    //githubbranches = await jsonserializer.deserializeasync
            //    //    <ienumerable<object>>(contentstream);


            //    string jsonstring = await httpresponsemessage.content.readasstringasync();
            //    dynamic? result = jsonconvert.deserializeobject(jsonstring);

            //    foreach (var item in result.items)
            //    {
            //        dynamic repoinfo = new
            //        {
            //            id = item.id.tostring(),
            //            avatar_url = item.owner.avatar_url.tostring(),
            //            name = item.name.tostring()
            //        };
            //        reposlist.add(repoinfo);
            //    }

            //}

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Stream stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                string jsonData = "";
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        jsonData = reader.ReadToEnd();
                    }
                }

                GithubJsonObject githubJsonObject = JsonConvert.DeserializeObject<GithubJsonObject>(jsonData);
                List<Bookmark> bookmarkList = new List<Bookmark>(); 
                foreach (var jres in githubJsonObject.Items)
                {
                    Bookmark bm = new Bookmark
                    {
                        Id = jres.Id,
                        Name = jres.Name,
                        Avatar_url = jres.Owner.Avatar_Url
                    };
                    bookmarkList.Add(bm);
                }

                return bookmarkList;


            }
            else
                return null;
            
            
        }
    }
}
