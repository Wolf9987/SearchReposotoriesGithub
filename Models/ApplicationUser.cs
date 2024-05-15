using Microsoft.AspNetCore.Identity;

namespace GithubRepoApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
