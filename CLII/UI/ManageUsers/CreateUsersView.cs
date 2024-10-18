using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUsersView
{
    private readonly IUserRepository userRepository;

    public CreateUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Enter user name: ");
        var username = Console.ReadLine();
        Console.WriteLine("Enter password: ");
        var password = Console.ReadLine();
        
        await userRepository.AddAsync(new User(username, password));
        
        Console.WriteLine("User added");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}