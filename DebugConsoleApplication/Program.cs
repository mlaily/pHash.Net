using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using pHash.Net;

namespace DebugConsoleApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			var file1 = @"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\white.jpg";
			var file2 = @"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\black.jpg";

			var res1 = pHashNet.CompareHashes(pHashNet.HashImage(HashAlgorithm.DCT, file1), pHashNet.HashImage(HashAlgorithm.DCT, file2));
			var res2 = pHashNet.CompareHashes(pHashNet.HashImage(HashAlgorithm.MH, file1), pHashNet.HashImage(HashAlgorithm.MH, file2));
			var res3 = pHashNet.CompareHashes(pHashNet.HashImage(HashAlgorithm.Radial, file1), pHashNet.HashImage(HashAlgorithm.Radial, file2));
		}
	}


}
