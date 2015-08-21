namespace ReservationDomain.Model
{
    public class ReservationOwner
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        private ReservationOwner()
        {
            
        }

        private ReservationOwner(string fullName, string phoneNumber)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;

        }

        private ReservationOwner(string userId, string fullName, string phoneNumber)
        {
            UserId = userId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        public static ReservationOwner CreateTemporaryOwner(string fullName, string phoneNumber)
        {
            return new ReservationOwner(fullName, phoneNumber);

        }

        public static ReservationOwner CreateOwner(string userId, string fullName, string phoneNumber)
        {
            return new ReservationOwner(userId, fullName, phoneNumber);
        }
    }
}