using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Armyknife.TestUtilities
{
    public static class AssertExtra
    {
        public static void AreEqual(byte[] expected, byte[] actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);

            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
