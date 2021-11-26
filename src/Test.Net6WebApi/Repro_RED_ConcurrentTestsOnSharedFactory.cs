using System.Threading.Tasks;
using Test.Net6WebApi.Setup;
using Xunit;

namespace Test.Net6WebApi
{
    /// <summary>
    /// Run 10 integration test classes concurrently, with 10 test methods each.
    /// Each test method reuses the factory created by its test class with <see cref="IClassFixture{TFixture}"/>.
    /// <see cref="MySlowStoppingHostedService"/> adds a 10 second delay in StopAsync.
    /// <br/><br/>
    /// This test is RED on .NET6, but green on .NET5.<br/>
    /// On Windows and .NET6, the test cleanup fails with TaskCanceledException (expected).<br/>
    /// On Ubuntu and .NET6, the test hangs.
    /// </summary>
    public class Repro_RED_ConcurrentTestsOnSharedFactory
    {
        public class TestClass1 : TestsBase { public TestClass1(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass2 : TestsBase { public TestClass2(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass3 : TestsBase { public TestClass3(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass4 : TestsBase { public TestClass4(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass5 : TestsBase { public TestClass5(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass6 : TestsBase { public TestClass6(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass7 : TestsBase { public TestClass7(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass8 : TestsBase { public TestClass8(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass9 : TestsBase { public TestClass9(MyWebApplicationFactory factory) : base(factory) { } }
        public class TestClass10 : TestsBase { public TestClass10(MyWebApplicationFactory factory) : base(factory) { } }

        public abstract class TestsBase : IClassFixture<MyWebApplicationFactory>
        {
            private readonly MyWebApplicationFactory _factoryPerTestClass;

            protected TestsBase(MyWebApplicationFactory factory)
            {
                _factoryPerTestClass = factory;
            }

            [Fact] public Task Test1() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test2() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test3() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test4() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test5() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test6() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test7() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test8() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test9() => MyTest.RunTest(_factoryPerTestClass);
            [Fact] public Task Test10() => MyTest.RunTest(_factoryPerTestClass);
        }
    }
}