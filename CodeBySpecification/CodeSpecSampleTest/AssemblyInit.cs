using System.Diagnostics;
using CodeBySpecification.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeSpecSampleTest
{
	internal class AssemblyInit
	{
		[AssemblyInitialize]
		public void Initialize()
		{
			Trace.WriteLine("Initializing Feature Base");
			var baseFeature = new FeatureBase();
			Trace.WriteLine(string.Format("Instantiated {0}", baseFeature));
		}
	}
}
