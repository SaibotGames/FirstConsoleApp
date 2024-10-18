using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostView
{
    private readonly IPostRepository postRepository;

    public ManagePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Edit post");
        Console.WriteLine("2. Delete post");
        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.WriteLine("Enter posts id:");
                int id = int.Parse(Console.ReadLine());
                var oldPost = postRepository.GetSingleAsync(id).Result;
                Console.WriteLine("Enter title:");
                var title = Console.ReadLine();
                Console.WriteLine("Enter body:");
                var body = Console.ReadLine();
                var post = new Post(title, body, oldPost.UserId);
                await postRepository.UpdateAsync(post);
                Console.WriteLine("Post updated");
                break;
            case "2":
                Console.WriteLine("Enter post id:");
                int idToDelete = int.Parse(Console.ReadLine());
                await postRepository.DeleteAsync(idToDelete);
                Console.WriteLine("Post deleted");
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
                
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}