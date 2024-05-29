using System;
using AutoMapper;
using Restaurante.API.DTOs;
using Restaurante.API.Models;

namespace Restaurante.API.Mappings
{
    public class EntitiesToDTOPappingProfile : Profile
    {
        public EntitiesToDTOPappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<RestauranteApp, AtendimentoDTO>().ReverseMap();
            CreateMap<Pedido, PedidoDTO>().ReverseMap();
            CreateMap<Mesa, MesaDTO>().ReverseMap();
        }
    }
}

