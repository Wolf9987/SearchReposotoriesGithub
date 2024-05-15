using GithubRepoApi.Services;

namespace GithubRepoApi.Models
{
    public class GithubJsonObject
    {
        public int Total_Count { get; set; }
        public List<Item> Items { get; set; }

    }
}
