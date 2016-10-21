using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBySpecification.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SampleProject
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
