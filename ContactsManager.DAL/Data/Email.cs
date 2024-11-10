namespace ContactsManager.DAL.Data
{
    public class Email
    {
        public int EmailId { get; set; }
        public required string EmailAddress { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int TypologyId { get; set; }
        public Typology? Typology { get; set; }
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
