using System;
using System.Collections.Generic;
using System.Text;
using NAudio;
using Xunit;
namespace FuTest.FuLibTest
{
  
    public class NAudioTest
    {
           [Fact]
        public void NAudioDurationTest()
        {
            string duration;
            NAudio.Wave.WaveFileReader wf = new NAudio.Wave.WaveFileReader(Environment.CurrentDirectory + @"\testfiles\GAudio_Receive4.wav");
            TimeSpan tp = wf.TotalTime;
            duration = tp.TotalSeconds.ToString();
            Console.WriteLine(duration);
        }
    }
}
