﻿using Autofac;
using FluentAssertions;
using M.Challenge.Write.Domain.Repositories.Person;
using M.Challenge.Write.Infrastructure.Repositories.Person;
using Xunit;

namespace M.Challenge.Write.UnitTests.Api.Infrastructure.CompositionRoot
{
    public class RepositoriesModuleTests : IClassFixture<CompositionRootFixture>
    {
        private readonly CompositionRootFixture _fixture;

        public RepositoriesModuleTests(CompositionRootFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldResolvingIPersonReadingRepositoryAsPersonReadingRepository()
        {
            var instance = _fixture
                .Container
                .Resolve<IPersonReadingRepository>();

            instance
                .Should()
                .BeOfType<PersonReadingRepository>();
        }

        [Fact]
        public void ForIPersonReadingRepository_ShouldBeATransientInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<IPersonReadingRepository>();

            var instance2 = _fixture
                .Container
                .Resolve<IPersonReadingRepository>();

            instance1
                .Should()
                .NotBe(instance2);
            instance1
                .GetHashCode()
                .Should()
                .NotBe(instance2.GetHashCode());
        }

        [Fact]
        public void ShouldResolvingIPersonWritingRepositoryAsPersonWritingRepository()
        {
            var instance = _fixture
                .Container
                .Resolve<IPersonWritingRepository>();

            instance
                .Should()
                .BeOfType<PersonWritingRepository>();
        }

        [Fact]
        public void ForIPersonWritingRepository_ShouldBeATransientInstance()
        {
            var instance1 = _fixture
                .Container
                .Resolve<IPersonWritingRepository>();

            var instance2 = _fixture
                .Container
                .Resolve<IPersonWritingRepository>();

            instance1
                .Should()
                .NotBe(instance2);
            instance1
                .GetHashCode()
                .Should()
                .NotBe(instance2.GetHashCode());
        }
    }
}
