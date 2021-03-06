﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart;

using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Xml;
using Ninject.Extensions.Conventions;

namespace NinjectIoc
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1 create the kernel
            var kernel = new StandardKernel();

            // 2 bind/map our dependencies
            //kernel.Bind<ICreditCard>().To<MasterCard>();

            //// 3 bind to self
            //kernel.Bind<Shopper>().ToSelf().InSingletonScope();

            //// 4 rebind
            //kernel.Rebind<ICreditCard>().To<Visa>();

            // 5 property injection

            // 6 lifetime and scope

            // 7 modules
            //kernel.Load(new ShopperModule());

            // 8 xml config
            //kernel.Load(Helpers.AssemblyDirectory + "\\*.xml");

            // 9 conventions
            //kernel.Bind(x => x
            //                  .FromAssembliesInPath(Helpers.AssemblyDirectory)
            //                  .SelectAllClasses()
            //                  .InheritedFrom<ICreditCard>()
            //                  .BindDefaultInterfaces()
            //                  .Configure(b => b.InSingletonScope()
            //                                   .WithConstructorArgument("name", "BackupCard"))
            //                  //.ConfigureFor<Shopper>(b => b.InThreadScope())
            //            );
            // 10 on activating
            //kernel.Bind<ICreditCard>().To<MasterCard>().OnActivation(ctx => )

            // 11 existing methods & factories
            kernel.Bind<ICreditCard>().ToMethod(ctx => GetCard("user"));

            Shopper shopper = kernel.Get<Shopper>();
            shopper.Charge();
            shopper.IterateCounter();

            Shopper shopper1 = kernel.Get<Shopper>();
            shopper1.Charge();
            shopper1.IterateCounter();

            Console.ReadKey();
        }

        public static ICreditCard GetCard(string user)
        {
            // some complicated logic 
            return new MasterCard();
        }

        public class ShopperModule : NinjectModule
        {

            public override void Load()
            {
                Bind<ICreditCard>().To<Visa>();
                Rebind<ICreditCard>().To<MasterCard>().InSingletonScope();
            }
        }
    }
}
