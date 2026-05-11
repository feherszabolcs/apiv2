using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using apiv2.dto.Association;
using apiv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiv2.Controller
{
    [Route("api/associations")]
    [ApiController]
    public class AssociationController : ControllerBase
    {
        private readonly PlanderDBContext _context;
        public AssociationController(PlanderDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var associations = _context.Associations.ToList();
            return Ok(associations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var association = _context.Associations.Find(id);
            if (association == null)
                return NotFound();

            return Ok(association);
        }

        [HttpPost]
        public async Task<IActionResult> PostAssociation([FromBody] AssociationDto associationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (associationDto == null)
                return BadRequest(associationDto);

            var association = new Association()
            {
                Name = associationDto.Name,
                Certificate = associationDto.Certificate,
                Location = associationDto.Location,
            };

            _context.Associations.Add(association);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = association.Id }, association);
        }
    }
}