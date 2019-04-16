using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sphaera.IdentityServer
{
    public class ModuleProduction : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RT
            base.Load(builder);
        }
    }
}
