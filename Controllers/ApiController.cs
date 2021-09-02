using API_Armazenamento.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Produto.Controllers
{
    [Route("")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApiContext _context;       //Adicione para configurar a API
        public ApiController(ApiContext context)   //Adicione para configurar a API
        {
            _context = context;
        }
        [HttpGet]
        [Route("produto/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);

                return Ok(produto);

            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }

        [HttpGet]
        [Route("produtos")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allProdutos = await _context.Produtos.ToListAsync();
                return Ok(allProdutos);

            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }

        }

        [HttpPut]
        [Route("produto")]

        public async Task<IActionResult> Put(int Id, [FromBody] Produto item)
        {

            try
            {
                var inserir = _context.Produtos.FindAsync(Id);

                //var inserir = _context.Produtos.Where(x => x.Id == item.Id).Select(p => new       //TAMBÉM FUNCIONA
                //{
                //    id = p.Id,
                //    Codigo = p.Codigo,
                //    Descricao = p.Descricao,
                //    Estoque = p.Estoque,
                //    Preco = p.Preco
                //});

                _context.Produtos.Update(item);
                await _context.SaveChangesAsync();
                return Ok("Produto atualizado com Sucesso");

            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }

        }

        [HttpPost]
        [Route("produto")]

        public async Task<IActionResult> Post([FromBody] Produto item)
        {
            try
            {
                await _context.Produtos.AddAsync(item);
                await _context.SaveChangesAsync();
                return Created("", item);

            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteId (int id)
        {
            try
            {
                var deletar = _context.Produtos.Where(x => x.Id == id).Select(p => new
                {
                    Codigo = p.Codigo,
                    Descricao = p.Descricao,
                    Estoque = p.Estoque,
                    Preco = p.Preco
                });
                _context.Produtos.Remove(new Produto() { Id = id });
                _context.SaveChanges();

                return Ok("Produto com id " + id + " foi removido");
                
            }

            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }
    }
}
