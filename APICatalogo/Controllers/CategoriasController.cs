using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<List<Categoria>>> ObterProdutosCategoria()
        {
            return await _context.Categorias.AsNoTracking().Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToListAsync(); 

        }

        [HttpGet("saudacao/{nome}")]
        public ActionResult<string> GetSaudacao([FromServices] IMeuServico meuServico, string nome) 
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> ObterCategorias()
        {
            return await _context.Categorias.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObterCategoriaPorId")]
        public async Task<ActionResult<Categoria>> ObterCategoriaPorId(int id)
        {
            try
            {
                var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound("Categoria não encontrada");
                }

                return categoria;

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AdicionarCategoria(Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    return BadRequest();
                }

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoriaPorId", new { id = categoria.CategoriaId }, categoria);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult AtualizarCategoria(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest();
                }

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(categoria);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletarCategoria(int id)
        {
            try
            {
                var categoria = await _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound("Categoria não localizado");
                }

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();

                return Ok(categoria);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
