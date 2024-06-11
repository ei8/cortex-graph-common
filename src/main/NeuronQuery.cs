using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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

        [QueryKey("posterurl")]
        public IEnumerable<string> PostsynapticExternalReferenceUrl { get; set; }

        [QueryKey]
        public int? Depth { get; set; }

        [QueryKey("direction")]
        public DirectionValues? DirectionValues { get; set; }

        [QueryKey("trdeppost")]
        public IEnumerable<DepthIdsPair> TraversalDepthPostsynaptic { get; set; }

        public override string ToString()
        {
            var queryStringBuilder = new StringBuilder();

            var nqType = typeof(NeuronQuery);

            this.Id.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.Id)), queryStringBuilder);
            this.IdNot.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.IdNot)), queryStringBuilder);
            this.TagContains.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.TagContains)), queryStringBuilder);
            this.TagContainsNot.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.TagContainsNot)), queryStringBuilder);

            this.TagContainsIgnoreWhitespace.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.TagContainsIgnoreWhitespace)), 
                v => v.ToString(),
                queryStringBuilder
                );

            this.Presynaptic.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.Presynaptic)), queryStringBuilder);
            this.PresynapticNot.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.PresynapticNot)), queryStringBuilder);
            this.Postsynaptic.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.Postsynaptic)), queryStringBuilder);
            this.PostsynapticNot.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.PostsynapticNot)), queryStringBuilder);
            this.RegionId.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.RegionId)), queryStringBuilder, true);
            this.RegionIdNot.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.RegionIdNot)), queryStringBuilder, true);
            this.ExternalReferenceUrl.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.ExternalReferenceUrl)), queryStringBuilder);
            this.ExternalReferenceUrlContains.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.ExternalReferenceUrlContains)), queryStringBuilder);
            this.PostsynapticExternalReferenceUrl.AppendQuery(nqType.GetQueryKey(nameof(NeuronQuery.PostsynapticExternalReferenceUrl)), queryStringBuilder);

            
            this.RelativeValues.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.RelativeValues)),
                v => ((int)v).ToString(),
                queryStringBuilder
                );

            this.PageSize.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.PageSize)),
                v => v.ToString(),
                queryStringBuilder
                );

            this.Page.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.Page)),
                v => v.ToString(),
                queryStringBuilder
                );

            this.NeuronActiveValues.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.NeuronActiveValues)),
                v => ((int)v).ToString(),
                queryStringBuilder
                );

            this.TerminalActiveValues.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.TerminalActiveValues)),
                v => ((int)v).ToString(),
                queryStringBuilder
                );

            this.SortOrder.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.SortOrder)),
                v => ((int)v).ToString(),
                queryStringBuilder
                );

            this.SortBy.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.SortBy)),
                v => ((int)v).ToString(),
                queryStringBuilder
                );

            this.Depth.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.Depth)),
                v => v.ToString(),
                queryStringBuilder
                );

            this.DirectionValues.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.DirectionValues)),
                v => ((int)v).ToString(),
                queryStringBuilder
                );

            this.TraversalDepthPostsynaptic.AppendQuery(
                nqType.GetQueryKey(nameof(NeuronQuery.TraversalDepthPostsynaptic)),
                queryStringBuilder,
                fieldSelector: t => JsonConvert.SerializeObject(t)
                );

            if (queryStringBuilder.Length > 0)
                queryStringBuilder.Insert(0, '?');

            return queryStringBuilder.ToString();
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
                    ExternalReferenceUrlContains = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.ExternalReferenceUrlContains))),
                    PostsynapticExternalReferenceUrl = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.PostsynapticExternalReferenceUrl))),
                    Depth = queryStrings.GetNullableIntValue(nqType.GetQueryKey(nameof(NeuronQuery.Depth))),
                    DirectionValues = queryStrings.GetNullableEnumValue<DirectionValues>(nqType.GetQueryKey(nameof(NeuronQuery.DirectionValues))),
                    TraversalDepthPostsynaptic = queryStrings.GetQueryArrayOrDefault(nqType.GetQueryKey(nameof(NeuronQuery.TraversalDepthPostsynaptic)), new Func<string, DepthIdsPair>(s => JsonConvert.DeserializeObject<DepthIdsPair>(s)))
                };
            }
            return result != null;
        }
    }
}
