using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<List<Categoria>> Get()
        {
            //return _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
            return _context.Categorias.AsNoTracking().Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList(); 


        }

        [HttpGet]
        public ActionResult<List<Categoria>> ObterCategorias()
        {
            return _context.Categorias.AsNoTracking().ToList();
        }

        [HttpGet("{id:int}", Name = "ObterCategoriaPorId")]
        public ActionResult<Categoria> ObterCategoriaPorId(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);

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
        public ActionResult DeletarCategoria(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

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
