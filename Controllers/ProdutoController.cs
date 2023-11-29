using Microsoft.AspNetCore.Mvc;
using ProjetoEscola_API.Models;
using ProjetoEscola_MySQL.Data;

namespace ProjetoEscola_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProdutoController : Controller
    {
        private readonly EscolaContext _context;

        public ProdutoController(EscolaContext context)
        {
            // construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            return _context.Produto.ToList();
        }
        [HttpGet("{ProfessorId}")]
        public ActionResult<Produto> Get(int ProdutoId)
        {
            try
            {
                var result = _context.Produto.Find(ProdutoId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
        [HttpPost]
        public async Task<ActionResult> post(Produto model)
        {
            try
            {
                _context.Produto.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/produto/{model.id}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }
        [HttpDelete("{ProdutoId}")]
        public async Task<ActionResult> delete(int ProdutoId)
        {
            try
            {
                //verifica se existe aluno a ser excluído
                var produto = await _context.Produto.FindAsync(ProdutoId);
                if (produto == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(produto);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
        [HttpPut("{ProdutoId}")]
        public async Task<IActionResult> put(int ProdutoId, Produto dadosProdutoAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Produto.FindAsync(ProdutoId);
                if (ProdutoId != result.id)
                {
                    return BadRequest();
                }

                result.nome = dadosProdutoAlt.nome;
                result.descricao = dadosProdutoAlt.descricao;
                result.imagem = dadosProdutoAlt.imagem;
                result.quant = dadosProdutoAlt.quant;
                await _context.SaveChangesAsync();
                return Created($"/api/produto/{dadosProdutoAlt.id}", dadosProdutoAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
    }
}