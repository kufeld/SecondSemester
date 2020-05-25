using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCipher;

namespace MyCipherUnitTest
{
    [TestClass]
    public class MyCipherTestClass
    {
        [TestMethod]
        public void LineProcessingTestMethod()
        {
            var line = "I love Rammstein!";
            var result = Program.LineProcessing(line);
            Assert.AreEqual("iloveramstein", result);

            line = "Hello, my name is Dima!";
            result = Program.LineProcessing(line);
            Assert.AreEqual("helomynameisdima", result);
        }

        [TestMethod]
        public void DecipherTestMethod()
        {
            var line = "lliqqloqqeevllemmdaaonngsss";
            var result = Program.Decipher(line);
            Assert.AreEqual("ilovedogs", result);
        }

        [TestMethod]
        public void CipherAndDecipherTestMethod()
        {
            var line = "Hello, my name is Dima";
            var decipher = Program.LineProcessing(line);
            var cipher = Program.Cipher(decipher);
            Assert.AreEqual(decipher, Program.Decipher(cipher));
        }
    }
}
