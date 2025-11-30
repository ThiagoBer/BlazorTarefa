using BlazorTarefa.Models;
using BlazorTarefas.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorTarefa.Controllers
{
    [ApiController]
    [Route("api/tarefas")]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TarefasController(AppDbContext db)
        {
            _db = db;
        }

        // Ler Todas as Tarefas
        [HttpGet]
        public async Task<ActionResult<List<Tarefa>>> Get()
        {
            Console.WriteLine("Controllers --- listado tarefas");
            var result = await _db.Tarefas
                .Where(t => t.IdPai == null)
                .Include(t => t.SubTarefas)
                .OrderBy(t => t.CriadoEm)
                .ToListAsync();
            return result;
        }

        // Ler Tarefa Especifica
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Tarefa>> Get(Guid id)
        {
            var result = await _db.Tarefas.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // Incluir Tarefa
        [HttpPost]
        public async Task<ActionResult<Tarefa>> Post(Tarefa tarefa)
        {
            _db.Tarefas.Add(tarefa);
            await _db.SaveChangesAsync();
            Console.WriteLine(tarefa.Id.ToString() + " - " + tarefa.Descricao);
            return Ok(tarefa);
        }

        // Alterar Tarefa
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, Tarefa tarefa)
        {
            Console.WriteLine("Alterando " + tarefa.Id.ToString() + " - " + tarefa.Descricao);
            if (id != tarefa.Id) return BadRequest();
            if (tarefa.Concluido)
            {
                tarefa.MarcarConcluida();
            }
            else {
                tarefa.DesmarcarConcluida();
            }
            _db.Update(tarefa);
            await _db.SaveChangesAsync();
            return Ok();
        }

        // Excluir Tarefa
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            Console.WriteLine("Excluindo " + id.ToString());
            var tarefa = await _db.Tarefas.FindAsync(id);
            if (tarefa is null) return NotFound();
            _db.Tarefas.Remove(tarefa);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
