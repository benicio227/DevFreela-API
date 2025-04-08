namespace DevFreela.Application.Models;
public class ProjectCommentViewModel
{
    public ProjectCommentViewModel(string content, string userName)
    {
        Content = content;
        UserName = userName;
    }

    public string Content { get; private set; }
    public string UserName { get; private set; }
}
