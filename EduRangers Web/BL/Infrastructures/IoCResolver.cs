using BinderLayer.Interfaces;
using BL.Identity;
using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Infrastructures
{
    public class IoCResolver
    {
        public static void Load(Container container)
        {
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<UserContext>(Lifestyle.Scoped);

            container.Register<IRepository, Repository>(Lifestyle.Scoped);

            container.Register<IAbilityManager, AbilityManager>(Lifestyle.Scoped);
            container.Register<IAttemptManager, AttemptManager>(Lifestyle.Scoped);
            container.Register<ICourseManager, CourseManager>(Lifestyle.Scoped);
            container.Register<IQuestionManager, QuestionManager>(Lifestyle.Scoped);
            container.Register<ITestManager, TestManager>(Lifestyle.Scoped);
            container.Register<IUserSlotManager, UserSlotManager>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);


            container.Register<IUserStore<User>>(() => new UserStore<User>(container.GetInstance<UserContext>()), Lifestyle.Scoped);
            container.Register<RoleStore<Role>>(() => new RoleStore<Role>(container.GetInstance<UserContext>()), Lifestyle.Scoped);


            container.Register<UserManager>(Lifestyle.Scoped);
            container.Register<RoleManager>(Lifestyle.Scoped);

        }
    }
}
