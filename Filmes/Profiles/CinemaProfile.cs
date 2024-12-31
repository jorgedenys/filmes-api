using AutoMapper;
using Filmes.Data.Dtos.Cinema;
using Filmes.Models;

namespace Filmes.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile() 
    {
        CreateMap<CreateCinemaDto, Cinema>();

        CreateMap<UpdateCinemaDto, Cinema>();

        CreateMap<Cinema, ReadCinemaDto>()
            .ForMember(cinemaDto => cinemaDto.Endereco,
            opt => opt.MapFrom(cinema => cinema.Endereco))
            .ForMember(cinemaDto => cinemaDto.Sessoes,
            opt => opt.MapFrom(cinema => cinema.Sessoes));

        CreateMap<Cinema, ReadCinemaDto>();
    }

}
