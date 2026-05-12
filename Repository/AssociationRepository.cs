using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using apiv2.dto.Association;
using apiv2.Interfaces;
using apiv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiv2.Repository
{
    public class AssociationRepository : IAssociationRepository
    {
        private readonly PlanderDBContext _context;
        public AssociationRepository(PlanderDBContext context)
        {
            _context = context;
        }

        public Task<List<Association>> GetAllAsync()
        {
            return _context.Associations.ToListAsync();
        }
        public async Task<Association?> GetByIdAsync(int id)
        {
            return await _context.Associations.FindAsync(id);
        }
        public async Task<Association> CreateAsync(Association association)
        {
            await _context.Associations.AddAsync(association);
            await _context.SaveChangesAsync();
            return association;
        }
        public async Task<Association?> UpdateAsync(int id, AssociationPatchDto dto)
        {
            var association = await _context.Associations.FirstOrDefaultAsync(x => x.Id == id);
            if (association == null) return null;

            association.IsConfirmed = dto.IsConfirmed;
            await _context.SaveChangesAsync();
            return association;
        }
    }
}