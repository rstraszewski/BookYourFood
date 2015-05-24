using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using ReservationDomain.Model;
using Common.Service;

namespace Reservaton.Service
{
    public class TableService : ApplicationService, ITableService
    {
        public TableService(ByfDbContext byfDbContext)
            : base(byfDbContext)
        {
        }
        public List<Table> GetTables()
        {
            return ByfDbContext.Tables.ToList();
        }
        public void CreateTable(Table table)
        {
            ByfDbContext.Tables.Add(table);
            ByfDbContext.SaveChanges();
        }

        public void UpdateTable(Table table)
        {
            var tableEntity = ByfDbContext.Tables.Find(table.Id);
            tableEntity.Reservations = table.Reservations;
            tableEntity.SeatsNumber = table.SeatsNumber;
            tableEntity.TableNumber = table.TableNumber;
            ByfDbContext.SaveChanges();
        }

        public void RemoveTable(Table table)
        {
            var tableEntity = ByfDbContext.Tables.Find(table.Id);
            ByfDbContext.Tables.Remove(tableEntity);
            ByfDbContext.SaveChanges();
        }
    }
}
