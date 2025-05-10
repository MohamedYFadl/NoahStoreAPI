namespace NoahStore.Core.Dto
{
    public class EmailDto
    {
        public EmailDto(string to, string from ,string subject, string message)
        {
            To = to;
            From = from;
            Subject = subject;
            Message = message;
        }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
