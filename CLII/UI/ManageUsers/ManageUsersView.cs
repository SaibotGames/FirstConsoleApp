using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        var createUserView = new CreateUsersView(userRepository);
        var listUsersView = new ListUsersView(userRepository);
        
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1) Create a new user");
        Console.WriteLine("2) Find users");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                await createUserView.RunAsync();
                break;
            case "2":
                await listUsersView.RunAsync();
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
}