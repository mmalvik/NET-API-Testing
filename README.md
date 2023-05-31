# README

## Intro

This is sample repo for setting up API testing in .NET.

It includes:
- Mocking services
- Getting application logs into test output by using xUnit's `ITestOutputHelper`

Hopefully more stuff to come...

## Setup

1. Run the SQL Server compose file:

```shell
docker-compose -f docker-compose.sqlserver.yaml up
```

2. Build and run API through Visual Studio, Rider or command line.

## Resources

- [Docs: Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0)
- [David Fowler's gist](https://gist.github.com/davidfowl/0e0372c3c1d895c3ce195ba983b1e03d#testing-with-webapplicationfactorytestserver)

## Notes

### EF Core in-memory provider

The EF Core in-memory provider is NOT recommended for use in integration testing:


> EF Core also comes with an in-memory provider. Although this provider was originally designed to support internal testing of EF Core itself, some developers use it as a database fake when testing EF Core applications. Doing so is highly discouraged.


Source: https://docs.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy#in-memory-as-a-database-fake