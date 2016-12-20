using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMusicApp;
using System.IO;
using Microsoft.CSharp;
using System.Media;
using System.Threading;
using System.Diagnostics;

namespace MusicPlayerTests
{
    [TestClass]
    public class MusicPlayerTest
    {
        private MusicPlayerModel m;

        [TestInitialize]
        public void ClassInit()
        {
            m = new MusicPlayerModel(@"TestResources\");
        }

        [TestMethod]
        [DeploymentItem(@"TestResources\Alarm01.wav", "TestResources")]
        public void CanFetchFile()
        {
            try
            {
                m.LoadSong(@"Alarm01.wav");
                //Thread.Sleep(3000);

                Assert.IsTrue(m.ModelSoundPlayer.IsLoadCompleted, "Resource could not load");
            }
            catch (FileNotFoundException e)
            {
                Trace.WriteLine(e.Message);
                Trace.WriteLine(e.FileName);
                Assert.IsFalse(true, "Failed to find resource file, or loadSong is failing.");
            }
        }

        [TestMethod]
        public void CannotFetchFile()
        {
            try
            {
                m.LoadSong(@"missing_file.wav");
            }
            catch (FileNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        [DeploymentItem(@"TestResources\TestDirectory\Alarm10.wav", @"TestResources\TestDirectory")]
        public void SetPathValid()
        {
            m.Path = m.Path + @"\TestDirectory\";
            m.LoadSong(@"Alarm10.wav");
        }

        [TestMethod]
        public void SetPathInvalid()
        {
            // this test aught to fail, since the deployment item was not included.
            bool result;
            try
            {
                m.Path = m.Path + @"\TestDirectory\TestDirectory2";
                result = false;
            }

            catch (DirectoryNotFoundException)
            {
                result = true;
            }

            Assert.IsTrue(result, "Uh oh, something bad happened.");
        }

        [TestMethod]
        [DeploymentItem(@"TestResources\Alarm01.wav", @"TestResources")]
        [DeploymentItem(@"TestResources\Alarm02.wav", @"TestResources")]
        [DeploymentItem(@"TestResources\Alarm03.wav", @"TestResources")]
        public void GetFilesValid()
        {
            string expectedResult = @"Alarm01.wav Alarm02.wav Alarm03.wav ";
            string actualResult = @"";
            foreach (string fileName in m.GetFiles())
            {
                actualResult += fileName + " ";
            }

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetFilesInvalid()
        {
            string expectedResult = @"Alarm01.wav Alarm02.wav Alarm03.wav ";
            string actualResult = @"";
            foreach (string fileName in m.GetFiles())
            {
                actualResult += fileName + " ";
            }

            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
