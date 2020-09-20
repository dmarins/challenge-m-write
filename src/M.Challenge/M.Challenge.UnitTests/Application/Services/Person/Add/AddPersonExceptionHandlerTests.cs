﻿using AutoFixture.Idioms;
using FluentAssertions;
using M.Challenge.Application.Services.Person.Add;
using M.Challenge.Domain.Dtos;
using M.Challenge.UnitTests.Config.AutoData;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace M.Challenge.UnitTests.Application.Services.Person.Add
{
    public class AddPersonExceptionHandlerTests
    {
        [Theory, AutoNSubstituteData]
        public void GuardandoOConstrutor(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(AddPersonExceptionHandler).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public async Task QuandoNaoHaExcecao(AddPersonExceptionHandler sut,
            PersonCrudDto dto)
        {
            var commandResultDto = new CommandResultDto().Success();

            sut.Decorated
                .Process(Arg.Any<PersonCrudDto>())
                .Returns(commandResultDto);

            var result = await sut.Process(dto);

            result.Data.Should().BeNull();
            result.Message.Should().BeNull();
            result.ReturnType.Should().Be(ReturnType.Success);

            sut.Logger
                .Received(0)
                .Error(Arg.Any<string>(), Arg.Any<Exception>());
        }

        [Theory, AutoNSubstituteData]
        public async Task QuandoHaExcecao(AddPersonExceptionHandler sut,
            PersonCrudDto dto)
        {
            var commandResultDto = new CommandResultDto().Fail();

            sut.Decorated
                .Process(Arg.Any<PersonCrudDto>())
                .Throws<Exception>();

            var result = await sut.Process(dto);

            result.Data.Should().BeNull();
            result.Message.Should().Be(commandResultDto.Message);
            result.ReturnType.Should().Be(ReturnType.Fail);

            sut.Logger
                .Received(1)
                .Error("Erro no cadastro de pessoa.", Arg.Any<Exception>());
        }
    }
}
