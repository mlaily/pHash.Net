//Copyright 2012 Melvyn Laily
//http://arcanesanctum.net

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace pHash.Net
{
	public class pHashNet
	{

		/// <summary>
		/// Normalize the comparison results for all the algorithms.
		/// This way, the result is always between 0 and 1.
		/// 0 meaning same images, and 1 meaning totally different images.
		/// </summary>
		/// <param name="usedAlgorithm"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		private static double NormalizeThreshold(HashAlgorithm usedAlgorithm, double value)
		{
			switch (usedAlgorithm)
			{
				case HashAlgorithm.DCT:
					return value / 64f; //sizeof(ulong);
				case HashAlgorithm.Radial:
					return 1f - value; //0 for totally different images, 1 for identical
				case HashAlgorithm.MH:
					return value;
				default:
					throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Return the hash of the specified file, calculated using the specified hash algorithm.
		/// </summary>
		/// <param name="algorithm"></param>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static ImageHash HashImage(HashAlgorithm algorithm, string filePath)
		{
			ImageHash hash;
			switch (algorithm)
			{
				case HashAlgorithm.DCT:
					ulong DCTHash;
					NativeFunctions.ph_dct_imagehash(filePath, out DCTHash);
					hash = new DCTHash(DCTHash);
					break;
				case HashAlgorithm.Radial:
					NativeStructures.Digest digest;
					NativeFunctions.ph_image_digest(filePath, 1, 1, out digest);
					hash = new RadialHash(digest);
					break;
				case HashAlgorithm.MH:
					int count;
					IntPtr hashBytes = NativeFunctions.ph_mh_imagehash(filePath, out count);
					hash = new MHHash(hashBytes, count);
					break;
				default:
					throw new NotImplementedException();
			}
			return hash;
		}

		/// <summary>
		/// Return a hash for the given text file.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static TextHash HashText(string filePath)
		{
			int count;
			IntPtr hashPoints = NativeFunctions.ph_texthash(filePath, out count);
			var result = new TextHash(hashPoints, count);
			return result;
		}

		/// <summary>
		/// Return the result of the comparison between two TextHash.
		/// </summary>
		/// <param name="hash1"></param>
		/// <param name="hash2"></param>
		/// <returns></returns>
		public static NativeStructures.TxtMatch[] CompareTextHashes(TextHash hash1, TextHash hash2)
		{
			int resultCount;
			IntPtr rawTxtMatches = NativeFunctions.ph_compare_text_hashes(hash1.HashPoints, hash1.PointsCount, hash2.HashPoints, hash2.PointsCount, out resultCount);
			NativeStructures.TxtMatch[] array = new NativeStructures.TxtMatch[resultCount];
			for (int i = 0; i < resultCount; i++)
			{
				array[i] = (NativeStructures.TxtMatch)Marshal.PtrToStructure(rawTxtMatches, typeof(NativeStructures.TxtMatch));
				rawTxtMatches = IntPtr.Add(rawTxtMatches, Marshal.SizeOf(typeof(NativeStructures.TxtMatch)));
			}
			return array;
		}

		/// <summary>
		/// Compare two image hashes and return a value between 0 and 1.
		/// 0 meaning the two hashes (and therefore underlying images) are the same,
		/// and 1 meaning they are totally different.
		/// </summary>
		/// <param name="hash1"></param>
		/// <param name="hash2"></param>
		/// <returns></returns>
		public static double CompareHashes(ImageHash hash1, ImageHash hash2)
		{
			if (hash1.Algorithm != hash2.Algorithm)
			{
				throw new ArgumentException("Both hashes must use the same algorithm!");
			}
			double result;
			switch (hash1.Algorithm)
			{
				case HashAlgorithm.DCT:
					int rawHammingResult =
						NativeFunctions.ph_hamming_distance(((DCTHash)hash1).Hash, ((DCTHash)hash2).Hash);
					result = NormalizeThreshold(HashAlgorithm.DCT, rawHammingResult);
					break;
				case HashAlgorithm.Radial:
					NativeStructures.Digest digest1 = ((RadialHash)hash1).Digest;
					NativeStructures.Digest digest2 = ((RadialHash)hash2).Digest;

					double rawCrossCorrResult;
					NativeFunctions.ph_crosscorr(ref digest1, ref digest2, out rawCrossCorrResult);
					result = NormalizeThreshold(HashAlgorithm.Radial, rawCrossCorrResult);
					break;
				case HashAlgorithm.MH:
					MHHash mhHash1 = (MHHash)hash1;
					MHHash mhHash2 = (MHHash)hash2;

					double rawHamming2Result =
						NativeFunctions.ph_hammingdistance2(mhHash1.BytesPtr, mhHash1.ByteCount, mhHash2.BytesPtr, mhHash2.ByteCount);
					result = NormalizeThreshold(HashAlgorithm.MH, rawHamming2Result);
					break;
				default:
					throw new NotImplementedException();
			}
			return result;
		}

		/// <summary>
		/// Compare two image hashes and return a boolean indicating if the underlying images are similar (true) or not (false).
		/// </summary>
		/// <param name="hash1"></param>
		/// <param name="hash2"></param>
		/// <param name="result"></param>
		/// <param name="threshold"></param>
		/// <returns></returns>
		public static bool CompareHashes(ImageHash hash1, ImageHash hash2, out double result, double threshold = .40f)
		{
			result = CompareHashes(hash1, hash2);
			return result <= threshold;
		}

		/// <summary>
		/// Compare two image hashes and return a boolean indicating if the underlying images are similar (true) or not (false).
		/// </summary>
		/// <param name="hash1"></param>
		/// <param name="hash2"></param>
		/// <param name="threshold"></param>
		/// <returns></returns>
		public static bool CompareHashes(ImageHash hash1, ImageHash hash2, double threshold = .40f)
		{
			double result = CompareHashes(hash1, hash2);
			return result <= threshold;
		}

		/// <summary>
		/// simply call the about method of the dll.
		/// </summary>
		/// <returns></returns>
		public static string About_pHash()
		{
			return NativeFunctions.ph_about();
		}

	}
}
