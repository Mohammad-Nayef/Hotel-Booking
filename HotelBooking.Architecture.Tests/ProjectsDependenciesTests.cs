using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace HotelBooking.Architecture.Tests
{
    public class ProjectsDependenciesTests
    {
        private const string DomainNamespace = "HotelBooking.Domain";
        private const string ApplicationNamespace = "HotelBooking.Application";
        private const string InfrastructureNamespace = "HotelBooking.Infrastructure";
        private const string ApiNamespace = "HotelBooking.Api";

        [Fact]
        public void Domain_ShouldNotHaveDependencyOn_ExternalAssemblies()
        {
            // Arrange
            var domain = Assembly.Load(DomainNamespace);
            var externalAssemblies = new[] { InfrastructureNamespace, ApiNamespace };

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
        public void Application_ShouldNotHaveDependencyOn_ExternalAssemblies()
        {
            // Arrange
            var domain = Assembly.Load(ApplicationNamespace);
            var externalAssemblies = new[] { InfrastructureNamespace, ApiNamespace };

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
        public void Infrastructure_ShouldNotHaveDependencyOn_ExternalAssemblies()
        {
            // Arrange
            var domain = Assembly.Load(InfrastructureNamespace);

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