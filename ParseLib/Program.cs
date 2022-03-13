// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Reflection;
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.VisualBasic;
using ParseLib.Core;
using ParseLib.Infrastructure;
using ParseLib.Models;

namespace ParseLib
{
    public class Holder
    {
        public List<string> Prop { get; set; }
        public string[] Prop2 { get; set; }
        public IEnumerable<int> InterList { get; set; }
        public ICollection NoGenList { get; set; }
    }

    public class Program
    {
        // Console.WriteLine(arrType.GetElementType());
        public static async Task Main(string[] args)
        {
            var obj = new Holder();
            var prop = obj.GetType().GetProperty("Prop2");
            var propType = prop.PropertyType;
            dynamic srcArr = Array.CreateInstance(propType.GetElementType(), 10);
            var rand = new Random();
            for (int i = 0; i < srcArr.Length; i++)
            {
                srcArr[i] = rand.Next(2) < 2 ? "Hello" : "yo";
            }

            Console.WriteLine(srcArr.GetType());
            dynamic dstArr = srcArr.ToList().Where(new Func<string, bool>((e) => e != "yo"));
            for (int i = 0; i < dstArr.Length; i++)
            {
                Console.WriteLine(dstArr[i]);
            }

            var data = new object[] {"1", "1", "1", "1", "1", "1", "1"};


            // objType.GetProperties().For(e=>Console.WriteLine(e.PropertyType.ContainsGenericParameters));
            // objType.GetProperties().For(e=>e.PropertyType.GetGenericArguments().For(Console.WriteLine));
            /*
            System.Collections.Generic.List`1[System.String]
            System.String[]
            System.Collections.Generic.IEnumerable`1[System.Int32]
            System.Collections.ICollection
            */
        }

        public static async Task GetTestData()
        {
            var configuration = ConfigFactory.CreateConfigurationIHerb();
            var document = await BrowsingContext.New(configuration).OpenAsync("https://www.iherb.com/c/vitamin-d");
            var blocks = document.QuerySelectorAll("div.product.ga-product");
            Console.WriteLine(blocks.Length);
            for (int i = 0; i < blocks.Length; i++)
            {
                Console.WriteLine(blocks[i]);
            }
        }

        // public static void Map(IDocument document, object obj, List<MapCommand> commands)
        // {
        //     var objType = obj.GetType();
        //     foreach (var (fieldName, selector, skipStart, skipEnd, enableDefault) in commands)
        //     {
        //         var prop = objType.GetProperty(fieldName) ??
        //                    throw new NoPropertyException($"Property with name {fieldName} not found");
        //         var propType = prop.PropertyType;
        //
        //         var selectorResult = document.QuerySelectorAll(selector);
        //
        //         // if (string.IsNullOrEmpty(selectorResult))
        //         // throw new($"QuerySelector {selector} not found in {document}");
        //
        //         if (propType == typeof(string)) //CASE STRING
        //         {
        //             if (selectorResult.Length != 0 && string.IsNullOrEmpty(selectorResult[0].InnerHtml))
        //             {
        //                 prop.SetValue(obj, selectorResult[0].InnerHtml);
        //             }
        //             else
        //             {
        //                 string value = enableDefault
        //                     ? ""
        //                     : throw new Exception(
        //                         $"Value of property {propType.Name} cant be null or default. Selector: {selector}, document: {document}");
        //                 prop.SetValue(obj, value);
        //             }
        //         }
        //         else if (NumericHelper.IsNumeric(propType)) //CASE NUMERIC
        //         {
        //             if (selectorResult.Length != 0 && string.IsNullOrEmpty(selectorResult[0].InnerHtml))
        //             {
        //                 prop.SetValue(obj, selectorResult[0].InnerHtml);
        //             }
        //             else
        //             {
        //                 string value = enableDefault
        //                     ? "0"
        //                     : throw new Exception(
        //                         $"Value of property {propType.Name} cant be null or default. Selector: {selector}, document: {document}");
        //                 prop.SetValue(obj, value);
        //             }
        //         }
        //         else if (propType.IsAssignableTo(typeof(IEnumerable))) //CASE ENUMERABLE
        //         {
        //             if (propType.IsInterface)
        //             {
        //                 var generic = propType.GetGenericArguments().FirstOrDefault(typeof(string));
        //                 // var tempArr = Array.CreateInstance();
        //             }
        //             else if (propType.IsArray)
        //             {
        //                 var tempArr = Activator.CreateInstance(propType);
        //             }
        //             else
        //             {
        //             }
        //         }
        //     }
        // }
    }
}