using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("1. List by ID");
        Console.WriteLine("2. List all users");
        var option = Console.ReadLine();
        switch (option)
        {
            case "1":
                Console.WriteLine("Put id:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine(await userRepository.GetSingleAsync(id));
                break;
            case "2":
                var users =  userRepository.GetMany();
                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
                break;
            default:
                Console.WriteLine("Invalid option");
                break;
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        
    }
}