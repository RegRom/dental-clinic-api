using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DentalClinicBLL.Models;
using DentalClinicDAL.Entities;

namespace DentalClinicBLL.MapperProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientDto, Patient>()
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Patient, Patient>();
            CreateMap<PatientDto, PatientDto>();
        }
    }
}
