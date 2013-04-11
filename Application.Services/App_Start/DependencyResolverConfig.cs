using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Advanced;
using Domain;
using Domain.Portals;
using Domain.Books;
using Infrastructure.Data;
using Domain.Topics;
using Domain.Articles;

namespace Application.Services
{
    public static class DependencyResolverConfig
    {
        public static void RegisterDependencyResolver()
        {
            Container container = new Container();
            container.Bind();
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorDependencyResolver(container);
        }

        public static void Bind(this Container container)
        {
            container.Register<IUnitOfWork, RefBookDb>();
            container.Register<IPortalRepository, PortalRepository>();
            container.Register<IBookRepository, dapperBookRepository>();
            container.Register<ITopicRepository, dapperTopicRepository>();
            container.Register<IArticleRepository, ArticleRepository>();
        }
    }
}