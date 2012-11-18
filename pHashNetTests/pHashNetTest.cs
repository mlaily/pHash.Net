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

using pHash.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace pHashNetTests
{


	/// <summary>
	///This is a test class for pHashTest and is intended
	///to contain all pHashTest Unit Tests
	///</summary>
	[TestClass()]
	public class pHashNetTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		/// <summary>
		///A test for NormalizeThreshold
		///</summary>
		[TestMethod()]
		[DeploymentItem("pHashNet.dll")]
		public void NormalizeThresholdTest()
		{
			double zero = 0F;
			double one = 1F;
			double half = .5F;

			Assert.AreEqual(zero, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.MH, 0));
			Assert.AreEqual(zero, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.DCT, 0));
			Assert.AreEqual(zero, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.Radial, 1));

			Assert.AreEqual(one, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.MH, 1));
			Assert.AreEqual(one, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.DCT, 64));
			Assert.AreEqual(one, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.Radial, 0));

			Assert.AreEqual(half, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.MH, .5));
			Assert.AreEqual(half, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.DCT, 32));
			Assert.AreEqual(half, pHashNet_Accessor.NormalizeThreshold(HashAlgorithm.Radial, .5));
		}

		/// <summary>
		///A test for HashImage
		///</summary>
		[TestMethod()]
		public void HashImageTest()
		{
			string filePath = CommonTest.file1;
			ImageHash actual;

			actual =  pHashNet.HashImage(HashAlgorithm.DCT, filePath);
			Assert.IsInstanceOfType(actual, typeof(DCTHash));

			actual = pHashNet.HashImage(HashAlgorithm.Radial, filePath);
			Assert.IsInstanceOfType(actual, typeof(RadialHash));

			actual = pHashNet.HashImage(HashAlgorithm.MH, filePath);
			Assert.IsInstanceOfType(actual, typeof(MHHash));
		}


		/// <summary>
		///A test for CompareHashes
		///</summary>
		[TestMethod()]
		public void CompareHashesTest()
		{
			ImageHash hash1 = pHashNet.HashImage(HashAlgorithm.DCT, CommonTest.file1);
			ImageHash hash2 = pHashNet.HashImage(HashAlgorithm.DCT, CommonTest.file2);
			double actual;
			Assert.IsTrue((actual = pHashNet.CompareHashes(hash1, hash2)) > 0);

			hash1 = pHashNet.HashImage(HashAlgorithm.MH, CommonTest.file1);
			hash2 = pHashNet.HashImage(HashAlgorithm.MH, CommonTest.file2);
			Assert.IsTrue((actual = pHashNet.CompareHashes(hash1, hash2)) > 0);

			hash1 = pHashNet.HashImage(HashAlgorithm.Radial, CommonTest.file1);
			hash2 = pHashNet.HashImage(HashAlgorithm.Radial, CommonTest.file2);
			Assert.IsTrue((actual = pHashNet.CompareHashes(hash1, hash2)) > 0);

			//test the comparison of different hash algorithm
			hash1 = pHashNet.HashImage(HashAlgorithm.DCT, CommonTest.file1);
			hash2 = pHashNet.HashImage(HashAlgorithm.MH, CommonTest.file2);
			try
			{
				pHashNet.CompareHashes(hash1, hash2);
				Assert.Fail("Should have thrown an exception.");
			}
			catch (ArgumentException) { }
			catch (Exception)
			{
				Assert.Fail("Unexpected exception");
			}
		}

		/// <summary>
		///A test for HashText
		///</summary>
		[TestMethod()]
		public void HashTextTest()
		{
			string filePath = CommonTest.file3;
			TextHash actual;
			actual = pHashNet.HashText(filePath);
			Assert.AreNotEqual(IntPtr.Zero, actual.HashPoints);
			Assert.IsTrue(actual.PointsCount > 0);
		}

		/// <summary>
		///A test for CompareTextHashes
		///</summary>
		[TestMethod()]
		public void CompareTextHashesTest()
		{
			TextHash hash1 = pHashNet.HashText(CommonTest.file3);
			TextHash hash2 = pHashNet.HashText(CommonTest.file4);
			NativeStructures.TxtMatch[] actual;
			actual = pHashNet.CompareTextHashes(hash1, hash2);
			Assert.IsNotNull(actual);
			Assert.IsTrue(actual.Length > 1);
		}
	}
}
