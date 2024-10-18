using RepositoryContracts;
using Entities;
namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository comRepository;
    
    public SinglePostView(IPostRepository postRepository, ICommentRepository comRepository){
        this.postRepository = postRepository;
        this.comRepository = comRepository;
        }
    
    public async Task RunAsync(){
        Console.WriteLine("Enter post id");
        int id = int.Parse(Console.ReadLine());
        var post = postRepository.GetSingleAsync(id).Result;
        var comms = comRepository.GetMany();
        Console.WriteLine(post);
        foreach(var com in comms){
            if(com.PostId == post.Id){
            Console.WriteLine(com);
            }
            }
        Console.WriteLine("Choose option");
        Console.WriteLine("1. Write a comment");
        Console.WriteLine("0. Return to menu");
        var choice = Console.ReadLine();
        switch(choice){
            case "1":
                Console.WriteLine("Type your comment");
                var body = Console.ReadLine();
                var comment = new Comment(body, id, 1);
            await comRepository.AddAsync(comment);
            Console.WriteLine("Comment added");
            break;
            case "0":
            return;
            default:
                            Console.WriteLine("Invalid choice.");
                            break;
            }
        Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                }
            
}