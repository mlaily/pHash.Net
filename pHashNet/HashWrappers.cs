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

namespace pHash.Net
{
	/// <summary>
	/// Wrap a hash result into an unified object,
	/// hiding the implementation details by storing the used algorithm at the same time.
	/// </summary>
	public abstract class ImageHash
	{
		public abstract HashAlgorithm Algorithm { get; }
	}

	public class DCTHash : ImageHash
	{
		public override HashAlgorithm Algorithm
		{
			get { return HashAlgorithm.DCT; }
		}

		public ulong Hash { get; protected set; }

		public DCTHash(ulong hash)
		{
			this.Hash = hash;
		}
	}

	public class RadialHash : ImageHash
	{
		public override HashAlgorithm Algorithm
		{
			get { return HashAlgorithm.Radial; }
		}

		public NativeStructures.Digest Digest { get; protected set; }

		public RadialHash(NativeStructures.Digest digest)
		{
			this.Digest = digest;
		}
	}

	public class MHHash : ImageHash
	{
		public override HashAlgorithm Algorithm
		{
			get { return HashAlgorithm.MH; }
		}

		public IntPtr BytesPtr { get; protected set; }
		public int ByteCount { get; protected set; }

		public MHHash(IntPtr bytesPtr, int count)
		{
			this.BytesPtr = bytesPtr;
			this.ByteCount = count;
		}
	}

	public class TextHash
	{
		public IntPtr HashPoints { get; protected set; }

		public int PointsCount { get; set; }

		public TextHash(IntPtr hashPoints, int count)
		{
			this.HashPoints = hashPoints;
			this.PointsCount = count;
		}
	}

}
