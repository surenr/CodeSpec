using System;
using CodeBySpecification.API;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using OpenQA.Selenium;

namespace ScreenRecorder.Base.Service
{
	public class BitmapRecorder : IScreenRecordService
	{
        Thread childThread;
        private static bool isRecording;
        private ITakesScreenshot takesScreenshot;
        //NReco.VideoConverter.FFMpegConverter ffMpeg = new NReco.VideoConverter.FFMpegConverter();

        public string OutputFile { get; set; }

        public BitmapRecorder()
        {

        }

        public BitmapRecorder(ITakesScreenshot takesScreenshot)
        {
            this.takesScreenshot = takesScreenshot;
        }

        public void Start()
		{
            ThreadStart childref = new ThreadStart(RecorderThread);
            Console.WriteLine("In Main: Creating the Child thread");
            childThread = new Thread(childref);
            isRecording = true;
            childThread.Start();
            
        }

        public void RecorderThread()
        {
            while (isRecording)
            {
                Thread.Sleep(50);
                CaptureScreen();
                CaptureBrowser();
            }
            
            Console.WriteLine("Child thread starts");
        }

        public void CaptureScreen()
        {
            System.Drawing.Rectangle screenSize = Screen.PrimaryScreen.Bounds;
            Bitmap target = new Bitmap(screenSize.Width, screenSize.Height);
            //ffMpeg.ConcatMedia;

            using (Graphics g = Graphics.FromImage(target))
            {
                g.CopyFromScreen(0, 0, 0, 0, new System.Drawing.Size(screenSize.Width, screenSize.Height));
            }
            target.Save(OutputFile.Replace(".wmv", "_screenshot_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-fff-tt}", DateTime.Now) + ".bmp"));
        }

        public void CaptureBrowser()
        {
            var screenshot = takesScreenshot.GetScreenshot();
            string outputFilePath = OutputFile.Replace(".wmv", "_browsershot_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-ffff-tt}", DateTime.Now) + ".png");
            screenshot.SaveAsFile(outputFilePath, ImageFormat.Png);
        }

        public void Stop()
		{
            isRecording = false;
        }
        
    }
}