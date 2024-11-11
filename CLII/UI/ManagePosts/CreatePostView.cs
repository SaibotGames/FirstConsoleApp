using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Input title:");
        var title = Console.ReadLine();
        Console.WriteLine("Input the body:");
        var body = Console.ReadLine();
        Console.WriteLine("Input author id:");
        int author = int.Parse(Console.ReadLine());
        await postRepository.AddAsync(new Post(title, body, author));
        Console.WriteLine("Post added");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    } 
}