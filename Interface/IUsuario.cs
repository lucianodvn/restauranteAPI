using System;
using Microsoft.AspNetCore.Mvc;
using Restaurante.API.Models;

namespace Restaurante.API.Interface
{
    public interface IUsuario
    {
        Task<ActionResult> RegistroUsuario(Usuario usuario);
    }
}

