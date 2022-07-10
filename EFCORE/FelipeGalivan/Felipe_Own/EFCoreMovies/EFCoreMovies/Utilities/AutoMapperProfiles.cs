using AutoMapper;
using EFCoreMovies.DTO;
using EFCoreMovies.Entities;

namespace EFCoreMovies.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Actor, ActorDTO>();
            CreateMap<Cinema, CinemaDTO>()
                .ForMember(dto => dto.Latitude, ent => ent.MapFrom(p=>p.Location.Y))
                .ForMember(dto => dto.Longitude, ent => ent.MapFrom(p => p.Location.X));

            CreateMap<Genre, GenreDTO>();

            CreateMap<Movie,MovieDTO>()
                .ForMember(dto=>dto.Cinemas,ent => ent.MapFrom(p => p.CinemaHalls.Select(c=>c.Cinema)))
                .ForMember(dto => dto.Actors,ent => ent.MapFrom(p=> p.MovieActors.Select(ma=>ma.Actor)));
        }
    }
}
