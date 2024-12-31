using AutoMapper;
using Filmes.Data.Dtos.Filme;
using Filmes.Models;

namespace Filmes.Profiles;

public class FilmeProfile : Profile
{

    public FilmeProfile() 
    {
        CreateMap<CreateFilmeDto, Filme>();

        CreateMap<UpdateFilmeDto, Filme>();

        CreateMap<Filme, ReadFilmeDto>()
            .ForMember(filmeDto => filmeDto.Sessoes,
            opt => opt.MapFrom(filme => filme.Sessoes));

    }

}
