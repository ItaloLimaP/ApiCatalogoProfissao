using ApiCatalogoProfissao.Exceptions;
using ApiCatalogoProfissao.InputModel;
using ApiCatalogoProfissao.Services;
using ApiCatalogoProfissao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoProfissao.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProfissaoController : ControllerBase
    {
        private readonly Services.IProfissaoService _profissaoService;

        public ProfissaoController(Services.IProfissaoService profissaoService)
        {
            _profissaoService = profissaoService;
        }

        /// <summary>
        /// Buscar todas as profissões de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar as profissões sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de profissões</response>
        /// <response code="204">Caso não haja profissões</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfissaoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var profissao = await _profissaoService.Obter(pagina, quantidade);

            if (profissao.Count() == 0)
                return NoContent();

            return Ok(profissao);
        }

        /// <summary>
        /// Buscar uma profissao pelo seu Id
        /// </summary>
        /// <param name="idProfissao">Id da profissao buscada</param>
        /// <response code="200">Retorna a profissao filtrada</response>
        /// <response code="204">Caso não haja profissao com este id</response>   
        [HttpGet("{idProfissao:guid}")]
        public async Task<ActionResult<ProfissaoViewModel>> Obter([FromRoute] Guid idProfissao)
        {
            var profissao = await _profissaoService.Obter(idProfissao);

            if (profissao == null)
                return NoContent();

            return Ok(profissao);
        }

        /// <summary>
        /// Inserir uma profissao no catálogo
        /// </summary>
        /// <param name="profissaoInputModel">Dados da profissao a ser inserida</param>
        /// <response code="200">Caso a profissao seja inserida com sucesso</response>
        /// <response code="422">Caso já exista uma profissao com mesmo nome para a mesma empresa</response>   
        [HttpPost]
        public async Task<ActionResult<ProfissaoViewModel>> InserirJogo([FromBody] ProfissaoInputModel profissaoInputModel)
        {
            try
            {
                var profissao = await _profissaoService.Inserir(profissaoInputModel);

                return Ok(profissao);
            }
            catch (ProfissaoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe uma profissão com este nome para esta empresa");
            }
        }

        /// <summary>
        /// Atualizar uma profissao no catálogo
        /// </summary>
        /// /// <param name="idProfissao">Id da rofissao a ser atualizada</param>
        /// <param name="profissaoInputModel">Novos dados para atualizar a profissao indicada</param>
        /// <response code="200">Caso a profissao seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista uma profissao com este Id</response>   
        [HttpPut("{idProfissao:guid}")]
        public async Task<ActionResult> AtualizarProfissao([FromRoute] Guid idProfissao, [FromBody] ProfissaoInputModel profissaoInputModel)
        {
            try
            {
                await _profissaoService.Atualizar(idProfissao, profissaoInputModel);

                return Ok();
            }
            catch (ProfissaoNaoCadastradoException ex)
            {
                return NotFound("Não existe esta profissao");
            }
        }

        /// <summary>
        /// Atualizar o preço de uma profissao
        /// </summary>
        /// /// <param name="idProfissao">Id da profissao a ser atualizada</param>
        /// <param name="preco">Novo preço da profissao</param>
        /// <response code="200">Caso a profissao seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista uma profissao com este Id</response>   
        [HttpPatch("{idProfissao:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarProfissao([FromRoute] Guid idProfissao, [FromRoute] double preco)
        {
            try
            {
                await _profissaoService.Atualizar(idProfissao, preco);

                return Ok();
            }
            catch (ProfissaoNaoCadastradoException ex)
            {
                return NotFound("Não existe este jogo");
            }
        }

        /// <summary>
        /// Excluir uma profissao
        /// </summary>
        /// /// <param name="idProfissao">Id da profissao a ser excluída</param>
        /// <response code="200">Caso o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista uma profissao com este Id</response>   
        [HttpDelete("{idProfissao:guid}")]
        public async Task<ActionResult> ApagarProfissao([FromRoute] Guid idProfissao)
        {
            try
            {
                await _profissaoService.Remover(idProfissao);

                return Ok();
            }
            catch (ProfissaoNaoCadastradoException ex)
            {
                return NotFound("Não existe esta profissao");
            }
        }

    }
}
