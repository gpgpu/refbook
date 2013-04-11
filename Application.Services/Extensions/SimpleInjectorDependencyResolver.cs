using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SimpleInjector;

namespace Application.Services
{
    public class SimpleInjectorDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private Container _container;
        public SimpleInjectorDependencyResolver(Container container)
        {
            _container = container;
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            IServiceProvider provider = this._container;
            return provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._container.GetAllInstances(serviceType);
        }

        public void Dispose()
        {
            
        }
    }
}