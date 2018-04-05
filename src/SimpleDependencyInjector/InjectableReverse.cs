using System;
namespace SimpleDependencyInjector
{
	public class InjectableReverse : IInjectable
	{
		public string GetFormatted(string input)
		{
			char[] arr = input.ToCharArray();
			Array.Reverse(arr);
			return new string(arr);
		}
	}
}
