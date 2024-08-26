using AutoMapper;
using EduAdmin.DTOs.AdmitidoComo;
using EduAdmin.DTOs.Carrera;
using EduAdmin.DTOs.Estudiante;
using EduAdmin.Models;

namespace EduAdmin.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Estudiante
            CreateMap<EstudianteCreacionDto, Estudiante>();
            CreateMap<Estudiante, EstudianteDTO>();

            //Carrera
            CreateMap<CarreraCreacionDTO, Carrera>();
            CreateMap<Carrera, CarreraDTO>();

            //AdmitidoComo
            CreateMap<AdmitidoComoCreacionDTO, AdmitidoComo>();
            CreateMap<AdmitidoComo, AdmitidoComoDTO>();
        }
    }

}
