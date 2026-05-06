using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using Microsoft.AspNetCore.Mvc;

namespace apiv2.Controller
{
    [Route("api/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly PlanderDBContext _context;

        public AssignmentController(PlanderDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var assignments = _context.Assignments.ToList();
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var assignment = _context.Assignments.Find(id);
            if (assignment == null)
                return NotFound();
                
            return Ok(assignment);
        }
    }
}