using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Question : Entity
    {
        public string Value { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
