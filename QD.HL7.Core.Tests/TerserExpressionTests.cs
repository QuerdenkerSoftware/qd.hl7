using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QD.HL7.Core.Tests {
    [TestClass]
    public class TerserExpressionTests {
        [TestMethod]
        public void TerserExpressionTest() {
            const string value = "DG1-1-1";
            TerserExpression t = value;
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetSegmentNameTest() {
            const string value = "DG1-1-1";
            TerserExpression t = value;
            var segment = t.GetSegmentName();

            Assert.AreEqual("DG1", segment);
        }

        [TestMethod]
        public void GetSegmentNameTest_WithRep() {
            const string value = "DG1(1)-1-1";
            TerserExpression t = value;
            var segment = t.GetSegmentName();

            Assert.AreEqual("DG1", segment);
        }

        [TestMethod]
        public void IsRepetitionTest() {
            const string value = "DG1(1)-1-1";
            TerserExpression t = value;
            var isRepetition = t.IsRepetition();

            Assert.IsTrue(isRepetition);
        }

        [TestMethod]
        public void GetRepetitionTest() {
            const string value = "DG1(1)-1-1";
            TerserExpression t = value;
            var repetition = t.GetRepetition();

            Assert.IsTrue(repetition == 1);
        }

        [TestMethod]
        public void GetIndicesTest() {
            const string value = "DG1-1-2";
            TerserExpression t = value;
            var indices = t.GetIndices();

            Assert.IsTrue(indices.Count == 2);
            Assert.AreEqual(1, indices[0]);
            Assert.AreEqual(2, indices[1]);
        }

        [TestMethod]
        public void GetIndicesTest_WithRep() {
            const string value = "DG1(1)-1-2";
            TerserExpression t = value;
            var indices = t.GetIndices();

            Assert.IsTrue(indices.Count == 2);
            Assert.AreEqual(1, indices[0]);
            Assert.AreEqual(2, indices[1]);
        }
    }
}