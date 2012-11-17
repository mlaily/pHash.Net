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
			var res1 = NativeFunctions.ph_dct_imagehash(@"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\1.jpg", out hash1);
			var res2 = NativeFunctions.ph_dct_imagehash(@"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\2.jpg", out hash2);
			var dist = NativeFunctions.ph_hamming_distance(hash1, hash2);

			NativeStructures.Digest digest1;
			NativeStructures.Digest digest2;
			var res3 = NativeFunctions.ph_image_digest(@"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\black.jpg", 1, 1, out digest1);
			var res4 = NativeFunctions.ph_image_digest(@"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\white.jpg", 1, 1, out digest2);
			double pcc;
			var x = NativeFunctions.ph_crosscorr(ref digest1, ref digest2, out pcc);
			int n1;
			int n2;
			var res5 = NativeFunctions.ph_mh_imagehash(@"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\1.jpg", out n1);
			var res6 = NativeFunctions.ph_mh_imagehash(@"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\2.jpg", out n2);
			var y = NativeFunctions.ph_hammingdistance2(res5, n1, res6, n2);
		}
	}


}
