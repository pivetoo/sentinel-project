using Sentinel.Domain.Entities;
using Sentinel.Domain.Repositories;
using Sentinel.Domain.Services.Base;
using Sentinel.Domain.Services.Security;

namespace Sentinel.Domain.Services
{
    public class UsuarioEmpresaService : CrudService<UsuarioEmpresa>
    {
        private readonly IQueryRepository<Papel> _papelRepository;
        private readonly IQueryRepository<Permissao> _permissaoRepository;
        private readonly Argon2HashingService _hashingService;

        public UsuarioEmpresaService(
            ICrudRepository<UsuarioEmpresa> repository,
            IQueryRepository<Papel> papelRepository,
            IQueryRepository<Permissao> permissaoRepository,
            Argon2HashingService hashingService) : base(repository)
        {
            _papelRepository = papelRepository;
            _permissaoRepository = permissaoRepository;
            _hashingService = hashingService;
        }

        public void InativarUsuarioEmpresa(long id)
        {
            var usuario = GetById(id);
            usuario.Ativo = false;
            Update(usuario);
        }

        public void AtivarUsuarioEmpresa(long id)
        {
            var usuario = GetById(id);
            usuario.Ativo = true;
            Update(usuario);
        }

        public void AtribuirPapel(long usuarioId, long papelId)
        {
            var usuario = GetById(usuarioId);
            var papel = _papelRepository.GetById(papelId);

            if (papel == null)
                throw new KeyNotFoundException("Papel não encontrado.");

            if (usuario.Papeis.Any(p => p.Id == papelId))
                throw new InvalidOperationException("Usuário já possui este papel.");

            usuario.Papeis.Add(papel);
            Update(usuario);
        }

        public void RemoverPapel(long usuarioId, long papelId)
        {
            var usuario = GetById(usuarioId);
            var papel = usuario.Papeis.FirstOrDefault(p => p.Id == papelId);

            if (papel == null)
                throw new InvalidOperationException("Usuário não possui este papel.");

            usuario.Papeis.Remove(papel);
            Update(usuario);
        }

        public IEnumerable<UsuarioEmpresa> ObterPorEmpresa(long empresaId)
        {
            return GetByFilter(u => u.EmpresaId == empresaId);
        }

        #region Overrides
        public override void Add(UsuarioEmpresa usuario)
        {
            ValidarUsuarioEmpresa(usuario);
            usuario.Senha = _hashingService.GerarHash(usuario.Senha);
            usuario.Ativo = true;

            if (usuario.Papeis == null)
                usuario.Papeis = new List<Papel>();

            base.Add(usuario);
        }

        public override void Update(UsuarioEmpresa usuario)
        {
            var usuarioExistente = GetById(usuario.Id);

            ValidarUsuarioEmpresa(usuario);

            usuarioExistente.Login = usuario.Login;
            usuarioExistente.NomeCompleto = usuario.NomeCompleto;
            usuarioExistente.Email = usuario.Email;

            if (!string.IsNullOrEmpty(usuario.Senha))
                usuarioExistente.Senha = _hashingService.GerarHash(usuario.Senha);

            base.Update(usuarioExistente);
        }
        #endregion

        #region Lógica Auxiliar
        private void ValidarUsuarioEmpresa(UsuarioEmpresa usuario)
        {
            if (usuario.EmpresaId <= 0)
                throw new InvalidOperationException("Empresa é obrigatória.");

            if (string.IsNullOrEmpty(usuario.Login))
                throw new InvalidOperationException("Login é obrigatório.");

            if (string.IsNullOrEmpty(usuario.NomeCompleto))
                throw new InvalidOperationException("Nome completo é obrigatório.");

            if (string.IsNullOrEmpty(usuario.Email))
                throw new InvalidOperationException("E-mail é obrigatório.");

            var loginExistente = _repository.Query(u =>
                u.Login == usuario.Login &&
                u.EmpresaId == usuario.EmpresaId &&
                u.Id != usuario.Id
            ).Any();

            if (loginExistente)
                throw new InvalidOperationException("Login já está em uso nesta empresa.");

            var emailExistente = _repository.Query(u =>
                u.Email == usuario.Email &&
                u.EmpresaId == usuario.EmpresaId &&
                u.Id != usuario.Id
            ).Any();

            if (emailExistente)
                throw new InvalidOperationException("E-mail já está em uso nesta empresa.");
        }
        #endregion
    }
}
