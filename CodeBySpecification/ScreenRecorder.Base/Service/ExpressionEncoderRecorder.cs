using System.IO;
using CodeBySpecification.API;
using Microsoft.Expression.Encoder.ScreenCapture;
using NReco.VideoConverter;

namespace ScreenRecorder.Base.Service
{
	public class ExpressionEncoderRecorder : IScreenRecordService
	{
		private ScreenCaptureJob scj;
		public string OutputFile { get; set; }

		public void Start()
		{
			scj = new ScreenCaptureJob();
			if (File.Exists(OutputFile))
				File.Delete(OutputFile);
			scj.OutputScreenCaptureFileName = OutputFile;
			scj.Start();
        }

		public void Stop()
		{
			scj.Stop();
            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
            ffMpeg.ConvertMedia(OutputFile, OutputFile.Replace(".wmv", ".mp4"), Format.mp4);
            ffMpeg.GetVideoThumbnail(OutputFile.Replace(".wmv", ".mp4"), OutputFile.Replace(".wmv", ".jpg"));
        }
	}
}