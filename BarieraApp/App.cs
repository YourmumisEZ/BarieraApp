using System;
using Android.App;
using Android.Runtime;
using Autofac;
using BarieraApp.Interfaces;
using BarieraApp.Services;

namespace BarieraApp
{
    [Application(Icon = "@drawable/icon", Label = "BarrieraApp")]
    public class App : Application
    {
        public static IContainer Container { get; set; }

        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
        {
        }

        public override void OnCreate()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainService>().As<IMainService>();

            Container = builder.Build();

            base.OnCreate();
        }
    }
}
