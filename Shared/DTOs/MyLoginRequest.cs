namespace DTOs;

public class MyLoginRequest()
{
    public string Username { get; set; }
    public string Password { get; set; }

    public MyLoginRequest(string username, string password) : this()
    {
        this.Username = username;
        this.Password = password;
    }
}