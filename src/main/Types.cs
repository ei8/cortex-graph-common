/*
    This file is part of the d# project.
    Copyright (c) 2016-2018 ei8
    Authors: ei8
     This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    EI8. EI8 DISCLAIMS THE WARRANTY OF NON INFRINGEMENT OF THIRD PARTY RIGHTS
     This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    https://github.com/ei8/cortex-diary/blob/master/LICENSE
     The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
     You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the d# software without
    disclosing the source code of your own applications.
     For more information, please contact ei8 at this address: 
     support@ei8.works
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ei8.Cortex.Graph.Common
{
    public enum RelativeType
    {
        NotSet,
        Postsynaptic,
        Presynaptic
    }

    [Flags]
    public enum RelativeValues
    {
        None = 0x0,
        Postsynaptic = 0x1,
        Presynaptic = 0x2,
        All = Postsynaptic | Presynaptic
    }

    [Flags]
    public enum ActiveValues
    {
        None = 0x0,
        Active = 0x1,
        Inactive = 0x2,
        All = Active | Inactive
    }

    // EnumMember values map to fields of ei8.Cortex.Graph.Domain.Model.NeuronResult
    public enum SortByValue
    {
        [EnumMember(Value = "Neuron.Tag")]
        NeuronTag,
        [EnumMember(Value = "Neuron.CreationTimestamp")]
        NeuronCreationTimestamp,
        [EnumMember(Value = "NeuronCreationAuthorTag")]
        NeuronCreationAuthorTag,
        [EnumMember(Value = "Neuron.LastModificationTimestamp")]
        NeuronLastModificationTimestamp,
        [EnumMember(Value = "NeuronLastModificationAuthorTag")]
        NeuronLastModificationAuthorTag,
        [EnumMember(Value = "Neuron.UnifiedLastModificationTimestamp")]
        NeuronUnifiedLastModificationTimestamp,
        [EnumMember(Value = "NeuronUnifiedLastModificationAuthorTag")]
        NeuronUnifiedLastModificationAuthorTag,
        [EnumMember(Value = "Neuron.Active")]
        NeuronActive,
        [EnumMember(Value = "NeuronRegionTag")]
        NeuronRegionTag,
        [EnumMember(Value = "Terminal.Effect")]
        TerminalEffect,
        [EnumMember(Value = "Terminal.Strength")]
        TerminalStrength,
        [EnumMember(Value = "Terminal.CreationTimestamp")]
        TerminalCreationTimestamp,
        [EnumMember(Value = "TerminalCreationAuthorTag")]
        TerminalCreationAuthorTag,
        [EnumMember(Value = "Terminal.LastModificationTimestamp")]
        TerminalLastModificationTimestamp,
        [EnumMember(Value = "TerminalLastModificationAuthorTag")] 
        TerminalLastModificationAuthorTag,
        [EnumMember(Value = "Terminal.Active")]
        TerminalActive,
        [EnumMember(Value = "Neuron.ExternalReferenceUrl")]
        NeuronExternalReferenceUrl,
        [EnumMember(Value = "Terminal.ExternalReferenceUrl")]
        TerminalExternalReferenceUrl
    }

    public enum SortOrderValue
    {
        [EnumMember(Value = "ASC")]
        Ascending,
        [EnumMember(Value = "DESC")]
        Descending
    }
}
