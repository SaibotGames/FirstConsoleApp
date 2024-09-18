using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private IUserRepository userRepository;
    private IPostRepository postRepository;
    private ICommentRepository commentRepository;

    public CliApp(IUserRepository userRepository,
        IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. User management");
            Console.WriteLine("2. Post management");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    //await MangeUsersAsync();
                    var manageUsers = new ManageUsersView(userRepository);
                    await manageUsers.RunAsync();
                    break;
                case "2":
                    await MangePostsAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    private async Task MangeUsersAsync()
    {
        Console.Clear();
        Console.WriteLine("User Management:");
        Console.WriteLine("1. Add User");
        Console.WriteLine("2. Find Users");
        Console.WriteLine("0. Return to main menu");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                var createUserView = new CreateUsersView(userRepository);
                await createUserView.RunAsync();
                break;
            case "2":
                var listUsersView = new ListUsersView(userRepository);
                await listUsersView.RunAsync();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }

    private async Task MangePostsAsync()
    {
        Console.Clear();
        Console.WriteLine("Post Management:");
        Console.WriteLine("1. Add Post");
        Console.WriteLine("2. Find Posts");
        Console.WriteLine("3. Manage Posts");
        Console.WriteLine("0. Return to main menu");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                var createPostView = new CreatePostView(postRepository);
                await createPostView.RunAsync();
                break;
            case "2":
                var listPostsView = new ListPostsView(postRepository);
                await listPostsView.RunAsync();
                break;
            case "3":
                var managePostView = new ManagePostView(postRepository);
                await managePostView.RunAsync();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Invalid input");
                break;
        }
    }
}