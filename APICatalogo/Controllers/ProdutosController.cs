using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> ObterProdutos()
        {
            try
            {
                var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

                if (produtos == null)
                {
                    return NotFound("Produtos não encontrados");
                }

                return produtos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id:int}", Name = "ObterProdutoPorId")]
        public async Task<ActionResult<Produto>> ObterPorId(int id)
        {
            try
            {
                var produto = await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);

                if (produto == null)
                {
                    return NotFound("Produto não encontrado");
                }

                return produto;

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AdicionarProduto(Produto produto)
        {
            try
            {
                if (produto == null)
                {
                    return BadRequest();
                }

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterProdutoPorId", new { id = produto.ProdutoId }, produto);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult AtualizarProduto(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest();
                }

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(produto);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletarProduto(int id)
        {
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);

                if (produto == null)
                {
                    return NotFound("Produto não localizado");
                }

                _context.Produtos.Remove(produto);
                _context.SaveChanges();

                return Ok(produto);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
