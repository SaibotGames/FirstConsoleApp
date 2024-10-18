using DTOs;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PostsController
{
    private readonly IPostRepository repository;
    private readonly ICommentRepository comRepository;

    public PostsController(IPostRepository repository, ICommentRepository comRepository)
    {
        this.repository = repository;
        this.comRepository = comRepository;
    }

    [HttpPost]
    public async Task<IResult> CreatePost(
        [FromBody] CreatePostDto createPostDto)
    {
        Post newPost = new Post()
        {
            Id = 1,
            Body = createPostDto.Body,
            Title = createPostDto.Title
        };
        var added = await repository.AddAsync(newPost);
        return Results.Created($"/posts/{added.Id}", added);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdatePost(int id,
        [FromBody] UpdatePostDto updatePostDto)
    {
        var existingPost = await repository.GetSingleAsync(id);
        existingPost.Body = updatePostDto.Body ?? existingPost.Body;
        await repository.UpdateAsync(existingPost);
        return Results.Accepted();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeletePost(int id)
    {
        await repository.DeleteAsync(id);
        return Results.NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetPost(int id)
    {
        var post = await repository.GetSingleAsync(id);
        return Results.Ok(MapToPostDto(post));
    }



    [HttpGet]
    public IActionResult GetAllPosts()
    {
        var posts = repository.GetMany().Select(MapToPostDto).ToList();
        return (IActionResult)Results.Ok(posts);
    }
    
    //COMMENTS
    
    [HttpGet("{id}/comments")]
    public async Task<IResult> GetComments(int id)
    {
        var post = await repository.GetSingleAsync(id);
        var comments = comRepository.GetMany().Where(c => c.PostId == id).Select(MapToCommentDto).ToList();
        return Results.Ok(comments);
    }

    [HttpPost("{postId}/comments")]
    public async Task<IResult> CreateComment(int postId,
        [FromBody] CreateCommentDto createCommentDto)
    {
        var post = await repository.GetSingleAsync(postId);
        var comment = new Comment(createCommentDto.Body, post.Id, createCommentDto.UserId);
        var commentAdded = await comRepository.AddAsync(comment);
        return Results.Created($"/comments/{commentAdded.Id}", commentAdded);
    }

    [HttpPut("{id}/comments/{commentId}")]
    public async Task<IResult> UpdateComment(int postId, int commentId,
        [FromBody] UpdateCommentDto updateCommentDto)
    {
        var existingComment = await comRepository.GetSingleAsync(commentId);
        if (existingComment.PostId != postId)
        {
            return Results.BadRequest("Comment doesn't belong to this post");
        }
        
        existingComment.Body = updateCommentDto.Body ?? existingComment.Body;
        await comRepository.UpdateAsync(existingComment);
        return Results.Accepted();
    }

    [HttpDelete("{id}/comments/{commentId}")]
    public async Task<IResult> DeleteComment(int postId, int commentId)
    {
        var existingComment = await comRepository.GetSingleAsync(commentId);
        if (existingComment.PostId != postId)
        {
            return Results.BadRequest("Comment doesn't belong to this post");
        }

        await comRepository.DeleteAsync(commentId);
        return Results.Ok();
    }

    [HttpGet("{id}/comments/{commentId}")]
    public async Task<IResult> GetComment(int postId, int commentId)
    {
        var comment = await comRepository.GetSingleAsync(commentId);
        if (comment.PostId != postId)
        {
            return Results.BadRequest("Comment doesn't belong to this post");
        }
        
        return Results.Ok(MapToCommentDto(comment));
    }

    private static PostDto MapToPostDto(Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Body = post.Body,
            UserId = post.UserId
        };

    }

    private static CommentDto MapToCommentDto(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Body = comment.Body,
            PostId = comment.PostId,
            UserId = comment.UserId
        };
    }
    
}
    
