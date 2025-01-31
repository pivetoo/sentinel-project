using Sentinel.Domain.Entities;
using Sentinel.Domain.Repositories;
using Sentinel.Domain.Services.Base;
using Sentinel.Domain.Services.Security;

namespace Sentinel.Domain.Services
{
    public class UsuarioSentinelService : CrudService<UsuarioSentinel>
    {
        private readonly Argon2HashingService _hashingService;

        public UsuarioSentinelService(
            ICrudRepository<UsuarioSentinel> repository,
            Argon2HashingService hashingService) : base(repository)
        {
            _hashingService = hashingService;
        }

        public void InativarUsuario(long id)
        {
            var usuario = GetById(id);
            usuario.Ativo = false;
            Update(usuario);
        }

        public void AtivarUsuario(long id)
        {
            var usuario = GetById(id);
            usuario.Ativo = true;
            Update(usuario);
        }

        #region Overrides
        public override void Add(UsuarioSentinel usuario)
        {
            ValidarUsuario(usuario);
            usuario.Senha = _hashingService.GerarHash(usuario.Senha);
            usuario.Ativo = true;
            base.Add(usuario);
        }

        public override void Update(UsuarioSentinel usuario)
        {
            var usuarioExistente = GetById(usuario.Id);

            ValidarUsuario(usuario);

            usuarioExistente.Login = usuario.Login;
            usuarioExistente.NomeCompleto = usuario.NomeCompleto;
            usuarioExistente.Email = usuario.Email;

            if (!string.IsNullOrEmpty(usuario.Senha))
                usuarioExistente.Senha = _hashingService.GerarHash(usuario.Senha);

            base.Update(usuarioExistente);
        }
        #endregion

        #region Lógica Auxiliar
        private void ValidarUsuario(UsuarioSentinel usuario)
        {
            if (string.IsNullOrEmpty(usuario.Login))
                throw new InvalidOperationException("Login é obrigatório.");

            if (string.IsNullOrEmpty(usuario.NomeCompleto))
                throw new InvalidOperationException("Nome completo é obrigatório.");

            if (string.IsNullOrEmpty(usuario.Email))
                throw new InvalidOperationException("E-mail é obrigatório.");

            var loginExistente = _repository.Query(u =>
                u.Login == usuario.Login &&
                u.Id != usuario.Id
            ).Any();

            if (loginExistente)
                throw new InvalidOperationException("Login já está em uso.");

            var emailExistente = _repository.Query(u =>
                u.Email == usuario.Email &&
                u.Id != usuario.Id
            ).Any();

            if (emailExistente)
                throw new InvalidOperationException("E-mail já está em uso.");
        }
        #endregion
    }
}
