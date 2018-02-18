using Adapter.Usql;
using NUnit.Framework;

namespace Adapter.Tests.Usql
{
    [TestFixture]
    public class TableTypeTests
    {
        [TestCase("abc","abc")]
        [Test]
        public void ShouldGetDatabase(string database, string expected)
        {
            var actual = new TableTypeBuilder().Catalog(database);

            Assert.AreEqual(expected, actual);
        }
    }
}
