using System;
using CodeBySpecification.API;
using Microsoft.Expression.Encoder.ScreenCapture;

namespace ScreenRecorder.Base.Service
{
	public class ExpressionEncoderRecorder : IScreenRecordService
	{
		private ScreenCaptureJob scj;
		public string OutputFile { get; set; }

		public void Start()
		{
			scj = new ScreenCaptureJob();
			scj.OutputScreenCaptureFileName = OutputFile;
			scj.Start();
		}

		public void Stop()
		{
			scj.Stop();
		}
	}
}