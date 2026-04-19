namespace NoahStore.Core.Dto
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> message { get; set; }
    }
}
