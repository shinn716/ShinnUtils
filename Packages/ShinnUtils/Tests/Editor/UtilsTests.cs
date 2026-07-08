using System.Linq;
using NUnit.Framework;

namespace Shinn.Tests
{
    public class UtilsTests
    {
        [Test]
        public void GetDegree_WrapsAbove180IntoNegative()
        {
            Assert.AreEqual(10f, Utils.GetDegree(370f), 1e-4f);
            Assert.AreEqual(-170f, Utils.GetDegree(190f), 1e-4f);
            Assert.AreEqual(180f, Utils.GetDegree(540f), 1e-4f);
            Assert.AreEqual(0f, Utils.GetDegree(0f), 1e-4f);
        }

        [Test]
        public void NonrepetitiveRandom_IsPermutationOfRange()
        {
            const int n = 50;
            int[] result = Utils.NonrepetitiveRandom(n);

            Assert.AreEqual(n, result.Length);
            CollectionAssert.AreEquivalent(Enumerable.Range(0, n).ToArray(), result);
        }

        [Test]
        public void Compare2Array_MatchesByValueAndLength()
        {
            Assert.IsTrue(Utils.Compare2Array(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }));
            Assert.IsFalse(Utils.Compare2Array(new[] { 1, 2, 3 }, new[] { 1, 2, 4 }));
            Assert.IsFalse(Utils.Compare2Array(new[] { 1, 2 }, new[] { 1, 2, 3 }));
        }

        [Test]
        public void Compare2ArrayValue_RespectsSign()
        {
            int[] values = { 5, 6, 7 };
            Assert.IsTrue(Utils.Compare2ArrayValue(values, 4, Utils.SignType.GREATER));
            Assert.IsFalse(Utils.Compare2ArrayValue(values, 6, Utils.SignType.GREATER));
            Assert.IsTrue(Utils.Compare2ArrayValue(values, 8, Utils.SignType.LESS));
        }

        [Test]
        public void FindCountOfStateInBoolArray_CountsMatches()
        {
            bool[] flags = { true, false, true, true };
            Assert.AreEqual(3, Utils.FindCountOfStateInBoolArray(flags, true));
            Assert.AreEqual(1, Utils.FindCountOfStateInBoolArray(flags, false));
        }

        [Test]
        public void CreateUUID_Is32HexCharsWithoutDashes()
        {
            string uuid = Utils.CreateUUID();
            Assert.AreEqual(32, uuid.Length);
            Assert.IsFalse(uuid.Contains("-"));
        }
    }

    public class ConverterTests
    {
        [Test]
        public void IntHex_RoundTrips()
        {
            Assert.AreEqual("ff", Converter.IntToHex(255));
            Assert.AreEqual(255, Converter.HexToInt("ff"));
            Assert.AreEqual(4660, Converter.HexToInt("1234"));
        }

        [Test]
        public void StringBytes_RoundTrips()
        {
            byte[] bytes = Converter.StringToBytes("4A3F");
            Assert.AreEqual(new byte[] { 0x4A, 0x3F }, bytes);
            Assert.AreEqual("4A3F", Converter.BytesToString(bytes));
        }
    }
}
