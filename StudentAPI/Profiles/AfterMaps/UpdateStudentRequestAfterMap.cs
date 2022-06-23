using AutoMapper;
using StudentAPI.DomainModels;
using Models = StudentAPI.Models;

namespace StudentAPI.Profiles.AfterMaps
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, Models.Student>
    {
        public void Process(UpdateStudentRequest source, Models.Student destination, ResolutionContext context)
        {
            destination.Address = new Models.Address()
            {
                PhysicalAdress = source.PhysicalAdress,
                PostalAdress = source.PostalAdress,

            };
        }
    }
}
