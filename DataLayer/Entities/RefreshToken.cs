namespace DataLayer.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        //references

        public User User { get; set; }

    }
}