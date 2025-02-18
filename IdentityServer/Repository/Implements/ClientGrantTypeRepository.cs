﻿using IdentityServer.Models.Context;
using IdentityServer.Models.DomainClasses;
using IdentityServer.Repository.Interfaces;

namespace IdentityServer.Repository.Implements
{
    public class ClientGrantTypeRepository : GenericRepository<ClientGrantType>, IClientGrantTypeRepository
    {
        public ClientGrantTypeRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
