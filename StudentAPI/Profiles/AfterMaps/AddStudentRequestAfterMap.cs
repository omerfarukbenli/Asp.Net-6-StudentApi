﻿using AutoMapper;
using StudentAPI.DomainModels;

namespace StudentAPI.Profiles.AfterMaps
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, Models.Student>
    {
        public void Process(AddStudentRequest source, Models.Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new Models.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAdress = source.PhysicalAdress,
                PostalAdress = source.PostalAdress

            };
        }
    }
}


        