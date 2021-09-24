using ApiCatalogoProfissao.Entities;
using ApiCatalogoProfissao.InputModel;
using ApiCatalogoProfissao.Repositories;
using ApiCatalogoProfissao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoProfissao.Services
{
    public class ProfissaoService : ApiCatalogoProfissao.IProfissaoService
    {
        private readonly IProfissaoRepository _profissaoRepository;

        public ProfissaoService(IProfissaoRepository profissaoRepository)
        {
            _profissaoRepository = profissaoRepository;
        }

        public async Task<List<ProfissaoViewModel>> Obter(int pagina, int quantidade)
        {
            var profissao = await _profissaoRepository.Obter(pagina, quantidade);

            return profissao.Select(profissao => new ProfissaoViewModel
            {
                Id = profissao.Id,
                Nome = profissao.Nome,
                Empresa = profissao.Empresa,
                Preco = profissao.Preco
            })
                               .ToList();
        }

        public async Task<ProfissaoViewModel> Obter(Guid id)
        {
            var profissao = await _profissaoRepository.Obter(id);

            if (profissao == null)
                return null;

            return new ProfissaoViewModel
            {
                Id = profissao.Id,
                Nome = profissao.Nome,
                Empresa = profissao.Empresa,
                Preco = profissao.Preco
            };
        }

        public async Task<ProfissaoViewModel> Inserir(ProfissaoInputModel profissao)
        {
            var entidadeProfissao = await _profissaoRepository.Obter(profissao.Nome, profissao.Empresa);

            if (entidadeProfissao.Count > 0)
                throw new ProfissaoJaCadastradoException();

            var profissaoInsert = new Profissao
            {
                Id = Guid.NewGuid(),
                Nome = profissao.Nome,
                Empresa = profissao.Empresa,
                Preco = profissao.Preco
            };

            await _profissaoRepository.Inserir(profissaoInsert);

            return new ProfissaoViewModel
            {
                Id = profissaoInsert.Id,
                Nome = profissao.Nome,
                Empresa = profissao.Empresa,
                Preco = profissao.Preco
            };
        }

        public async Task Atualizar(Guid id, ProfissaoInputModel profissao)
        {
            var entidadeProfissao = await _profissaoRepository.Obter(id);

            if (entidadeProfissao == null)
                throw new ProfissaoNaoCadastradoException();

            entidadeProfissao.Nome = profissao.Nome;
            entidadeProfissao.Empresa = profissao.Empresa;
            entidadeProfissao.Preco = profissao.Preco;

            await _profissaoRepository.Atualizar(entidadeProfissao);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeProfissao = await _profissaoRepository.Obter(id);

            if (entidadeProfissao == null)
                throw new ProfissaoNaoCadastradoException();

            entidadeProfissao.Preco = preco;

            await _profissaoRepository.Atualizar(entidadeProfissao);
        }

        public async Task Remover(Guid id)
        {
            var profissao = await _profissaoRepository.Obter(id);

            if (profissao == null)
                throw new ProfissaoNaoCadastradoException();

            await _profissaoRepository.Remover(id);
        }

        public void Dispose()
        {
            _profissaoRepository?.Dispose();
        }
    }
}
