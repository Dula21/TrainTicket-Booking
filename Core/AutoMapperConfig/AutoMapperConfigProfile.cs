using AutoMapper;
using Booking_TrainTickets.Core.DTO;
using Booking_TrainTickets.Core.Entities;
using System.Net.Sockets;

namespace Booking_TrainTickets.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            //Tickets
            CreateMap<CreateTrainDto, Train>();
            CreateMap<Train, GetTrainDto>();
            CreateMap<UpdateTrainDto, UpdateTrainDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            //Passenger
            CreateMap<CreatePassengerDto, Passenger>();
            CreateMap<Passenger, GetPassengerDto>();
            CreateMap<UpdatePassengerDto, UpdatePassengerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}