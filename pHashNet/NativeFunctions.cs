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
	public class NativeFunctions
	{

		/// <summary>
		/// compute discrete cosine transform (dct) robust image hash.
		/// </summary>
		/// <param name="file">string variable for name of file.</param>
		/// <param name="hash">hash of type ulong64 (must be 64-bit variable).</param>
		/// <returns>
		/// return int value - -1 for failure, 1 for success.
		/// *** wrong! seems to return 0 on success...
		/// </returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ph_dct_imagehash(
			[MarshalAs(UnmanagedType.LPStr)] string file,
			out ulong hash);

		/// <summary>
		/// Once you have the hashes for two files, you can compare them.
		/// The hamming distance function can be used for both video and image hashes.
		/// </summary>
		/// <param name="hash1"></param>
		/// <param name="hash2"></param>
		/// <returns></returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ph_hamming_distance(ulong hash1, ulong hash2);

		/// <summary>
		/// If you want to use the radial hashing method for images.
		/// Compute the image digest given the file name.
		/// Use values sigma=1.0 and gamma=1.0 for now. N indicates the number of lines to project through the center for 0 to 180 degrees orientation. Use 180.
		/// Be sure to declare a digest before calling the function, like this:
		/// Digest dig;
		/// ph_image_digest(filename, 1.0, 1.0, dig, 180);
		/// </summary>
		/// <param name="file">string value for file name of input image.</param>
		/// <param name="sigma">double value for the deviation for gaussian filter</param>
		/// <param name="gamma">double value for gamma correction on the input image.</param>
		/// <param name="digest">Digest struct.</param>
		/// <param name="N">int value for number of angles to consider</param>
		/// <returns>The function returns -1 if it fails. This standard will be found in most of the functions in the pHash library. </returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ph_image_digest(string file, double sigma, double gamma, out NativeStructures.Digest digest, int N = 180);

		/// <summary>
		/// Compute the cross correlation of two series vectors.
		/// 
		/// To compare two radial hashes, a peak of cross correlation is determined between two hashes:
		/// int ph_crosscorr(Digest &x, Digest &y, double &pcc, double threshold=0.90);
		/// The peak of cross correlation between the two vectors is returned in the pcc parameter. 
		/// </summary>
		/// <param name="x">Digest struct.</param>
		/// <param name="y">Digest struct.</param>
		/// <param name="pcc">double value the peak of cross correlation.</param>
		/// <param name="threshold">double value for the threshold value for which 2 images are considered the same or different.</param>
		/// <returns>int value - 1 (true) for same, 0 (false) for different, < 0 for error</returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ph_crosscorr(ref NativeStructures.Digest x, ref NativeStructures.Digest y, out double pcc, double threshold = 0.90);

		/// <summary>
		/// Compare 2 images given the file names.
		/// </summary>
		/// <param name="file1">char string of first image file.</param>
		/// <param name="file2">char string of second image file.</param>
		/// <param name="pcc">(out) double value for peak of cross correlation.</param>
		/// <param name="sigma">double value for deviation of gaussian filter.</param>
		/// <param name="gamma">double value for gamma correction of images.</param>
		/// <param name="N">int number for number of angles.</param>
		/// <param name="threshold"></param>
		/// <returns>return int 0 (false) for different image, 1 (true) for same images, less than 0 for error.</returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern int ph_compare_images(string file1, string file2, out double pcc, double sigma = 3.5, double gamma = 1.0, int N = 180, double threshold = 0.90);

		/// <summary>
		///  textual hash for file.
		/// </summary>
		/// <param name="filename">char* name of file.</param>
		/// <param name="nbpoints">int length of array of return value (out).</param>
		/// <returns>return TxtHashPoint* array of hash points with respective index into file.</returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern /*NativeStructures.TxtHashPoint[]*/ IntPtr ph_texthash(string filename, out int nbpoints);


		/// <summary>
		/// compare 2 text hashes.
		/// </summary>
		/// <param name="hash1">TxtHashPoint.</param>
		/// <param name="N1">int length of hash1.</param>
		/// <param name="hash2">TxtHashPoint.</param>
		/// <param name="N2">int length of hash2.</param>
		/// <param name="nbmatches">int number of matches found (out).</param>
		/// <returns>list of all matches.</returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern /*NativeStructures.TxtMatch[]*/ IntPtr ph_compare_text_hashes(/*NativeStructures.TxtHashPoint[]*/ IntPtr hash1, int N1, /*NativeStructures.TxtHashPoint[]*/ IntPtr hash2, int N2, out int nbmatches);

		/// <summary>
		/// create MH image hash for filename image.
		/// </summary>
		/// <param name="filename">string name of image file.</param>
		/// <param name="N">(out) int value for length of image hash returned.</param>
		/// <param name="alpha">int scale factor for marr wavelet (default=2).</param>
		/// <param name="lvl">int level of scale factor (default = 1).</param>
		/// <returns>return uint8_t array</returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		//[return: MarshalAs(UnmanagedType.SafeArray)]
		public static extern /*byte[]*/ IntPtr ph_mh_imagehash(string filename, out int N, float alpha = 2.0f, float lvl = 1.0f);

		/// <summary>
		/// compute hamming distance between two byte arrays.
		/// </summary>
		/// <param name="hashA">byte array for first hash.</param>
		/// <param name="lenA">int length of hashA.</param>
		/// <param name="hashB">byte array for second hash.</param>
		/// <param name="lenB">int length of hashB.</param>
		/// <returns>double value for normalized hamming distance</returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern double ph_hammingdistance2(/*byte[]*/ IntPtr hashA, int lenA, /*byte[]*/ IntPtr hashB, int lenB);

		/// <summary>
		///  copyright information
		/// </summary>
		/// <returns></returns>
		[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		public static extern string ph_about();


		//        /** /brief create a list of datapoint's directly from a directory of image files
		// *  /param dirname - path and name of directory containg all image file names
		// *  /param capacity - int value for upper limit on number of hashes
		// *  /param count - number of hashes created (out param)
		// *  /return pointer to a list of DP pointers (NULL for error)
		// */

		//[DllImport("pHash.dll", CallingConvention = CallingConvention.Cdecl)]
		//        DP** ph_read_imagehashes(const char *dirname,int capacity, int &count);

	}
}
