using ApiCatalogoProfissao.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoProfissao.Repositories
{
    public interface IProfissaoRepository : IDisposable
    {
        Task<List<Profissao>> Obter(int pagina, int quantidade);
        Task<Profissao> Obter(Guid id);
        Task<List<Profissao>> Obter(string nome, string empresa);
        Task Inserir(Profissao profissao);
        Task Atualizar(Profissao profissao);
        Task Remover(Guid id);
    }
}
