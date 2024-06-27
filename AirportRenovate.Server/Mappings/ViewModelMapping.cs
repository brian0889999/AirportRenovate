using AirportRenovate.Server.DTOs;
using AirportRenovate.Server.Models;
using AirportRenovate.Server.Utilities;
using AirportRenovate.Server.ViewModels;
using AutoMapper;

namespace AirportRenovate.Server.Mappings
{
    public class ViewModelMapping: Profile
    {
        public ViewModelMapping()
        {
            CreateMap<SoftDeleteViewModel, Money3DbModel>()
            .ForMember(dest => dest.MoneyDbModel, opt => opt.Ignore());
        }
    }
}
