namespace LydFramework.Application.Contracts.LydServers.Users.Dtos
{
    public class LoginRDto
    {
        public long Id { get; set; }
        public string UserName { get; set;}
        public string Status { get; set; }
        public List<string>? Roles { get; set; }
        public string accessToken { get; set; }
        public LoginRDto() 
        { 
            Roles=new List<string>();
        }
    }
    
}
