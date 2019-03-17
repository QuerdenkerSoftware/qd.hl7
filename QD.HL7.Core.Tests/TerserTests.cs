using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QD.HL7.Core.Tests {
    [TestClass]
    public class TerserTests {
        [TestMethod]
        public void TerserEmptyMessageInitTest() {
            var msg = new Parser().Parse(string.Empty);
            var t = new Terser(msg);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void WriteFullMessageTest() {
            var msg = new Parser().Parse(string.Empty);
            var t = new Terser(msg);


            //MSH
            t.Set(@"^~\&", "MSH-2");
            t.Set("7edit", "MSH-3");
            t.Set("7edit", "MSH-5");
            t.Set(DateTime.Now.ToString("yyyyMMddHHmmss"), "MSH-7");
            t.Set("BAR", "MSH-9-1");
            t.Set("P01", "MSH-9-2");
            //EVN
            t.Set("P01", "EVN-1");
            t.Set(DateTime.Now.ToString("yyyyMMddHHmmss"), "EVN-2");
            //PID

            //PV1

            //DG1
            t.Set("1", "DG1(1)-1");
            t.Set("2", "DG1(2)-1");
            //PR1
            var hl7 = msg.ToString();
        }
    }
}