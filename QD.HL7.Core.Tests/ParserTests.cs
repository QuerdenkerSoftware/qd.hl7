using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QD.HL7.Core.Tests {
    [TestClass]
    public class ParserTests {
        [TestMethod]
        public void ReadTest() {
            var parser = new Parser();
            var msg = parser.Parse(Resource1.BARP01);

            var terser = new Terser(msg);

            var value = terser.Get("DG1-3-3");

            Assert.AreEqual("OPS2", value);
        }


        [TestMethod]
        public void WriteTest() {
            var parser = new Parser();
            var msg = parser.Parse(Resource1.BARP01);

            var terser = new Terser(msg);

            terser.Set("OPS2", "DG1-3-3");
        }
    }
}