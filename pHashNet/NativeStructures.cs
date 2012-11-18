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

			private byte[] _Coeffs;
			public byte[] Coeffs
			{
				get
				{
					if (_Coeffs == null)
					{
						_Coeffs = new byte[size];
						for (int i = 0; i < size; i++)
						{
							Coeffs[i] = Marshal.ReadByte(coeffs, i);
						}
					}
					return _Coeffs;
				}
			}
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
