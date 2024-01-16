using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace HotelBooking.Architecture.Tests
{
    public class ProjectsDependenciesTests
    {
        private const string DomainNamespace = "HotelBooking.Domain";
        private const string ApplicationNamespace = "HotelBooking.Application";
        private const string DbNamespace = "HotelBooking.Db";
        private const string ApiNamespace = "HotelBooking.Api";

        [Fact]
        public void Domain_ShouldNot_HaveDependencyOnExternalExternalAssemblies()
        {
            // Arrange
            var domain = Assembly.Load(DomainNamespace);
            var externalAssemblies = new[] { DbNamespace, ApiNamespace };

            // Act
            var result = Types
                .InAssembly(domain)
                .ShouldNot()
                .HaveDependencyOnAll(externalAssemblies)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_ShouldNot_HaveDependencyOnExternalExternalAssemblies()
        {
            // Arrange
            var domain = Assembly.Load(ApplicationNamespace);
            var externalAssemblies = new[] { DbNamespace, ApiNamespace };

            // Act
            var result = Types
                .InAssembly(domain)
                .ShouldNot()
                .HaveDependencyOnAll(externalAssemblies)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Db_ShouldNot_HaveDependencyOnExternalExternalAssemblies()
        {
            // Arrange
            var domain = Assembly.Load(DbNamespace);

            // Act
            var result = Types
                .InAssembly(domain)
                .ShouldNot()
                .HaveDependencyOn(ApiNamespace)
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}