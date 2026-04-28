using AutoMapper;
using Nhs.PatientRegistry.Api.DTOs;
using Nhs.PatientRegistry.Api.Models;

namespace Nhs.PatientRegistry.Api.Mapping
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreateMap<Patient, PatientDetailsDto>();
        }
    }
}
