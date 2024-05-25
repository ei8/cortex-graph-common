using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ei8.Cortex.Graph.Common
{
    public static class ExtensionMethods
    {
        public static void AppendQuery<T>(this IEnumerable<T> field, string fieldName, StringBuilder queryStringBuilder, bool convertNulls = false, Func<T, string> fieldSelector = null)
        {
            if (field != null && field.Any())
            {
                if (queryStringBuilder.Length > 0)
                    queryStringBuilder.Append('&');

                IEnumerable<string> fieldValues = null;
                if (fieldSelector != null)
                    fieldValues = field.Select(fieldSelector);
                else
                    fieldValues = field.Cast<string>();

                queryStringBuilder.Append(string.Join("&", fieldValues.Select(s => $"{fieldName}={(convertNulls && s == null ? "\0" : HttpUtility.UrlEncode(s))}")));
            }
        }

        public static void AppendQuery<T>(this Nullable<T> nullableValue, string queryStringKey, Func<T, string> valueProcessor, StringBuilder queryStringBuilder) where T : struct
        {
            if (nullableValue.HasValue)
            {
                if (queryStringBuilder.Length > 0)
                    queryStringBuilder.Append('&');

                queryStringBuilder
                    .Append($"{queryStringKey}=")
                    .Append(valueProcessor(nullableValue.Value));
            }
        }

        public static int? GetNullableIntValue(this IEnumerable<string> queryStrings, string fieldName) =>
            ExtensionMethods.QueryStringsContains(queryStrings, fieldName) ? 
                (int?) int.Parse(ExtensionMethods.GetSingleQueryParameter(queryStrings, fieldName)) : 
                null;

        public static bool? GetNullableBoolValue(this IEnumerable<string> queryStrings, string fieldName) =>
            ExtensionMethods.QueryStringsContains(queryStrings, fieldName) ?
                (bool?) bool.Parse(ExtensionMethods.GetSingleQueryParameter(queryStrings, fieldName)) :
                null;

        public static T? GetNullableEnumValue<T>(this IEnumerable<string> queryStrings, string fieldName) where T : struct, Enum =>
            ExtensionMethods.QueryStringsContains(queryStrings, fieldName) ? 
                (T?) Enum.Parse(typeof(T), ExtensionMethods.GetSingleQueryParameter(queryStrings, fieldName), true) : 
                null;

        private static bool IsQueryParameter(string query, string parameterName) =>
            query.StartsWith(parameterName + "=", true, null);
        private static bool QueryStringsContains(IEnumerable<string> queryStrings, string parameterName) =>
            queryStrings.Any(s => ExtensionMethods.IsQueryParameter(s, parameterName));
        private static string GetSingleQueryParameter(IEnumerable<string> queryStrings, string parameterName) =>
            queryStrings.SingleOrDefault(s => ExtensionMethods.IsQueryParameter(s, parameterName))?.Substring(parameterName.Length + 1);
        private static IEnumerable<T> ConvertQueryParameters<T>(IEnumerable<string> queryStrings, string parameterName, Func<string, T> converter) =>
            queryStrings.Where(s => ExtensionMethods.IsQueryParameter(s, parameterName)).Select(s => converter(HttpUtility.UrlDecode(s.Substring(parameterName.Length + 1))));

        public static IEnumerable<string> GetQueryArrayOrDefault(this IEnumerable<string> queryStrings, string parameterName) =>
            queryStrings.GetQueryArrayOrDefault<string>(parameterName, new Func<string, string>(value => value != "\0" && value != "%00" ? value : null));

        public static IEnumerable<T> GetQueryArrayOrDefault<T>(this IEnumerable<string> queryStrings, string parameterName, Func<string, T> converter)
        {
            var parameterNameExclamation = Regex.Replace(parameterName, "Not", "!", RegexOptions.IgnoreCase);
            var resultArray = ExtensionMethods.QueryStringsContains(queryStrings, parameterName) ?
                ExtensionMethods.ConvertQueryParameters(queryStrings, parameterName, converter) :
                ExtensionMethods.QueryStringsContains(queryStrings, parameterNameExclamation) ?
                    ExtensionMethods.ConvertQueryParameters(queryStrings, parameterNameExclamation, converter) :
                    null;

            return resultArray;
        }

        public static string GetQueryKey(this Type type, string propertyName)
        {
            var pInfo = type.GetProperty(propertyName);

            if (pInfo == null)
                throw new ArgumentOutOfRangeException($"Specified property with name '{propertyName}' was not found.");

            var qk = pInfo.GetCustomAttribute<QueryKeyAttribute>();

            if (qk == null)
                throw new ArgumentOutOfRangeException($"Specified property with name '{propertyName}' does not have a QueryKeyAttribute.");

            return !string.IsNullOrWhiteSpace(qk.Value) ? qk.Value : propertyName;
        }
    }
}
