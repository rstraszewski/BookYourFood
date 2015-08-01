using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationUserDomain.Infrastructure;

namespace Common.Service
{
    public class ApplicationService<T>
    {
        protected readonly T _context; 
        public ApplicationService(T context)
        {
            _context = context;
        }
    }



}
