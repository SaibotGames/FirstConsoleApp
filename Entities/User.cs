﻿namespace Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public List<Comment> Comments { get; set; } = [];
    public List<Post> Posts { get; set; } = [];

    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    override public string ToString()
    {
        return UserName;
    }
}