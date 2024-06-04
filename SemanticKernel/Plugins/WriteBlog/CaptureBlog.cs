using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.TextToImage;
using SemanticKernel.Models;
using System.ComponentModel;
using System.Text;

namespace SemanticKernel.Plugins.WriteBlog
{
    public class CaptureBlog
    {
        public Blog Blog = new Blog();

        [KernelFunction]
        [Description("return the image style of the cover")]
        public string CoverImageStyle()
        {
            return "Abstract Art";
            //example: Pixel Art | Abstract Art | Minimalist | Infographic | Pop Art | Photographic | Satirical Cartoon 
        }

        [KernelFunction]
        [Description("Topic")]
        public string Topic(Kernel kernel, [Description]string topic)
        {
            Blog.Topic = topic;
            return topic;
        }

        [KernelFunction]
        [Description("blog title")]
        public string Title(Kernel kernel, string title)
        {
            //char[] trimChars = new char[] { '\"', '\r', '\n' };
            //string result = input.Trim(trimChars);
            //context.Variables.Set("title", result);
            Blog.Title = title;
            return title;
        }

        [KernelFunction]
        [Description("blog subtitle")]
        public string Subtitle(Kernel kernel, string subtitle)
        {
            //char[] trimChars = new char[] { '\"', '\r', '\n' };
            //string result = input.Trim(trimChars);
            //context.Variables.Set("subtitle", result);

            Blog.Subtitle = subtitle;
            return subtitle;
        }

        [KernelFunction]
        [Description("blog cover")]
        public string Cover(Kernel kernel, string cover)
        {
            Blog.CoverDescription = cover;
            return cover;
        }

        [KernelFunction]
        [Description("generates the blog cover")]
        public async Task<string> GenerateCoverImage(Kernel kernel, 
        [Description("Description how the cover should look like ")] string coverDescription)
        {
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            ITextToImageService dallE = kernel.GetRequiredService<ITextToImageService>();
            Blog.CoverUrl = await dallE.GenerateImageAsync(coverDescription, 1024, 1024);
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            //using (WebClient client = new WebClient())
            //{
            //    string url = image;
            //    Uri uri = new Uri(url);
            //    string localPath = System.IO.Path.GetFileName(uri.LocalPath);

            //    client.DownloadFile(url, localPath);
            //}

            return Blog.CoverUrl;
        }

        [KernelFunction]
        [Description("Saves blog to a file")]
        public void SaveBlogToFile(Kernel kernel, 
            [Description("File name you want to use to save the blog")]string nameOfBlog)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"# {Blog.Title}");
            sb.AppendLine($"{Blog.Subtitle}\r\n");
            sb.AppendLine($"![{Blog.Title}]({Blog.CoverUrl})\r\n");
            sb.AppendLine($"{Blog.TableOfContents}\r\n");
            sb.AppendLine($"{Blog.Chapters}\r\n");
            using (StreamWriter sw = new StreamWriter(nameOfBlog))
            {
                sw.Write(sb.ToString());
            }
        }

        [KernelFunction]
        [Description("blog table of contents")]
        public string TableOfContents(Kernel kernel, string tableOfContents)
        {
            Blog.TableOfContents = tableOfContents;
            return tableOfContents;
        }

        [KernelFunction]
        [Description("blog chapters")]
        public string Chapters(Kernel kernel, string chapters)
        {
            Blog.Chapters = chapters;
            return chapters;
        }
    }
}
