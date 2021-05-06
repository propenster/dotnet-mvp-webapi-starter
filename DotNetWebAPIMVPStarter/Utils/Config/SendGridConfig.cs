using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Utils.Config
{
    public class SendGridConfig
    {
        public string CompanyName { get; set; }
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
        public string Default_From_Email { get; set; }
        public string ContactEmail { get; set; }
    }
}
