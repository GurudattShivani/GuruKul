using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Gurukul.Business;
using Gurukul.DAL;
using Gurukul.DAL.Abstract;
using Gurukul.DAL.Concrete;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using Ninject.Web.Common;
using Ninject.Web.WebApi;
using StageDocs.DAL.Abstract;

namespace Gurukul.Web.Infrastructure
{
    public class GurukulDependencyResolver : NinjectDependencyScope, System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly IKernel kernel;

        public GurukulDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
            this.RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            //
            // Db Context
            //
            this.kernel.Bind<DbContext>().To<GurukulDbContext>().InRequestScope();
            //
            // Repository factory
            //
            this.kernel.Bind<IRepositoryFactory>().To<RepositoryFactory>().InRequestScope();
            //
            // Custom repositories
            //
            this.kernel.Bind<Dictionary<Type, object>>().ToMethod(x =>
            {
                var dbContext = this.kernel.Get<DbContext>();

                return new Dictionary<Type, object>
                {
                    //{typeof (Course), new CourseRepository(dbContext)},
                    {typeof (Instructor), new InstructorRepository(dbContext)}
                };
            }).WhenInjectedInto<IRepositoryFactory>().InRequestScope();

            this.kernel.Bind<IUoW>().To<UoW>().InRequestScope(); //.WithParameter(new Parameter("UoW", (c,t)=>c.Kernel.Get<IUoW>(), false));

            this.kernel.Bind(typeof (GenericRepository<>))
                .To(typeof (GenericRepository<>))
                .InRequestScope()
                .WithPropertyValue("UoW", x => x.Kernel.Get<IUoW>());
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}