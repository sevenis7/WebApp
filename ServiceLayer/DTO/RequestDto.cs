namespace ServiceLayer.DTO
{
    public class RequestDto
    {
        public int RequestId { get; set; }

        public string Text { get; set; } = "";

        public string Status { get; set; } = "";

        public DateTime Date { get; set; } 

        public string Username { get; set; } = "";
    }
}
