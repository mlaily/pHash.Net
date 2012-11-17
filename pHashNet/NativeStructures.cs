using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
	public class NativeStructures
	{
		/// <summary>
		/// Digest info.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct Digest
		{
			/// <summary>
			/// hash id.
			/// </summary>
			public string id;
			/// <summary>
			/// uint8_t *coeffs;
			/// the head of the digest integer coefficient array.
			/// </summary>
			//[MarshalAs(UnmanagedType.ByValArray, SizeConst=5)]
			public /*byte[]*/ IntPtr coeffs;
			/// <summary>
			/// the size of the coeff array.
			/// </summary>
			public int size;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TxtHashPoint
		{
			public ulong hash;
			/// <summary>
			/// off_t index;
			/// pos of hash in orig file.
			/// </summary>
			public int index;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TxtMatch
		{
			/// <summary>
			/// off_t first_index;
			///  offset into first file.
			/// </summary>
			public int first_index;

			/// <summary>
			/// off_t second_index;
			/// offset into second file.
			/// </summary>
			public int second_index;
			/// <summary>
			/// length of match between 2 files.
			/// </summary>
			public uint length;
		}
	}
}
