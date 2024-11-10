namespace ContactsManager.DAL.Data
{
    public class Contact
    {
        public int ContactId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<PhoneNumber>? PhoneNumbers { get; set; }
        public ICollection<Email>? Emails { get; set; }

        public Contact() 
        { 
            PhoneNumbers = new List<PhoneNumber>();
            Emails = new List<Email>();
        }
    }
}