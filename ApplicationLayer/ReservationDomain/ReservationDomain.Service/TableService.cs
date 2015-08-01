using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReservationDomain.Model;
using Common.Service;
using ReservationDomain.Infrastructure;

namespace Reservaton.Service
{
    public class TableService : ApplicationService<ReservationDomainDbContext>, ITableService
    {
        public TableService(ReservationDomainDbContext context)
            : base(context)
        {
        }
        public List<Table> GetTables()
        {
            return _context.Tables.ToList();
        }
        public void CreateTable(Table table)
        {
            _context.Tables.Add(table);
            _context.SaveChanges();
        }

        public void UpdateTable(Table table)
        {
            var tableEntity = _context.Tables.Find(table.Id);
            tableEntity.Reservations = table.Reservations;
            tableEntity.SeatsNumber = table.SeatsNumber;
            tableEntity.TableNumber = table.TableNumber;
            _context.SaveChanges();
        }

        public void RemoveTable(Table table)
        {
            var tableEntity = _context.Tables.Find(table.Id);
            _context.Tables.Remove(tableEntity);
            _context.SaveChanges();
        }
    }
}
