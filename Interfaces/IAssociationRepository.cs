using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiv2.Interfaces
{
    public interface IAssociationRepository
    {
        public Task<List<Association>> GetAllAsync();
        public Task<Association?> GetByIdAsync(int id);
        public Task<Association> CreateAsync(Association association);
    }
}