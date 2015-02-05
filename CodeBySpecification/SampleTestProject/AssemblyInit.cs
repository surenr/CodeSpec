using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBySpecification.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SampleTestProject
{
	internal class AssemblyInit
	{
		[AssemblyInitialize]
		public void Initialize()
		{
			Trace.WriteLine("Initializing System.Web.Providers");
			var dummy = new FeatureBase();
			Trace.WriteLine(string.Format("Instantiated {0}", dummy));
		}
	}
}
