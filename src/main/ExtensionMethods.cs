using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace ei8.Cortex.Graph.Common
{
    public static class ExtensionMethods
    {
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
        private static IEnumerable<string> ConvertQueryParameters(IEnumerable<string> queryStrings, string parameterName) =>
            queryStrings.Where(s => ExtensionMethods.IsQueryParameter(s, parameterName)).Select(s => HttpUtility.UrlDecode(s.Substring(parameterName.Length + 1)));

        public static IEnumerable<string> GetQueryArrayOrDefault(this IEnumerable<string> queryStrings, string parameterName)
        {
            var parameterNameExclamation = parameterName.Replace("Not", "!");
            var stringArray = ExtensionMethods.QueryStringsContains(queryStrings, parameterName) ?
                ExtensionMethods.ConvertQueryParameters(queryStrings, parameterName) :
                ExtensionMethods.QueryStringsContains(queryStrings, parameterNameExclamation) ?
                    ExtensionMethods.ConvertQueryParameters(queryStrings, parameterNameExclamation) :
                    null;

            return stringArray != null ? stringArray.Select(s => s != "\0" && s != "%00" ? s : null) : stringArray;
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
