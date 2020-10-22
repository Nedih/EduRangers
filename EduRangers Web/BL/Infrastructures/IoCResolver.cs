using BinderLayer.Interfaces;
using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Repositories;
using SimpleInjector;
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
            container.Register<UserContext>(Lifestyle.Scoped);

            container.Register<IRepository, Repository>(Lifestyle.Scoped);

            container.Register<IAbilityManager, AbilityManager>(Lifestyle.Scoped);
           /* container.Register<IPartChapterService, PartChapterService>(Lifestyle.Scoped);
            container.Register<IChoiseService, ChoiseService>(Lifestyle.Scoped);
            container.Register<ITextService, TextService>(Lifestyle.Scoped);
            container.Register<IVariantService, VariantService>(Lifestyle.Scoped);
            */
        }
    }
}
