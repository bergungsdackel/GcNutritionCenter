using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDMA.Core.TestData
{
    public class TestDataGenerator
    {
        public TestDataGenerator()
        {

        }

        public static IList<T> GetListOfTestData<T>(int size)
        {
            return Builder<T>.CreateListOfSize(size).Build();
        }
    }
}
