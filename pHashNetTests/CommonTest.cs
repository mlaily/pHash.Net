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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace pHashNetTests
{
	/// <summary>
	/// Summary description for CommonTest
	/// </summary>
	[TestClass]
	public class CommonTest
	{

		public static readonly string dir = @"D:\Mes Documents\CodeSource\HG\pHashNet\pHashNetTests\";
		public static readonly string file1 = dir + "1.jpg";
		public static readonly string file2 = dir + "2.jpg";

		public static readonly string file3 = dir + "3.txt";
		public static readonly string file4 = dir + "4.txt";

		public CommonTest()
		{
			//
			// TODO: Add constructor logic here
			//
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
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

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
	}
}
