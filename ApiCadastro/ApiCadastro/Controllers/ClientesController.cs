using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiCadastro;

namespace ApiCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly CadastroContext _context;

        public ClientesController(CadastroContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetClientes(string id)
        {
            var clientes = await _context.Clientes.FindAsync(id);

            if (clientes == null)
            {
                return NotFound();
            }

            return clientes;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(string id, Clientes clientes)
        {
            if (id != clientes.Cpf)
            {
                return BadRequest();
            }

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            _context.Clientes.Add(clientes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientesExists(clientes.Cpf))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClientes", new { id = clientes.Cpf }, clientes);
        }
        */



        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                if (ClientesExists(cliente.Cpf))
                {
                    return Conflict();
                }
                
            }

            return CreatedAtAction("GetClientes", new { id = cliente.Cpf }, cliente); ;
        }



























        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Clientes>> DeleteClientes(string id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            return clientes;
        }

        private bool ClientesExists(string id)
        {
            return _context.Clientes.Any(e => e.Cpf == id);
        }
    }
}
