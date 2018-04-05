using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SimpleDependencyInjector
{
    class Program
    {

        static void Main(string[] args)
        {
			_settings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            //Run our custom injector to lookup which implementation.
			IInjectable injectable = InjectionFactory.Get<IInjectable>();

			Console.WriteLine(injectable.GetFormatted("Hello, World!"));
            Console.WriteLine("------- Press any key to exit -------");
            Console.ReadKey();
        }

		private static IConfigurationBuilder _settings;
		internal static IConfigurationBuilder Settings
        {
            get
            {
                return _settings;
            }
        }

        /// <summary>
		/// Injection factory; looks up the name of the interface type in
		/// the settings and returns a concrete implementation.
        /// </summary>
        class InjectionFactory
		{
			public static T Get<T>()
            {
				var config = _settings.Build();                
				string concreteName = config.GetSection("App:Injector:" + typeof(T).Name).Value;
                //Use CreateInstance to init a new instance of the supplied type
                if(!string.IsNullOrWhiteSpace(concreteName))
                {
                    object concrete = Activator.CreateInstance(Type.GetType(concreteName));
                    return (T)concrete;
                }

                throw new ApplicationException($"No dependency configuration could be found for the type {typeof(T).Name}");
            }
		}
    }
}
