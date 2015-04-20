using System.Collections.Generic;
using Common.Model;

namespace ReservationDomain.Model
{
    public class Answer : Entity
    {
        public string Value { get; set; }
        public virtual List<HashTag> HashTags { get; set; }
    }
}