namespace MTGDeckBuilder.Data
{
    public class ExternalAuthInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
