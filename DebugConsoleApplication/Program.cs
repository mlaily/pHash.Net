using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			var about = NativeFunctions.ph_about();
			ulong hash1;
			ulong hash2;
			var res1 = NativeFunctions.ph_dct_imagehash(@"D:\mes documents\visual studio 2010\Projects\ConsoleApplication1\ConsoleApplication1\bin\Debug\1.jpg", out hash1);
			var res2 = NativeFunctions.ph_dct_imagehash(@"D:\mes documents\visual studio 2010\Projects\ConsoleApplication1\ConsoleApplication1\bin\Debug\2.jpg", out hash2);
			var dist = NativeFunctions.ph_hamming_distance(hash1, hash2);
		}
	}

	
}
