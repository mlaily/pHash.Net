using ConsoleApplication1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.InteropServices;

namespace pHashNetTests
{


	/// <summary>
	///This is a test class for NativeFunctionsTest and is intended
	///to contain all NativeFunctionsTest Unit Tests
	///</summary>
	[TestClass()]
	public class NativeFunctionsTest
	{

		[TestMethod]
		public void Initialization()
		{
			if (!System.IO.File.Exists(file1))
			{
				Assert.Fail(file1);
			}
			if (!System.IO.File.Exists(file2))
			{
				Assert.Fail(file2);
			}
			if (!System.IO.File.Exists(file3))
			{
				Assert.Fail(file3);
			}
			if (!System.IO.File.Exists(file4))
			{
				Assert.Fail(file4);
			}
		}

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

		private static readonly string dir = @"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\";
		private static readonly string file1 = dir + "1.jpg";
		private static readonly string file2 = dir + "2.jpg";

		private static readonly string file3 = dir + "3.txt";
		private static readonly string file4 = dir + "4.txt";

		/// <summary>
		///A test for ph_about
		///</summary>
		[TestMethod()]
		public void ph_aboutTest()
		{
			string actual;
			actual = NativeFunctions.ph_about();
			Assert.IsNotNull(actual);
		}

		/// <summary>
		///A test for ph_dct_imagehash
		///</summary>
		[TestMethod()]
		public void ph_dct_imagehashTest()
		{
			string file = file1;
			ulong hash = 42;
			int actual;
			actual = NativeFunctions.ph_dct_imagehash(file, out hash);
			Assert.AreNotEqual(42, hash);
			Assert.AreEqual(0, actual);
		}

		/// <summary>
		///A test for ph_hamming_distance
		///</summary>
		[TestMethod()]
		public void ph_hamming_distanceTest()
		{
			ulong hash1;
			NativeFunctions.ph_dct_imagehash(file1, out hash1);
			ulong hash2 = 0;
			NativeFunctions.ph_dct_imagehash(file2, out hash2);
			int actual;
			actual = NativeFunctions.ph_hamming_distance(hash1, hash2);
			Assert.AreNotEqual(0, actual);
		}

		/// <summary>
		///A test for ph_image_digest
		///</summary>
		[TestMethod()]
		public void ph_image_digestTest()
		{
			string file = file1;
			NativeStructures.Digest digest;
			int expected = 0;
			int actual;
			actual = NativeFunctions.ph_image_digest(file, 1.0f, 1.0f, out digest);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for ph_crosscorr
		///</summary>
		[TestMethod()]
		public void ph_crosscorrTest()
		{
			NativeStructures.Digest x;
			NativeFunctions.ph_image_digest(file1, 1.0f, 1.0f, out x);
			NativeStructures.Digest y;
			NativeFunctions.ph_image_digest(file2, 1.0f, 1.0f, out y);
			double pcc = 42;
			int actual;
			actual = NativeFunctions.ph_crosscorr(ref x, ref y, out pcc);
			Assert.AreNotEqual(42, pcc);
			Assert.AreEqual(0, actual);
		}

		/// <summary>
		///A test for ph_mh_imagehash
		///</summary>
		[TestMethod()]
		public void ph_mh_imagehashTest()
		{
			string filename = file1;
			int N = 0;
			IntPtr actual;
			actual = NativeFunctions.ph_mh_imagehash(filename, out N);
		}

		/// <summary>
		///A test for ph_compare_images
		///</summary>
		[TestMethod()]
		public void ph_compare_imagesTest()
		{
			double pcc = 42;
			int actual;
			actual = NativeFunctions.ph_compare_images(file1, file2, out pcc);
			Assert.AreNotEqual(42, pcc);
			Assert.AreEqual(0, actual);
		}

		/// <summary>
		///A test for ph_hammingdistance2
		///</summary>
		[TestMethod()]
		public void ph_hammingdistance2Test()
		{
			int lenA = 0;
			IntPtr hashA = NativeFunctions.ph_mh_imagehash(file1, out lenA);
			int lenB = 0;
			IntPtr hashB = NativeFunctions.ph_mh_imagehash(file2, out lenB);
			double actual;
			actual = NativeFunctions.ph_hammingdistance2(hashA, lenA, hashB, lenB);
			Assert.AreNotEqual(0, actual);
		}

		/// <summary>
		///A test for ph_texthash
		///</summary>
		[TestMethod()]
		public void ph_texthashTest()
		{
			string filename = file1;
			int nbpoints = 42;
			IntPtr actual;
			actual = NativeFunctions.ph_texthash(filename, out nbpoints);
			Assert.AreNotEqual(42, nbpoints);
		}

		/// <summary>
		///A test for ph_compare_text_hashes
		///</summary>
		[TestMethod()]
		public void ph_compare_text_hashesTest()
		{

			int N1 = 0;
			IntPtr hash1 = NativeFunctions.ph_texthash(file3, out N1);
			int N2 = 0;
			IntPtr hash2 = NativeFunctions.ph_texthash(file4, out N2);
			int nbmatches = 42;
			/*NativeStructures.TxtMatch[]*/
			IntPtr actual;
			actual = NativeFunctions.ph_compare_text_hashes(hash1, N1, hash2, N2, out nbmatches);

			NativeStructures.TxtMatch[] array = new NativeStructures.TxtMatch[nbmatches];
			for (int i = 0; i < array.Length; i++)
			{
				var x1 = Marshal.ReadInt32(actual);
				var x2 = Marshal.ReadInt32(actual, sizeof(int));
				var x3 = (uint)Marshal.ReadInt64(actual, sizeof(int) * 2);
				array[i] = new NativeStructures.TxtMatch() { first_index = x1, second_index = x2, length = x3 };
				//(NativeStructures.TxtMatch)Marshal.PtrToStructure(actual, typeof(NativeStructures.TxtMatch));
				actual = IntPtr.Add(actual, 12); //! hardcoded !
			}

			Assert.AreNotEqual(42, nbmatches);
		}
	}
}
