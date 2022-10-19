using DDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
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
    }
}
