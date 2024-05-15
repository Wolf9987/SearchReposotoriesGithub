using GithubRepoApi.Models.DTO;
using GithubRepoApi.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace GithubRepoApi.Controllers
{
    /// <summary>
    /// GithubApi controller that has GetGitHubRepositories method.
    /// This method executes service to bring repositories from github
    /// ResponseDto is custom httpResponse to client.
    /// </summary>
    [Route("api/githubApi")]
    [ApiController]
    public class GitHubApiController : ControllerBase
    {
        private IGitHubService _gitHubService;
        private ResponseDto _response;
        public GitHubApiController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
            _response = new ResponseDto();
        }

        /// <summary>
        /// Method that receives repository name to search, and
        /// returns list of results from github
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseDto> GetGitHubRepositories(string searchName)
        {
            try
            {
                object res = await _gitHubService.GetRepositoryInfoAsync(searchName);
                if (res != null)
                {
                    _response.Result = res;
                }
                else
                {
                    _response.Message = "No values found";
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
