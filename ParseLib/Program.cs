// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Collections.Immutable;
using System.Net.Http.Headers;
using System.Reflection;
using AngleSharp.Dom;
using ParseLib.Core;
using ParseLib.Infrastructure;
using ParseLib.Models;

namespace ParseLib
{
    public class Program
    {
        public static NumericHelper NumericHelper { get; set; }

        public static async Task Main(string[] args)
        {
            
            // var blocks = document.QuerySelectorAll("div.product.ga-product");
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
        //         else if (propType.IsAssignableTo(typeof(IList))) //CASE ENUMERABLE
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