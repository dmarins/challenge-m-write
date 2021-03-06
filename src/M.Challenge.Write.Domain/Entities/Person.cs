﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace M.Challenge.Write.Domain.Entities
{
    public class Person
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Ethnicity { get; set; }
        public string Genre { get; set; }
        public ICollection<Person> Filiation { get; set; }
        public ICollection<Person> Children { get; set; }
        public string EducationLevel { get; set; }

        public Person(string name,
            string lastName,
            string ethnicity,
            string genre,
            string educationLevel)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Ethnicity = ethnicity ?? throw new ArgumentNullException(nameof(ethnicity));
            Genre = genre ?? throw new ArgumentNullException(nameof(genre));
            EducationLevel = educationLevel ?? throw new ArgumentNullException(nameof(educationLevel));

            Filiation = new List<Person>();
            Children = new List<Person>();
        }

        public Person AddFilliation(Person fatherOrMother)
        {
            fatherOrMother.Id = Guid.NewGuid().ToString();

            Filiation.Add(fatherOrMother);

            return this;
        }

        public Person AddChild(Person sonOrDaughter)
        {
            sonOrDaughter.Id = Guid.NewGuid().ToString();

            Children.Add(sonOrDaughter);

            return this;
        }
    }
}
