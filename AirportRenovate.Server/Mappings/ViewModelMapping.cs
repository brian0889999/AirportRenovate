using AirportRenovate.Server.DTOs;
using AirportRenovate.Server.Models;
using AutoMapper;

namespace AirportRenovate.Server.Mappings
{
    public class ViewModelMapping: Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Money3DbModel, Money3DbModelDto>();
        }
    }
}
