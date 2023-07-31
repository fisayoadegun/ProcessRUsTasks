namespace ProcessRUsTasks.Dtos
{
    public class LoginResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
