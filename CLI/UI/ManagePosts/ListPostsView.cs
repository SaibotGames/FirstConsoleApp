using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Show post by ID");
        Console.WriteLine("2. Show all posts");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("Enter post ID:");
                int id = int.Parse(Console.ReadLine());
                await postRepository.GetSingleAsync(id);
                break;
            case "2":
                var posts = postRepository.GetMany();
                foreach (var post in posts)
                {
                    Console.WriteLine(post);
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}