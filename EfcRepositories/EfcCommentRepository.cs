using System.Globalization;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories
{
    public class EfcCommentRepository : ICommentRepository
    {
        private readonly AppContex ctx;

        public EfcCommentRepository(AppContex ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
            await ctx.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task UpdateAsync(Comment comment)
        {
            if (!(await ctx.Comments.AnyAsync(c => c.Id == comment.Id)))
            {
                throw new NotFoundException($"Comment with id {comment.Id} not found");
            }

            ctx.Comments.Update(comment);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
            if (existing == null)
            {
                throw new NotFoundException($"Comment with id {id} not found");
            }

            ctx.Comments.Remove(existing);
            await ctx.SaveChangesAsync();
        }

        public IQueryable<Comment> GetMany()
        {
            return ctx.Comments.AsQueryable();
        }

        public async Task DeleteManyAsync(int postId)
        {
            // Find all comments associated with the given postId
            var commentsToDelete = await ctx.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();

            // If no comments are found for the given postId, throw an exception
            if (!commentsToDelete.Any())
            {
                throw new NotFoundException($"No comments found for Post with id {postId}");
            }

            // Remove all the comments related to the PostId
            ctx.Comments.RemoveRange(commentsToDelete);
            await ctx.SaveChangesAsync();
        }

        public async Task<Comment> GetSingleAsync(int id)
        {
            Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
            if (existing == null)
            {
                throw new CultureNotFoundException("Comment not found");
            }
            return existing;
        }

        // Optionally, you could create additional methods for retrieving comments based on PostId or UserId
        public IQueryable<Comment> GetCommentsByPostId(int postId)
        {
            return ctx.Comments.Where(c => c.PostId == postId);
        }

        public IQueryable<Comment> GetCommentsByUserId(int userId)
        {
            return ctx.Comments.Where(c => c.UserId == userId);
        }
    }
}
