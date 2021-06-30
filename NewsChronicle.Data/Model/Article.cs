using NewsChronicle.Data.Model.DTOs;
using SQLite;

namespace NewsChronicle.Data.Model
{
    [Table("articles")]
    public class Article
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string SourceName { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(250)]
        public string UrlToArticle { get; set; }

        [MaxLength(250)]
        public string UrlToArticleImage { get; set;}

        [MaxLength(250)]
        public string PublishedAt { get; set; }

        [MaxLength(250)]
        public string Content { get; set; }

        public Article()
        {
        }

        public Article(ArticleDTO article)
        {
            if(article != null)
            {
                if(article.Source != null)
                {
                    SourceName = $"{article.Source.Name} ";
                }
                Title = article.Title;
                Description = article.Description;
                UrlToArticle = article.Url;
                UrlToArticleImage = article.UrlToImage;
                PublishedAt = article.PublishedAt.ToString("dd MMM yyyy");

                var tempContent = "\t" + article.Description;
                tempContent += article.Content;

                // since the api returns contents of the website along with some html tags too
                // this will try and remove as many as possible
                Content = string.Join("", System.Text.RegularExpressions.Regex.Split(tempContent, @"(?:\r\n|\n|\r|<li>|<ul>)"));
            }
        }
    }
}
