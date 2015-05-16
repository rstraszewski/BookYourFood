using System;

namespace Reservaton.Service
{
    public class ReservationDto
    {
        public DateTime ReservationTime { get; set; }

        public string ReservationTimeAsString
        {
            get { return ReservationTime.ToShortDateString() + " " + ReservationTime.ToShortTimeString(); }
        }

        public DateTime ReservationDate
        {
            get
            {
                return ReservationTime.Date;  
            } 
        }

        public int Duration { get; set; }
        public long TableNumber { get; set; }
        public string UserSurname { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}