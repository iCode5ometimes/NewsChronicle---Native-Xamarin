using System.Collections.Generic;
using System.Threading.Tasks;
using NewsChronicle.Data.Model;
using System.Linq;
using NewsChronicle.Data.Model.DTOs;
using System.Threading;

namespace NewsChronicle.Data.Utils
{
    public static class Utilities
    {
        public static async Task<List<Article>> ToModelAsync(ArticleListDTO articleListDto, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return articleListDto?.Articles?.Select(article => new Article(article)).ToList() ?? new List<Article>();
            }, token);
        }

        public static string GetCountryIconIdentifier(string country)
        {
            string iconIdentifier = string.Empty;

            switch (country)
            {
                case "en":
                    {
                        iconIdentifier = "IconAmerica";
                        break;
                    }
                case "ro":
                    {
                        iconIdentifier = "IconRomania";
                        break;
                    }
                case "it":
                    {
                        iconIdentifier = "IconItaly";
                        break;
                    }
                case "de":
                    {
                        iconIdentifier = "IconGermany";
                        break;
                    }
                case "es":
                    {
                        iconIdentifier = "IconSpain";
                        break;
                    }
                case "fr":
                    {
                        iconIdentifier = "IconFrance";
                        break;
                    }
                case "nl":
                    {
                        iconIdentifier = "IconNetherlands";
                        break;
                    }
                case "no":
                    {
                        iconIdentifier = "IconNorway";
                        break;
                    }
                case "pt":
                    {
                        iconIdentifier = "IconPortugal";
                        break;
                    }
                case "ru":
                    {
                        iconIdentifier = "IconRussia";
                        break;
                    }
                case "he":
                    {
                        iconIdentifier = "IconIsrael";
                        break;
                    }
            }
            return iconIdentifier;
        }
    }
}
