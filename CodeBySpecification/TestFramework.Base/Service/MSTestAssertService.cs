using CodeBySpecification.API.Service.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFramework.Base.Service
{
	public class MSTestAssertService : ITestAssertService
	{
		public void Fail(string message)
		{
			Assert.Fail(message);
		}

		public void IsEqual(object expected, object actual, string message = null)
		{
			Assert.AreEqual(expected, actual, message);
		}

		public void IsEqual(string expected, string actual, string message = null)
		{
			Assert.AreEqual(expected, actual, message);
		}

		public void IsEqual(int expected, int actual, string message = null)
		{
			Assert.AreEqual(expected, actual, message);
		}

		public void IsEqual(double expected, double actual, string message = null)
		{
			Assert.AreEqual(expected, actual, message);
		}

		public void IsEqual(decimal expected, decimal actual, string message = null)
		{
			Assert.AreEqual(expected, actual, message);
		}

		public void IsEqual(float expected, float actual, string message = null)
		{
			Assert.AreEqual(expected, actual, message);
		}

		public void IsNotEqual(object expected, object actual, string message = null)
		{
			Assert.AreNotEqual(expected, actual, message);
		}

		public void IsNotEqual(string expected, string actual, string message = null)
		{
			Assert.AreNotEqual(expected, actual, message);
		}

		public void IsNotEqual(int expected, int actual, string message = null)
		{
			Assert.AreNotEqual(expected, actual, message);
		}

		public void IsNotEqual(double expected, double actual, string message = null)
		{
			Assert.AreNotEqual(expected, actual, message);
		}

		public void IsNotEqual(decimal expected, decimal actual, string message = null)
		{
			Assert.AreNotEqual(expected, actual, message);
		}

		public void IsNotEqual(float expected, float actual, string message = null)
		{
			Assert.AreNotEqual(expected, actual, message);
		}

		public void IsNotNull(object element)
		{
			Assert.IsNotNull(element);
		}

		public void IsNull(object element)
		{
			Assert.IsNull(element);
		}
	}
}
