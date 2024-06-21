using AirportRenovate.Server.DTOs;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Utilities;
using AirportRenovate.Server.ViewModels;
using AutoMapper;

namespace AirportRenovate.Server.Mappings
{
    public class DeletedRecordsMapping : Profile
    {
        public DeletedRecordsMapping()
        {
            CreateMap<Money3DbModel, DeletedRecordsDto>()
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text != null ? src.Text.Trim() : ""))
            .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note != null ? src.Note.Trim() : ""))
            .ForMember(dest => dest.People, opt => opt.MapFrom(src => src.People != null ? src.People.Trim() : ""))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name.Trim() : ""))
            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks != null ? src.Remarks.Trim() : ""))
            .ForMember(dest => dest.People1, opt => opt.MapFrom(src => src.People1 != null ? src.People1.Trim() : ""))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status != null ? src.Status.Trim() : ""))
            .ForMember(dest => dest.Group1, opt => opt.MapFrom(src => src.Group1 != null ? src.Group1.Trim() : ""))
            .ForMember(dest => dest.All, opt => opt.MapFrom(src => src.All != null ? src.All.Trim() : ""))
            .ForMember(dest => dest.True, opt => opt.MapFrom(src => src.True != null ? src.True.Trim() : ""))
            .ForMember(dest => dest.Year1, opt => opt.MapFrom(src => src.Year1 != null ? src.Year1.Trim() : ""));
        }
    }
}
