using ApiCatalogoProfissao.InputModel;
using ApiCatalogoProfissao.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoProfissao.Services
{
    public interface IProfissaoService : IDisposable
    {
        Task<List<ProfissaoViewModel>> Obter(int pagina, int quantidade);
        Task<ProfissaoViewModel> Obter(Guid id);
        Task<ProfissaoViewModel> Inserir(ProfissaoInputModel jogo);
        Task Atualizar(Guid id, ProfissaoInputModel jogo);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
