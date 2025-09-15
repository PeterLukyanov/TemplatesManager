namespace Models;

public class Template
{
    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Content { get; private set; } = null!;

    public Template(string name, string content)
    {
        Name = name;
        Content = content;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeContent(string content)
    {
        Content = content;
    }
}