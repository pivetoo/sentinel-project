using Sentinel.Domain.Entities;
using Sentinel.Domain.Repositories;
using Sentinel.Domain.Services.Base;

namespace Sentinel.Domain.Services
{
    public class EmpresaService : CrudService<Empresa>
    {
        public EmpresaService(ICrudRepository<Empresa> repository) : base(repository)
        {
        }

        public void Inativar(long id)
        {
            var empresa = GetById(id);
            empresa.Ativo = false;
            Update(empresa);
        }

        public void Ativar(long id)
        {
            var empresa = GetById(id);
            empresa.Ativo = true;
            Update(empresa);
        }

        #region Overrides
        public override void Add(Empresa empresa)
        {
            ValidarEmpresa(empresa);
            empresa.Ativo = true;
            empresa.TenantId = empresa.Nome.ToLower().Replace(" ", "_");

            if (empresa.Usuarios == null)
                empresa.Usuarios = new List<UsuarioEmpresa>();

            if (empresa.Contratos == null)
                empresa.Contratos = new List<Contrato>();

            base.Add(empresa);
        }

        public override void Update(Empresa empresa)
        {
            var empresaExistente = GetById(empresa.Id);
            ValidarEmpresa(empresa);

            empresaExistente.Nome = empresa.Nome;
            empresaExistente.Email = empresa.Email;
            empresaExistente.Telefone = empresa.Telefone;
            empresaExistente.Cnpj = empresa.Cnpj;

            base.Update(empresaExistente);
        }
        #endregion

        #region Lógica Auxiliar
        private void ValidarEmpresa(Empresa empresa)
        {
            if (string.IsNullOrEmpty(empresa.Nome))
                throw new InvalidOperationException("Nome é obrigatório.");

            if (string.IsNullOrEmpty(empresa.Cnpj))
                throw new InvalidOperationException("CNPJ é obrigatório.");

            if (string.IsNullOrEmpty(empresa.Email))
                throw new InvalidOperationException("Email é obrigatório.");

            var cnpjExistente = _repository.Query(e =>
                e.Cnpj == empresa.Cnpj &&
                e.Id != empresa.Id
            ).Any();

            if (cnpjExistente)
                throw new InvalidOperationException("CNPJ já está em uso.");

            var emailExistente = _repository.Query(e =>
                e.Email == empresa.Email &&
                e.Id != empresa.Id
            ).Any();

            if (emailExistente)
                throw new InvalidOperationException("Email já está em uso.");
        }
        #endregion
    }
}
