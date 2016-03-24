using System.Reflection;
using System.Web.Http;

namespace CoolBlueTask
{
    [RoutePrefix("version")]
    public class VersionController: ApiController
    {
        [HttpGet]
        [Route("")]
        public string Get()
        { 
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return version.ToString();
        }
    }
}