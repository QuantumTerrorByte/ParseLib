using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;
using ParseLib.Models.DAO;

namespace ParseLib.Core
{
    public class ParserIHerb
    {
        public IBrowsingContext BrowsingContext { get; set; }

        public ParserIHerb(IBrowsingContext browsingContext)
        {
            BrowsingContext = browsingContext;
        }

        public async Task Parse(string url, int firstPage = default, int lastPage = default, string prefix = "")
        {
            if (firstPage < lastPage)
                for (int i = firstPage; i <= lastPage; i++)
                {
                    var document = await BrowsingContext.OpenAsync($"{url}{prefix}{i}");
                    Console.WriteLine(document.All.Length);
                    await GetProductsFromPage(document);
                }

            var document1 = await BrowsingContext.OpenAsync($"{url}");
            Console.WriteLine(document1.All.Length);
            await GetProductsFromPage(document1);
        }

        public async Task<List<Product>> GetProductsFromPage(IDocument document)
        {
            var result = new List<Product>(24);
            var productBlocks = document.QuerySelectorAll("div.product.ga-product");
            Console.WriteLine(productBlocks.Length);
            // Console.WriteLine(productBlocks[0].InnerHtml);
            foreach (IElement productBlock in productBlocks)
            {
                Console.WriteLine(productBlock.QuerySelector("div.product-title bdi").InnerHtml);
            }

            return result;
        }

        public void ParseProductBase(IElement html, Product product)
        {
            product.Name = html.QuerySelector("div.product-title bdi").InnerHtml;
            var priceStr = html.QuerySelector("span.price bdi").InnerHtml;
            var price = new Regex(@"\d*\.\d*").Match(priceStr)
                        ?? throw new Exception(
                            $"failed to match decimal in {(string.IsNullOrEmpty(priceStr) ? "___" : priceStr)}");
            product.PriceUsd = decimal.Parse(price.Value);
        }

    }

    
}

// public class Executor<TResult, TValue> where TResult : class, new()
// {
//     public TResult Execute(Dictionary<string, TValue> command, TResult obj)
//     {
//         TResult result = obj ?? new TResult();
//         Type targetType = result.GetType();
//         foreach (KeyValuePair<string, TValue> mapPair in command)
//         {
//             try
//             {
//                 PropertyInfo? prop = targetType.GetProperty(mapPair.Key);
//                 prop.SetValue(result, mapPair.Value);
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine($"Error occured when trying map {mapPair.Key} {mapPair.Value}");
//                 throw;
//             }
//         }
//
//         return result;
//     }
// }