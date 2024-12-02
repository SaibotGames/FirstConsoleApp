namespace DTOs;

public class UserDto
{
  public int Id { get; set; }
  public string UserName { get; set; }
  
  public List<PostDto> Posts { get; set; }
  public List<CommentDto> Comments { get; set; }
}