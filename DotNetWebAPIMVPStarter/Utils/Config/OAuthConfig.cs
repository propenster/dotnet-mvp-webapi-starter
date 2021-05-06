using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Utils.Config
{
    public class OAuthConfig
    {
        public LinkedIn LinkedIn { get; set; }
        public Facebook Facebook { get; set; }
        public Google Google { get; set; }
    }

    public class Google
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class Facebook
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class LinkedIn
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
