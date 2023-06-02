using Xunit;

namespace Test.NetWebApi.Infrastructure;

/// <summary>
/// Test collection for API tests.
/// For more on test collections see https://xunit.net/docs/running-tests-in-parallel.html
/// </summary>
[CollectionDefinition(nameof(ApiTestCollection))]
public class ApiTestCollection : ICollectionFixture<SqlServerTestFixture>
{
    
}