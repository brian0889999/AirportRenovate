using AirportRenovate.Server.DTOs;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Utilities;
using AirportRenovate.Server.ViewModels;
using AutoMapper;

namespace AirportRenovate.Server.Mappings
{
    public class PrivilegeMapping: Profile
    {
        public PrivilegeMapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name.Trim() : ""))
                .ForMember(dest => dest.Account, opt => opt.MapFrom(src => src.Account != null ? src.Account.Trim() : ""))
                //.ForMember(dest => dest.Password, opt => opt.Ignore()) // 不在這裡處理Password
                .ForMember(dest => dest.Auth, opt => opt.MapFrom(src => src.Auth != null ? src.Auth.Trim() : ""))
                .ForMember(dest => dest.Status1, opt => opt.MapFrom(src => src.Status1 != null ? src.Status1.Trim() : ""))
                .ForMember(dest => dest.Status2, opt => opt.MapFrom(src => src.Status2 != null ? src.Status2.Trim() : ""))
                .ForMember(dest => dest.Status3, opt => opt.MapFrom(src => src.Status3 != null ? src.Status3.Trim() : ""));
            // New mapping for AddUser and UpdateUser
            CreateMap<UserDto, User>()
                //.ForMember(dest => dest.Password, opt => opt.Ignore()) // 不在這裡處理Password
                .ForMember(dest => dest.Status1, opt => opt.MapFrom(src => src.Status1 != null ? src.Status1.Trim() : ""))
                //.ForMember(dest => dest.Status2, opt => opt.MapFrom(src => src.Status2 != null ? src.Status2.Trim() : ""))
                .ForMember(dest => dest.Status3, opt => opt.MapFrom(src => src.Status3 != null ? src.Status3.Trim() : ""))
                .ForMember(dest => dest.Status2, opt => opt.MapFrom(src => "O")) // 預設值
                .ForMember(dest => dest.Account_Open, opt => opt.MapFrom(src => "y")) // 預設值
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => new DateTime(1990, 1, 1, 0, 0, 0))) // 預設值
                .ForMember(dest => dest.Time1, opt => opt.MapFrom(src => DateTime.Now)) // 當前時間
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => 0)) // 預設值
                .ForMember(dest => dest.Unit_No, opt => opt.MapFrom(src => "M")); // 預設值
            //CreateMap<Money3DbModel, Money3DbModel>(); 
            //CreateMap<THospital, MCIHospital>() 
            //.ForMember(dest => dest.Id, opt => opt.Ignore())
            //.ForMember(dest => dest.HospitalId, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name ?? string.Empty))
            //.ForMember(dest => dest.Abbreviation, opt => opt.MapFrom(src => src.CallName ?? string.Empty))
            //.ForMember(dest => dest.Alias, opt => opt.MapFrom(src => src.Alias ?? string.Empty))
            //.ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color ?? string.Empty))
            //.ForMember(dest => dest.DistanceSort, opt => opt.Ignore());
        }
    }
}
