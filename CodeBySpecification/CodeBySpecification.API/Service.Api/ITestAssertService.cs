using System;

namespace CodeBySpecification.API.Service.Api
{
	public interface ITestAssertService
	{
		void Fail(string message);

		void IsEqual(object expected, object actual, string message = null);

		void IsEqual(string expected, string actual, string message = null);

		void IsEqual(int expected, int actual, string message = null);

		void IsEqual(double expected, double actual, string message = null);

		void IsEqual(decimal expected, decimal actual, string message = null);

		void IsEqual(float expected, float actual, string message = null);

		void IsNotEqual(object expected, object actual, string message = null);

		void IsNotEqual(string expected, string actual, string message = null);

		void IsNotEqual(int expected, int actual, string message = null);

		void IsNotEqual(double expected, double actual, string message = null);

		void IsNotEqual(decimal expected, decimal actual, string message = null);

		void IsNotEqual(float expected, float actual, string message = null);

		void IsNotNull(object element);

		void IsTrue(bool condition);

		void IsNotTrue(bool condition);

		void IsNull(object element);
	}
}
