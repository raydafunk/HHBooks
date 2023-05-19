using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HHBooks.API.Data;
using HHBooks.API.Modles.Author;
using AutoMapper;
using HHBooks.API.Static;

namespace HHBooks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly HhbookStoreContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(HhbookStoreContext context, IMapper mapper, ILogger<AuthorsController> logger)
        {
            _context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorReadOnlyDto>>> GetAuthors()
        {
            try
            {
                var authorsDto = _mapper.Map<IEnumerable<AutorReadOnlyDto>>(await _context.Authors.ToListAsync());
                return Ok(authorsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Pefroming GET in{nameof(GetAuthors)}");
                return StatusCode(500, Messages.ErroMessage);
            }
         
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorReadOnlyDto>> GetAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"Record not found: {nameof(GetAuthors)}- ID:{id}");
                    return NotFound();
                }
                var authordto = _mapper.Map<AutorReadOnlyDto>(author);
                return Ok(authordto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Pefroming GET in{nameof(GetAuthors)}");
                return StatusCode(500, Messages.ErroMessage);
            }
            
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, UpdateAurthorDto authorDto)
        {
            if (id != authorDto.AuthorsId)
            {
                return BadRequest();
            }

            var author = _context.Authors.FindAsync(id);
            if (await author == null)
            {
                return NotFound();
            }

              await _mapper.Map(authorDto, author);
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreateAuthorDto>> PostAuthor(CreateAuthorDto authordto)
        {
            var author = _mapper.Map<Author>(authordto);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.AuthorsId }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors!.AnyAsync(e => e.AuthorsId == id);
        }
    }
}
