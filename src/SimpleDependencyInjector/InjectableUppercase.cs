using System;
namespace SimpleDependencyInjector
{
	public class InjectableUppercase : IInjectable
	{
		public string GetFormatted(string input)
		{
			return input.ToUpper();
		}
	}
}
