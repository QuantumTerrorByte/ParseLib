using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom;
using ParseLib.Models;
using ParseLib.Models.DAO;

namespace ParseLib.Core
{
    public class Parser
    {
        public IBrowsingContext BrowsingContext { get; }

        public NumericHelper NumericHelper { get; } = new NumericHelper();

        public Parser(IBrowsingContext browsingContext)
            => BrowsingContext = browsingContext;

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
            var nHelper = new NumericHelper();
            product.Name = html.QuerySelector("div.product-title bdi")?.InnerHtml;
            var priceStr = html.QuerySelector("span.price bdi")?.InnerHtml;
            var price = new Regex(@"\d*\.\d*").Match(priceStr)
                        ?? throw new Exception(
                            $"failed to match decimal in {(string.IsNullOrEmpty(priceStr) ? "___" : priceStr)}");
            product.PriceUsd = decimal.Parse(price.Value);
        }

        public void Map(IDocument document, object obj, List<MapCommand> commands)
        {
            var objType = obj.GetType();
            foreach (var (fieldName, selector, skipStart, skipEnd, enableDefault) in commands)
            {
                var prop = objType.GetProperty(fieldName) ??
                           throw new NoPropertyException($"Property with name {fieldName} not found");
                var propType = prop.PropertyType;
                IHtmlCollection<IElement> selectorResult = document.QuerySelectorAll(selector);

                if (propType == typeof(string)) //CASE STRING
                {
                    if (selectorResult.Length != 0 && string.IsNullOrEmpty(selectorResult[0].InnerHtml))
                    {
                        prop.SetValue(obj, selectorResult[0].InnerHtml);
                    }
                    else
                    {
                        string value = enableDefault
                            ? ""
                            : throw new Exception(
                                $"Value of property {propType.Name} cant be null or default. Selector: {selector}, document: {document}");
                        prop.SetValue(obj, value);
                    }
                }
                else if (NumericHelper.IsNumeric(propType)) //CASE NUMERIC
                {
                    if (selectorResult.Length != 0 && string.IsNullOrEmpty(selectorResult[0].InnerHtml))
                    {
                        prop.SetValue(obj, selectorResult[0].InnerHtml);
                    }
                    else
                    {
                        string value = enableDefault
                            ? "0"
                            : throw new Exception(
                                $"Value of property {propType.Name} cant be null or default. Selector: {selector}, document: {document}");
                        prop.SetValue(obj, value);
                    }
                }
                else if (propType.IsAssignableTo(typeof(IEnumerable)))
                {
                    if (propType.IsInterface)
                    {
                        var generic = propType.GetGenericArguments().FirstOrDefault(typeof(string));
                        var tempArr = (IList) Array.CreateInstance(generic, selectorResult.Length);

                        if (enableDefault)
                        {
                            for (int i = skipStart; i < selectorResult.Length - skipEnd; i++)
                            {
                                tempArr[i] = selectorResult[i].InnerHtml;
                            }
                        }

                        var arr = selectorResult.Skip(skipStart).SkipLast(skipEnd).Select(e => e.InnerHtml);


                        prop.SetValue(obj, tempArr);
                    }
                    else if (propType.IsArray)
                    {
                        var tempArr = Activator.CreateInstance(propType.GetType(), selectorResult.Length);
                    }
                    else
                    {
                        var generic = propType.GetGenericArguments().FirstOrDefault(typeof(string));
                        var tempArr = Array.CreateInstance(generic, selectorResult.Length);
                    }
                }
            }
        }

        public void MapIList(IHtmlCollection<Element> selectorResult, object obj,
            PropertyInfo prop, MapCommand command)
        {
            var propType = prop.PropertyType;

            if (propType.IsAssignableTo(typeof(IEnumerable))) // IEnumerable, ICollection, IList, List, Set, Array 
            {
                Type? resultType = null;
                dynamic arrayDTO;

                if (propType.IsArray)
                    resultType = propType.GetElementType();
                else
                    resultType = propType.GetGenericArguments().FirstOrDefault(typeof(string));

                if (resultType == null)
                    throw new Exception(
                        $"Undefined type: {propType}, available types: IEnumerable, ICollection, IList, List, Set, Array ");

                arrayDTO = Array.CreateInstance(resultType,
                    selectorResult.Length - (command.SkipStart + command.SkipEnd));

                if (NumericHelper.IsNumeric(resultType)) // what is better forgot dry or waste performance?
                {
                    for (int i = command.SkipStart; i < selectorResult.Length - command.SkipEnd; i++)
                    {
                        var content = selectorResult[i].InnerHtml;
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            arrayDTO[i] = NumericHelper.ParseNumeric(resultType, content);
                        }
                    }

                    if (!command.EnableDefaultValue)
                    {
                        //todo clear arr, search info dynamic with extension methods
                    }
                }
                else
                {
                    for (int i = command.SkipStart; i < selectorResult.Length - command.SkipEnd; i++)
                    {
                        var content = selectorResult[i].InnerHtml;
                        if (!string.IsNullOrWhiteSpace(content) || command.EnableDefaultValue)
                        {
                            arrayDTO[i] = content;
                        }
                    }
                }

                if (propType.IsArray)
                {
                    prop.SetValue(obj, arrayDTO);
                }
                else
                {
                    try
                    {
                        dynamic temp = Activator.CreateInstance(propType, arrayDTO);
                        prop.SetValue(obj, temp);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
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