using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pHashNet
{
	public class pHash
	{

		protected double NormalizeThreshold(HashAlgorithm usedAlgorithm, double value)
		{
			switch (usedAlgorithm)
			{
				case HashAlgorithm.DCT:
					return value / 64f; //sizeof(ulong);
				case HashAlgorithm.Radial:
					return value * -1f + 1f; //0 for totally different images, 1 for identical
				case HashAlgorithm.MH:
					return value;
				default:
					throw new NotImplementedException();
			}
		}

	}

	public enum HashAlgorithm
	{
		/// <summary>
		/// Discrete Cosine Transform hashing (recommended).
		/// </summary>
		DCT,
		/// <summary>
		/// Radial hashing.
		/// </summary>
		Radial,
		/// <summary>
		/// Mexican Hat wavelet hashing.
		/// </summary>
		MH,
	}
}
