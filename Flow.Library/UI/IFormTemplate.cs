namespace Flow.Library.UI
{
    public interface IFormTemplate
    {
        int Id { get; set; }
        string Name { get; set; }
        string Body { get; set; }
        string Head { get; set; }
        string Html { get; set; }
    }
}