using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaria.Api.Data;
using Pizzaria.Api.Dtos;
using Pizzaria.Api.Models;

namespace Pizzaria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;

    public AuthController(AppDbContext db)
    {
        _db = db;
    }

    private static string Hash(string senha)
    {
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
        return Convert.ToHexString(bytes);
    }

    [HttpPost("registrar")]
    public async Task<ActionResult<UsuarioDto>> Registrar(RegistrarDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Senha))
            return BadRequest("Preencha nome, e-mail e senha.");

        var email = dto.Email.Trim().ToLower();
        var existe = await _db.Usuarios.AnyAsync(u => u.Email == email);
        if (existe) return BadRequest("Já existe uma conta com esse e-mail.");

        var usuario = new Usuario
        {
            Nome = dto.Nome.Trim(),
            Email = email,
            SenhaHash = Hash(dto.Senha)
        };

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();

        return Ok(new UsuarioDto(usuario.Id, usuario.Nome, usuario.Email));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UsuarioDto>> Login(LoginDto dto)
    {
        var email = (dto.Email ?? "").Trim().ToLower();
        var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario is null || usuario.SenhaHash != Hash(dto.Senha ?? ""))
            return Unauthorized("E-mail ou senha inválidos.");

        return Ok(new UsuarioDto(usuario.Id, usuario.Nome, usuario.Email));
    }
}
