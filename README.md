# README

## Intro

This is a sample repo for setting up API testing in .NET.

It includes:
- Demonstrating mocking services
- Getting application logs into test output by using xUnit's `ITestOutputHelper`
- Using Testcontainers to run tests against a real SQL Server

The project contains 2 different APIs and 2 different test projects
- `NetWebApi` and `Test.NetWebApi` using the regular Microsoft provided dependency injection container
- `NetWebApi.LightInject` and `Test.NetWebApi.LightInject` using the [LightInject](https://github.com/seesharper/LightInject/tree/master) dependency injection container

## Setup

### Running the API

1. Run the SQL Server compose file:

```shell
docker-compose up -d
```

2. Build and run API through Visual Studio, Rider or command line.

### Running the tests

1. Make sure you have Docker running.

2. Run the tests in either `Test.NetWebApi` or `Test.NetWebApi.LightInject`

## Resources

- [Docs: Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0)
- [David Fowler's gist](https://gist.github.com/davidfowl/0e0372c3c1d895c3ce195ba983b1e03d#testing-with-webapplicationfactorytestserver)
- [What is Testcontainers, and why should you use it?](https://testcontainers.com/guides/introducing-testcontainers/)

## Notes

### EF Core in-memory provider

The EF Core in-memory provider is NOT recommended for use in integration testing:


> EF Core also comes with an in-memory provider. Although this provider was originally designed to support internal testing of EF Core itself, some developers use it as a database fake when testing EF Core applications. Doing so is highly discouraged.


Source: https://docs.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy#in-memory-as-a-database-fake