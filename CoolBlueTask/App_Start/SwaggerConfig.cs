using System.Web.Http;
using Swashbuckle.Application;

namespace CoolBlueTask
{
    public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.EnableSwagger(c =>
                    {

                        //c.RootUrl(req => url);

                        c.SingleApiVersion("v1", "Cool Blue API");

                        
                    })
                .EnableSwaggerUi("help/{*assetPath}", c =>
                {
                    //c.InjectStylesheet(thisAssembly, "CoolBlueTask.HelpPage.help.css");
                    
                    //c.CustomAsset("Logo.png", thisAssembly, "CoolBlueTask.HelpPage.coolblue-logo.png");
                    //c.CustomAsset("index", thisAssembly, "CoolBlueTask.HelpPage.help.html");
                    
                });
        }
    }
}
