using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using Microsoft.AspNetCore.Mvc;

namespace apiv2.Controller
{
    [Route("api/association")]
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
    }
}