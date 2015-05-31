using ReservationDomain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservaton.Service
{
    public interface ITableService
    {
        List<Table> GetTables();
        void CreateTable(Table table);
        void UpdateTable(Table table);
        void RemoveTable(Table table);

    }
}
