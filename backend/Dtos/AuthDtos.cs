namespace Pizzaria.Api.Dtos;

public record RegistrarDto(string Nome, string Email, string Senha);

public record LoginDto(string Email, string Senha);

public record UsuarioDto(int Id, string Nome, string Email);
