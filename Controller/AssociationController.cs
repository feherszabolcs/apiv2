using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using apiv2.dto.Association;
using apiv2.Interfaces;
using apiv2.Mappers;
using apiv2.Models;
using apiv2.Repository;
using Microsoft.AspNetCore.Mvc;

namespace apiv2.Controller
{
    [Route("api/associations")]
    [ApiController]
    public class AssociationController : ControllerBase
    {
        private readonly PlanderDBContext _context;
        private readonly IAssociationRepository _repo;
        public AssociationController(PlanderDBContext context, IAssociationRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var associations = await _repo.GetAllAsync();
            var associationDto = associations.Select(a => a.GetAssociationDto());
            return Ok(associations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var association = await _repo.GetByIdAsync(id);
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

            var association = associationDto.ToAssociation();

            await _repo.CreateAsync(association);

            return CreatedAtAction(nameof(GetById), new { id = association.Id }, association.GetAssociationDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAssociation([FromRoute] int id, AssociationPatchDto associationPatchDto)
        {
            var association = await _repo.UpdateAsync(id, associationPatchDto);
            if (association == null) return NotFound();
            return Ok();
        }
    }
}