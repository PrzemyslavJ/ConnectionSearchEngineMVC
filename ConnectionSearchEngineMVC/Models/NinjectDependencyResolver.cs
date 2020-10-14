using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionSearchEngineMVC.Models
{
    public class NinjectDependencyResolver : IDbDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public object GetService(Type type, object key)
        {
            return kernel.TryGet(type);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IEnumerable<object> GetServices(Type type, object key)
        {
            throw new NotImplementedException();
        }

        private void AddBindings()
        {
            this.kernel.Bind<IResultRepository>().To<AllRecords>();
            this.kernel.Bind<IgetRegisterRecords>().To<ReservationClass>();
        }












        /*
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public object GetService(Type type, object key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        public IEnumerable<object> GetServices(Type type, object key)
        {
            throw new NotImplementedException();
        }

        private void AddBindings()
        {
            kernel.Bind<IResultRepository>().To<AllRecords>();
            kernel.Bind<IgetRegisterRecords>().To<ReservationRegister>();
        }
        */
    }
}
