using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreCourseBeginner.Controllers
{
    //about

    [Route("[controller]/[action]")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "1+555+555+5455";
        }

        public string Address()
        {
            return "My Fake Address";
        }
    }
}
