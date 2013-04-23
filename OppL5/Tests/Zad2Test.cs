using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zad2;

namespace Tests
{
    [TestClass]
    public class Zad2Test
    {
        #region Public methods

        [TestMethod]
        public void TestStreamReadPositiveShift()
        {
            const int N_SAMPLES = 100;

            var rnd = new Random();

            var bufferLength = rnd.Next(100, 1000);
            var testBytes = new byte[bufferLength];
            var resultBytes = new byte[bufferLength];

            for (var i = 0; i < N_SAMPLES; i++)
            {
                var shift = rnd.Next(0, byte.MaxValue*3);
                rnd.NextBytes(testBytes);
                var expectedBytes = testBytes.Select(b => (byte) ((b + shift)%(byte.MaxValue + 1))).ToArray();
                using (var mStream = new MemoryStream(testBytes))
                {
                    var caesar = new CaesarStream(mStream, shift);
                    caesar.Read(resultBytes, 0, bufferLength);
                    CollectionAssert.AreEqual(resultBytes, expectedBytes);
                }
            }
        }

        [TestMethod]
        public void TestStreamReadWriteDuality()
        {
            const int N_SAMPLES = 100;

            var rnd = new Random();

            var bufferLength = rnd.Next(100, 1000);
            var testBytes = new byte[bufferLength];
            var resultBytes = new byte[bufferLength];

            for (var i = 0; i < N_SAMPLES; i++)
            {
                var shift = rnd.Next(0, byte.MaxValue*3);
                rnd.NextBytes(testBytes);
                using (MemoryStream sourceStream = new MemoryStream(testBytes),
                                    destStream = new MemoryStream())
                using (CaesarStream caeToRead = new CaesarStream(sourceStream, shift),
                                    caeToWrite = new CaesarStream(destStream, -shift))
                {
                    caeToRead.Read(resultBytes, 0, bufferLength);
                    caeToWrite.Write(resultBytes, 0, bufferLength);

                    destStream.Seek(0, SeekOrigin.Begin);
                    destStream.Read(resultBytes, 0, bufferLength);
                    CollectionAssert.AreEqual(resultBytes, testBytes);
                }
            }
        }

        [TestMethod]
        public void TestStreamWritePositiveShift()
        {
            const int N_SAMPLES = 100;

            var rnd = new Random();

            var bufferLength = rnd.Next(100, 1000);
            var testBytes = new byte[bufferLength];
            var resultBytes = new byte[bufferLength];

            for (var i = 0; i < N_SAMPLES; i++)
            {
                var shift = rnd.Next(0, byte.MaxValue*3);
                rnd.NextBytes(testBytes);
                var expectedBytes = testBytes.Select(b => (byte) ((b + shift)%(byte.MaxValue + 1))).ToArray();
                using (var mStream = new MemoryStream())
                using (var caesar = new CaesarStream(mStream, shift))
                {
                    caesar.Write(testBytes, 0, bufferLength);

                    mStream.Seek(0, SeekOrigin.Begin);
                    mStream.Read(resultBytes, 0, bufferLength);
                    CollectionAssert.AreEqual(resultBytes, expectedBytes);
                }
            }
        }

        #endregion
    }
}