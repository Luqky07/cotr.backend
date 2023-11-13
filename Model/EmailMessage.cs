namespace cotr.backend.Model
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailMessage(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }

        public override bool Equals(object? obj)
        {
            return obj is EmailMessage message &&
                   To == message.To &&
                   Subject == message.Subject &&
                   Body == message.Body;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Body, Subject, To);
        }
    }
}
