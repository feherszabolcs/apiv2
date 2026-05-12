using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.dto.Association;
using apiv2.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace apiv2.Interfaces
{
    public interface IAssociationRepository
    {
        public Task<List<Association>> GetAllAsync();
        public Task<Association?> GetByIdAsync(int id);
        public Task<Association> CreateAsync(Association association);
        public Task<Association?> UpdateAsync(int id, AssociationPatchDto dto);
    }
}