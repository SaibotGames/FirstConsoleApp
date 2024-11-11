using DTOs;

namespace BlazorApp.Services;

public interface IPostService
{
    Task<CreatePostDto> AddPostAsync(CreatePostDto post);
    Task UpdateAsync(UpdatePostDto post, int id);
    Task DeletePostAsync(int id);
    Task<PostDto> GetSinglePostAsync(int id);
    Task<List<PostDto>> GetManyPosts();
    
    Task<CreateCommentDto> AddCommentAsync(CreateCommentDto comment, int id);
    Task UpdateCommentAsync(UpdateCommentDto comment, int postId, int commentId);
    Task DeleteCommentAsync( int postId, int commentId);
    Task<CommentDto> GetSingleCommentAsync(int postId, int commentId);
    Task<List<CommentDto>> GetManyComments(int postId);
}