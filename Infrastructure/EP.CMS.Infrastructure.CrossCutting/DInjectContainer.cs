using EP.CMS.Domain.ContentModule;
using EP.CMS.Domain.DirectoryModule;
using EP.CMS.Domain.MenuModule;
using EP.CMS.Domain.MenuModule.Services;
using EP.CMS.Domain.PageModule;
using EP.CMS.Domain.SeedWork;
using EP.CMS.Domain.UserModule;
using EP.CMS.Infrastructure.Data.Repositories;
using EP.CMS.Infrastructure.Data.UnitOfWork;
using EP.CMS.Infrastructure.ExternalServices.Managers;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.CMS.Infrastructure.CrossCutting
{
    public class DInjectContainer
    {

        //public static IUnityContainer Current
        //{
        //    get
        //    {
        //        return _currentContainer;
        //    }
        //}

        #region Constructor

        public DInjectContainer(IKernel kernel)
        {
            ConfigureContainer(kernel);
        }

        #endregion


        public static void ConfigureContainer(IKernel kernel)
        {
            /*
             * Add here the code configuration or the call to configure the container 
             * using the application configuration file
             */

            kernel.Bind<IPageServices>().To<PageServices>();
            kernel.Bind<IContentServices>().To<ContentServices>();
            kernel.Bind<IMenuServices>().To<MenuServices>();
            kernel.Bind<IUserServices>().To<UserServices>();
            kernel.Bind<IDirectoryServices>().To<DirectoryServices>();

            kernel.Bind<IUnitOfWork>().To<MainUnitOfWork>();
            kernel.Bind<IContentRepository>().To<ContentRepository>();
            kernel.Bind<IMenuRepository>().To<MenuRepository>();
            kernel.Bind<IPageRepository>().To<PageRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IDirectoryRepository>().To<DirectoryRepository>();


            kernel.Bind<IPageExtRepository>().To<PageManager>();
            kernel.Bind<IBinaryExtRepository>().To<BinaryManager>();
            kernel.Bind<IMenuExtRepository>().To<MenuManager>();

            //kernel.Bind<IPageExtRepository>().To<MockPageManager>();
            //kernel.Bind<IBinaryExtRepository>().To<MockBinaryManager>();
            //kernel.Bind<IMenuExtRepository>().To<MockMenuManager>();
            //-> Unit of Work and repositories

        }

    }
}
