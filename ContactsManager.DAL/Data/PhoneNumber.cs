namespace ContactsManager.DAL.Data
{
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }
        public required string Number { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int TypologyId { get; set; }
        public Typology? Typology { get; set; }
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
    }
}
