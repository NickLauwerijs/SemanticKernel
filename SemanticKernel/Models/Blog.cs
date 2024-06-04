
namespace SemanticKernel.Models
{
    public class Blog
    {
        public string Topic { get; set; } = "";
        public string Title { get; set; } = "";
        public string Subtitle { get; set; } = "";
        public string CoverDescription { get; set; }
        public string CoverUrl { get; set; } = "";
        public string TableOfContents { get; set; } = "";
        public string Chapters { get; set; }
    }
}
