using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Common.Service
{
    public class ApplicationService
    {
        protected readonly ByfDbContext ByfDbContext; 
        public ApplicationService(ByfDbContext byfDbContext)
        {
            ByfDbContext = byfDbContext;
        }
    }



}
