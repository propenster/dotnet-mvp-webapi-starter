using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Services.OAuth
{
    public interface IOAuthService
    {
        Task LinkedinAuth();
        Task FacebookAuth();
        Task GoogleAuth();
        Task GithubAuth();
    }
}
