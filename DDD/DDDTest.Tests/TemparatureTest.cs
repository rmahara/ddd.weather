using DDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDTest.Tests
{
    [TestClass]
    public class TemparatureTest
    {
        [TestMethod]
        public void 小数点以下2桁でまるめて表示できる()
        {
            var t = new Temperature(12.3f);
            Assert.AreEqual(12.3f, t.Value);
            Assert.AreEqual("12.30℃", t.DisplayValue);
        }

        [TestMethod]
        public void 温度Equels()
        {
            var t1 = new Temperature(12.3f);
            var t2 = new Temperature(12.3f);

            Assert.AreEqual(true, t1.Equals(t2));
        }

        [TestMethod]
        public void 温度EquelsEquals()
        {
            var t1 = new Temperature(12.3f);
            var t2 = new Temperature(12.3f);

            Assert.AreEqual(true, t1 == t2);
        }

        [TestMethod]
        public void 値Equels()
        {
            float t1 = 12.3f;
            float t2 = 12.3f;

            Assert.AreEqual(true, t1.Equals(t2));
            Assert.AreEqual(true, t1 == t2);
        }
    }
}
