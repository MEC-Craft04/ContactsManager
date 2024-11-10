namespace ContactsManager.DAL.Data
{
    public class Typology
    {
        public int TypologyId { get; set; }
        public required string TypologyName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<PhoneNumber>? PhoneNumbers { get; set; }
        public ICollection<Email>? Emails { get; set; }

        public Typology() 
        {
            PhoneNumbers = new List<PhoneNumber>();
            Emails = new List<Email>();
        }
    }
}
