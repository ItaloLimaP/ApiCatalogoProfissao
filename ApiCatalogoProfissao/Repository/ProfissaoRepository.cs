using ApiCatalogoProfissao.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoProfissao.Repositories
{
    public class ProfissaoRepository : IProfissaoRepository
    {
        private static Dictionary<Guid, Profissao> profissoes = new Dictionary<Guid, Profissao>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Profissao{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Nome = "Dev Web", Empresa = "Google", Preco = 4000} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Profissao{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Nome = "DBA", Empresa = "Microsoft", Preco = 5000} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Profissao{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Nome = "QA", Empresa = "Tesla", Preco = 3500} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Profissao{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Nome = "Full Stack", Empresa = "Apple", Preco = 6000} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Profissao{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Nome = "DevOps", Empresa = "Amazon", Preco = 7000} }
        };

        public Task<List<Profissao>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(profissoes.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Profissao> Obter(Guid id)
        {
            if (!profissoes.ContainsKey(id))
                return Task.FromResult<Profissao>(null);

            return Task.FromResult(profissoes[id]);
        }

        public Task<List<Profissao>> Obter(string nome, string empresa)
        {
            return Task.FromResult(profissoes.Values.Where(profissao => profissao.Nome.Equals(nome) && profissao.Empresa.Equals(empresa)).ToList());
        }

        public Task<List<Profissao>> ObterSemLambda(string nome, string empresa)
        {
            var retorno = new List<Profissao>();

            foreach (var profissao in profissoes.Values)
            {
                if (profissao.Nome.Equals(nome) && profissao.Empresa.Equals(empresa))
                    retorno.Add(profissao);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Profissao profissao)
        {
            profissoes.Add(profissao.Id, profissao);
            return Task.CompletedTask;
        }

        public Task Atualizar(Profissao profissao)
        {
            profissoes[profissao.Id] = profissao;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            profissoes.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
