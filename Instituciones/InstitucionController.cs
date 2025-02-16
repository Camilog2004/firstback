using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTBACK.Instituciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionController : ControllerBase
    {
        private readonly IInstitucionService _institucionService;
        private readonly IMapper _mapper;

        public InstitucionController(IInstitucionService institucionService, IMapper mapper)
        {
            _institucionService = institucionService;
            _mapper = mapper;
        }

        // GET: api/Institucion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitucionDTO>>> GetAll()
        {
            /* return Ok(await _institucionService.GetAllAsync()); */

            var Instituciones = await _institucionService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<InstitucionDTO>>(Instituciones));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Institucion>> Get(int id)
        {
            var product = await _institucionService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        // POST: api/
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Institucion institucion)
        {
            await _institucionService.CreateAsync(institucion);
            return CreatedAtAction(nameof(Get), new { id = institucion.id }, institucion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Institucion institucion)
        {
            var existinginstitucion = await _institucionService.GetByIdAsync(id);
            if (existinginstitucion == null)
                return NotFound();
            institucion.id = id;
            await _institucionService.UpdateAsync(id, institucion);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existinginstitucion = await _institucionService.GetByIdAsync(id);
            if (existinginstitucion == null)
                return NotFound();
            await _institucionService.DeleteAsync(id);
            return NoContent();
        }

    }
}
