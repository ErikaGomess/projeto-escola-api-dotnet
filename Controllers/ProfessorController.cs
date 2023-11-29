using Microsoft.AspNetCore.Mvc;
using ProjetoEscola_API.Models;
using ProjetoEscola_MySQL.Data;

namespace ProjetoEscola_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProfessorController : Controller
    {
        private readonly EscolaContext _context;

        public ProfessorController(EscolaContext context)
        {
            // construtor
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Professor>> GetAll()
        {
            return _context.Professor.ToList();
        }
        [HttpGet("{ProfessorId}")]
        public ActionResult<Curso> Get(int ProfessorId)
        {
            try
            {
                var result = _context.Professor.Find(ProfessorId);
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
        public async Task<ActionResult> post(Professor model)
        {
            try
            {
                _context.Professor.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/professor/{model.id}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();
        }
        [HttpDelete("{ProfessorId}")]
        public async Task<ActionResult> delete(int ProfessorId)
        {
            try
            {
                //verifica se existe aluno a ser excluído
                var curso = await _context.Professor.FindAsync(ProfessorId);
                if (curso == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(curso);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
        [HttpPut("{ProfessorId}")]
        public async Task<IActionResult> put(int ProfessorId, Professor dadosProfessorAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Professor.FindAsync(ProfessorId);
                if (ProfessorId != result.id)
                {
                    return BadRequest();
                }

                result.nome = dadosProfessorAlt.nome;
                result.codCurso = dadosProfessorAlt.codCurso;
                result.email = dadosProfessorAlt.email;
                await _context.SaveChangesAsync();
                return Created($"/api/curso/{dadosProfessorAlt.id}", dadosProfessorAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
    }
}