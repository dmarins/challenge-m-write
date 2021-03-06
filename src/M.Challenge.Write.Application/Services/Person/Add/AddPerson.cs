﻿using M.Challenge.Write.Domain.Dtos;
using M.Challenge.Write.Domain.Logger;
using M.Challenge.Write.Domain.Persistence;
using M.Challenge.Write.Domain.Repositories.Person;
using M.Challenge.Write.Domain.Services.Person;
using System;
using System.Threading.Tasks;

namespace M.Challenge.Write.Application.Services.Person.Add
{
    public class AddPerson : IAddPersonService
    {
        public ILogger Logger { get; }
        public IPersonWritingRepository PersonWritingRepository { get; }
        public IUnitOfWork UnitOfWork { get; }

        public AddPerson(ILogger logger,
            IPersonWritingRepository personWritingRepository,
            IUnitOfWork unitOfWork)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            PersonWritingRepository = personWritingRepository ?? throw new ArgumentNullException(nameof(personWritingRepository));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ResultDto> Process(PersonCrudDto dto)
        {
            var person = new Domain.Entities.Person(
                dto.Name,
                dto.LastName,
                dto.Ethnicity,
                dto.Genre,
                dto.EducationLevel);

            for (int i = 0; i < 2; i++)
            {
                person.AddFilliation(dto.Filiation[i]);
            }

            foreach (var child in dto.Children)
            {
                person.AddChild(child);
            }

            var personStored = await PersonWritingRepository.Add(person);

            await UnitOfWork.Commit();

            return new CommandResultDto().Created(personStored);
        }
    }
}
