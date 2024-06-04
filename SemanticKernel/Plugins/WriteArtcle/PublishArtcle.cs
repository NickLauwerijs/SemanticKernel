using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Text;

namespace SemanticKernel.Plugins.WriteArtcle
{
    public class PublishArtcle
    {
        [KernelFunction]
        [Description("Writes article to filesystem")]
        public void WriteToFileSystem(
            Kernel kernel,
            [Description("title of the article")] string title,
            [Description("subtitle of the article")] string subtitle,
            [Description("content of the article")] string content,
            [Description("url of the article image")] string imageUrl)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"# {title}");
            sb.AppendLine($"{subtitle}\r\n");
            sb.AppendLine($"![articleImage]({imageUrl})\r\n");
            sb.AppendLine($"{content}\r\n");
            using (StreamWriter sw = new StreamWriter("article.md"))
            {
                sw.Write(sb.ToString());
            }
        }

        [KernelFunction]
        [Description("Post the article to server")]
        public void PostToServer(
            Kernel kernel,
            [Description("title of the article")] string title,
            [Description("subtitle of the article")] string subtitle,
            [Description("content of the article")] string content,
            [Description("url of the article image")] string imageUrl)
        {
            Console.WriteLine($"Posted Article:{title} to server");
        }
    }
}
