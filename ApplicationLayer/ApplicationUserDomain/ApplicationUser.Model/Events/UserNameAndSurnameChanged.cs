using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace ApplicationUserDomain.Model.Events
{
    public class UserNameAndSurnameChanged : IDomainEvent
    {
        public DateTime EventOccured { get; set; }
        public string OldName { get; set; }
        public string OldSurname { get; set; }
        public string NewName { get; set; }
        public string NewSurname { get; set; }

        public UserNameAndSurnameChanged()
        {
            
        }
    }
}
