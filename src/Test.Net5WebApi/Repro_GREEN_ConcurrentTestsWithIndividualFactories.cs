using System.Threading.Tasks;
using Test.Net5WebApi.Setup;
using Xunit;

namespace Test.Net5WebApi
{
    /// <summary>
    /// This test is GREEN on both .NET5 and .NET6.
    /// <br/><br/>
    /// Create a separate factory for every test method and run the the tests concurrently with multiple test classes.
    /// Each test method gets its own factory instance.
    /// <see cref="MySlowStoppingHostedService"/> adds a 10 second delay in StopAsync.
    /// </summary>
    public class Repro_GREEN_ConcurrentTestsWithIndividualFactories
    {
        public class TestClass1 : TestsBase {}
        public class TestClass2 : TestsBase {}
        public class TestClass3 : TestsBase {}
        public class TestClass4 : TestsBase {}
        public class TestClass5 : TestsBase {}
        public class TestClass6 : TestsBase {}
        public class TestClass7 : TestsBase {}
        public class TestClass8 : TestsBase {}
        public class TestClass9 : TestsBase {}
        public class TestClass10 : TestsBase {}

        public abstract class TestsBase
        {
            [Fact] public Task Test1() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test2() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test3() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test4() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test5() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test6() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test7() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test8() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test9() => MyTest.RunTest(CreateFactory());
            [Fact] public Task Test10() => MyTest.RunTest(CreateFactory());

            private static MyWebApplicationFactory CreateFactory() => new();
        }
    }
}