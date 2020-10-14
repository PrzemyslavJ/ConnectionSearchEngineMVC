using Ninject;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConnectionSearchEngineMVC
{
    public class NinjectWebCommon
    {
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new
            ConnectionSearchEngineMVC.Models.NinjectDependencyResolver(kernel));
        }
    }
}
 
