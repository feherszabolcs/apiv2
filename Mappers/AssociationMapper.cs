using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.dto.Association;
using apiv2.Models;

namespace apiv2.Mappers
{
    public static class AssociationMapper
    {
        public static AssociationDto GetAssociationDto(this Association association)
        {
            return new AssociationDto
            {
                Name = association.Name,
                Certificate = association.Certificate,
                Location = association.Location
            };
        }
        public static Association ToAssociation(this AssociationDto associationDto)
        {
            return new Association()
            {
                Name = associationDto.Name,
                Certificate = associationDto.Certificate,
                Location = associationDto.Location,
                Email = associationDto.Email
            };
        }
    }
}