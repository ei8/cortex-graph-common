using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace ei8.Cortex.Graph.Common
{
    public class NeuronQuery
    {
        [QueryKey]
        public IEnumerable<string> Postsynaptic { get; set; }

        [QueryKey]
        public IEnumerable<string> PostsynapticNot { get; set; }

        [QueryKey]
        public IEnumerable<string> Presynaptic { get; set; }

        [QueryKey]
        public IEnumerable<string> PresynapticNot { get; set; }

        [QueryKey]
        public IEnumerable<string> TagContains { get; set; }

        [QueryKey]
        public IEnumerable<string> TagContainsNot { get; set; }

        [QueryKey("tagcontainsiw")]
        public bool? TagContainsIgnoreWhitespace { get; set; }

        [QueryKey]
        public IEnumerable<string> Id { get; set; }

        [QueryKey]
        public IEnumerable<string> IdNot { get; set; }

        [QueryKey]
        public IEnumerable<string> RegionId { get; set; }

        [QueryKey]
        public IEnumerable<string> RegionIdNot { get; set; }

        [QueryKey("relative")]
        public RelativeValues? RelativeValues { get; set; }

        [QueryKey("nactive")]
        public ActiveValues? NeuronActiveValues { get; set; }
        
        [QueryKey("tactive")]
        public ActiveValues? TerminalActiveValues { get; set; }

        [QueryKey("sortby")]
        public SortByValue? SortBy { get; set; }

        [QueryKey("sortorder")]
        public SortOrderValue? SortOrder { get; set; }
        
        [QueryKey("pagesize")]
        public int? PageSize { get; set; }

        [QueryKey("page")]
        public int? Page { get; set; }

        [QueryKey("erurl")]
        public IEnumerable<string> ExternalReferenceUrl { get; set; }

        [QueryKey("erurlcontains")]
        public IEnumerable<string> ExternalReferenceUrlContains { get; set; }

        public override string ToString()
        {
            var queryStringBuilder = new StringBuilder();

            var nqType = typeof(NeuronQuery);

            this.AppendQuery(this.Id, nqType.GetQueryKey(nameof(NeuronQuery.Id)), queryStringBuilder);
            this.AppendQuery(this.IdNot, nqType.GetQueryKey(nameof(NeuronQuery.IdNot)), queryStringBuilder);
            this.AppendQuery(this.TagContains, nqType.GetQueryKey(nameof(NeuronQuery.TagContains)), queryStringBuilder);
            this.AppendQuery(this.TagContainsNot, nqType.GetQueryKey(nameof(NeuronQuery.TagContainsNot)), queryStringBuilder);

            this.AppendQuery(
                this.TagContainsIgnoreWhitespace,
                nqType.GetQueryKey(nameof(NeuronQuery.TagContainsIgnoreWhitespace)), 
                v => v.ToString(),
                queryStringBuilder
                );

            this.AppendQuery(this.Presynaptic, nqType.GetQueryKey(nameof(NeuronQuery.Presynaptic)), queryStringBuilder);
            this.AppendQuery(this.PresynapticNot, nqType.GetQueryKey(nameof(NeuronQuery.PresynapticNot)), queryStringBuilder);
            this.AppendQuery(this.Postsynaptic, nqType.GetQueryKey(nameof(NeuronQuery.Postsynaptic)), queryStringBuilder);
            this.AppendQuery(this.PostsynapticNot, nqType.GetQueryKey(nameof(NeuronQuery.PostsynapticNot)), queryStringBuilder);
            this.AppendQuery(this.RegionId, nqType.GetQueryKey(nameof(NeuronQuery.RegionId)), queryStringBuilder, true);
            this.AppendQuery(this.RegionIdNot, nqType.GetQueryKey(nameof(NeuronQuery.RegionIdNot)), queryStringBuilder, true);
            this.AppendQuery(this.ExternalReferenceUrl, nqType.GetQueryKey(nameof(NeuronQuery.ExternalReferenceUrl)), queryStringBuilder);
            this.AppendQuery(this.ExternalReferenceUrlContains, nqType.GetQueryKey(nameof(NeuronQuery.ExternalReferenceUrlContains)), queryStringBuilder);

            this.AppendQuery(
                    this.RelativeValues,
                    nqType.GetQueryKey(nameof(NeuronQuery.RelativeValues)),
                    v => ((int)v).ToString(),
                    queryStringBuilder
                    );

            this.AppendQuery(
                    this.PageSize,
                    nqType.GetQueryKey(nameof(NeuronQuery.PageSize)),
                    v => v.ToString(),
                    queryStringBuilder
                    );

            this.AppendQuery(
                    this.Page,
                    nqType.GetQueryKey(nameof(NeuronQuery.Page)),
                    v => v.ToString(),
                    queryStringBuilder
                    );

            this.AppendQuery(
                    this.NeuronActiveValues,
                    nqType.GetQueryKey(nameof(NeuronQuery.NeuronActiveValues)),
                    v => ((int)v).ToString(),
                    queryStringBuilder
                    );

            this.AppendQuery(
                    this.TerminalActiveValues,
                    nqType.GetQueryKey(nameof(NeuronQuery.TerminalActiveValues)),
                    v => ((int)v).ToString(),
                    queryStringBuilder
                    );

            this.AppendQuery(
                    this.SortOrder,
                    nqType.GetQueryKey(nameof(NeuronQuery.SortOrder)),
                    v => ((int)v).ToString(),
                    queryStringBuilder
                    );

            this.AppendQuery(
                    this.SortBy,
                    nqType.GetQueryKey(nameof(NeuronQuery.SortBy)),
                    v => ((int)v).ToString(),
                    queryStringBuilder
                    );

            if (queryStringBuilder.Length > 0)
                queryStringBuilder.Insert(0, '?');

            return queryStringBuilder.ToString();
        }

        private void AppendQuery(IEnumerable<string> field, string fieldName, StringBuilder queryStringBuilder, bool convertNulls = false)
        {
            if (field != null && field.Any())
            {
                if (queryStringBuilder.Length > 0)
                    queryStringBuilder.Append('&');
                queryStringBuilder.Append(string.Join("&", field.Select(s => $"{fieldName}={(convertNulls && s == null ? "\0" : HttpUtility.UrlEncode(s))}")));
            }
        }
        private void AppendQuery<T>(Nullable<T> nullableValue, string queryStringKey, Func<T, string> valueProcessor, StringBuilder queryStringBuilder) where T : struct
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

        public static bool TryParse(string value, out NeuronQuery result)
        {
            result = null;

            value = value.StartsWith("?") ? value.Substring(1) : value;
            var queryStrings = value.Split('&');

            if (queryStrings.Length > 0)
            {
                var nqType = typeof(NeuronQuery);
                result = new NeuronQuery
                {
                    TagContains = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.TagContains))),
                    TagContainsNot = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.TagContainsNot))),
                    TagContainsIgnoreWhitespace = queryStrings.GetNullableBoolValue(nqType.GetQueryKey(nameof(NeuronQuery.TagContainsIgnoreWhitespace))),
                    Id = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.Id))),
                    IdNot = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.IdNot))),
                    Postsynaptic = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.Postsynaptic))),
                    PostsynapticNot = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.PostsynapticNot))),
                    Presynaptic = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.Presynaptic))),
                    PresynapticNot = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.PresynapticNot))),
                    RegionId = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.RegionId))),
                    RegionIdNot = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.RegionIdNot))),
                    RelativeValues = queryStrings.GetNullableEnumValue<RelativeValues>(nqType.GetQueryKey(nameof(NeuronQuery.RelativeValues))),
                    PageSize = queryStrings.GetNullableIntValue(nqType.GetQueryKey(nameof(NeuronQuery.PageSize))),
                    Page = queryStrings.GetNullableIntValue(nqType.GetQueryKey(nameof(NeuronQuery.Page))),
                    NeuronActiveValues = queryStrings.GetNullableEnumValue<ActiveValues>(nqType.GetQueryKey(nameof(NeuronQuery.NeuronActiveValues))),
                    TerminalActiveValues = queryStrings.GetNullableEnumValue<ActiveValues>(nqType.GetQueryKey(nameof(NeuronQuery.TerminalActiveValues))),
                    SortBy = queryStrings.GetNullableEnumValue<SortByValue>(nqType.GetQueryKey(nameof(NeuronQuery.SortBy))),
                    SortOrder = queryStrings.GetNullableEnumValue<SortOrderValue>(nqType.GetQueryKey(nameof(NeuronQuery.SortOrder))),
                    ExternalReferenceUrl = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.ExternalReferenceUrl))),
                    ExternalReferenceUrlContains = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.ExternalReferenceUrlContains)))
                };
            }
            return result != null;
        }
    }
}
