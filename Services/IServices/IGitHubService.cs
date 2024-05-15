namespace GithubRepoApi.Services.IServices
{
    public interface IGitHubService
    {
        public Task<object> GetRepositoryInfoAsync(string searchName);
    }
}
