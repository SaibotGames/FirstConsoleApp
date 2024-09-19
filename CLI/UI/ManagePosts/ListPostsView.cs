using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository comRepository;

    public ListPostsView(IPostRepository postRepository,ICommentRepository comRepository)
    {
        this.postRepository = postRepository;
        this.comRepository = comRepository;
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
                var post = postRepository.GetSingleAsync(id).Result;
                var comms = comRepository.GetMany();
                Console.WriteLine(post);
                foreach(var com in comms){
                    if(com.PostId == post.Id){
                            Console.WriteLine(com);
                      }
                            }
                
                break;
            case "2":
                var posts = postRepository.GetMany();
                foreach (var element in posts)
                {
                    Console.WriteLine(element);
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