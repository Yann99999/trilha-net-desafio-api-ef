using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using todo.Context;
using todo.Models;


namespace todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;
         public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpPost()]
        public IActionResult CriarTarefa(Tarefa tarefa){
            if(tarefa.Data == DateTime.MinValue)
                return BadRequest(new {Erro = "A data da tarefa não pode ser vazia!"});

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(PegarPorId), new { id = tarefa.Id }, tarefa);

        }
        [HttpPut("{id}")]
        public IActionResult EditarTarefa(int id, Tarefa tarefa){
            var tarefaBanco = _context.Tarefas.Find(id);

            if(tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia!" });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletarTarefa(int id, Tarefa tarefa){
            var tarefaBanco = _context.Tarefas.Find(id);

            if(tarefaBanco == null)
                return NotFound();

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }   
    

        [HttpGet("{id}")]
        public IActionResult PegarPorId(int id){
            var tarefaBanco = _context.Tarefas.Find(id);
            
            if(tarefaBanco == null)
                return NotFound();

            return Ok(tarefaBanco);
        }

         [HttpGet("ObterPorTitulo")]
        public IActionResult PegarPorTitulo(string titulo){
            var tarefaBanco = _context.Tarefas.Where((x) => x.Titulo == titulo);
            
            if(tarefaBanco == null)
                return NotFound();

            return Ok(tarefaBanco);
        }

        [HttpGet("ObterPorData")]
        public IActionResult PegarPorData(DateTime data){
            var tarefaBanco = _context.Tarefas.Where((x) => x.Data == data);
            
            if(tarefaBanco == null)
                return NotFound();

            return Ok(tarefaBanco);
        }
    
        [HttpGet("ObterPorStatus")]
        public IActionResult PegarPorStatus(StatusTarefa status){
            var tarefaBanco = _context.Tarefas.Where((x) => x.Status == status);
            
            if(tarefaBanco == null)
                return NotFound();

            return Ok(tarefaBanco);
        }
    
        [HttpGet("ObterTodos")]
        public IActionResult PegarTudo(){
            var tarefaBanco = _context.Tarefas;
            
            if(tarefaBanco == null)
                return NotFound();

            return Ok(tarefaBanco);
        }
    }
}