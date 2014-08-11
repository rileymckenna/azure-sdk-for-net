// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.WindowsAzure.Management.WebSites.Models
{
    /// <summary>
    /// Parameters supplied to the Get Historical Usage Metrics Web Site
    /// operation.
    /// </summary>
    public partial class WebSiteGetHistoricalUsageMetricsParameters
    {
        private System.DateTime? _endTime;
        
        /// <summary>
        /// Optional. The ending time of the metrics to return. If this
        /// parameter is not specified, the current time is used.
        /// </summary>
        public System.DateTime? EndTime
        {
            get { return this._endTime; }
            set { this._endTime = value; }
        }
        
        private bool _includeInstanceBreakdown;
        
        /// <summary>
        /// Optional. Flag which specifies if the metrics for each machine
        /// instance should be included. For sites that run on more than one
        /// machine this could be useful to identify a bad machine.
        /// </summary>
        public bool IncludeInstanceBreakdown
        {
            get { return this._includeInstanceBreakdown; }
            set { this._includeInstanceBreakdown = value; }
        }
        
        private IList<string> _metricNames;
        
        /// <summary>
        /// Optional. Specifies a comma-separated list of the names of the
        /// metrics to return. If the names parameter is not specified, then
        /// all available metrics are returned.
        /// </summary>
        public IList<string> MetricNames
        {
            get { return this._metricNames; }
            set { this._metricNames = value; }
        }
        
        private bool _slotView;
        
        /// <summary>
        /// Optional. Flag which specifies if the metrics returned should
        /// reflect slot swaps. Let's take for example following case: if
        /// production slot has hostname www.contos.com and take traffic for
        /// 12 hours and later is swapped with staging slot. Getting metrics
        /// with SlotView=false will reflect the swap - e.g. there will be a
        /// increase on the staging slot metrics after it goes to
        /// production.If SlotView=true is used it will show the metrics for
        /// the www.contoso.com regardless which slot was serving at the
        /// moment.
        /// </summary>
        public bool SlotView
        {
            get { return this._slotView; }
            set { this._slotView = value; }
        }
        
        private System.DateTime? _startTime;
        
        /// <summary>
        /// Optional. The starting time of the metrics to return. If this
        /// parameter is not specified, the beginning of the current hour is
        /// used.
        /// </summary>
        public System.DateTime? StartTime
        {
            get { return this._startTime; }
            set { this._startTime = value; }
        }
        
        private string _timeGrain;
        
        /// <summary>
        /// Optional. The grain at which the metrics are returned. Supported
        /// values are PT1M (minute), PT1H (hour), P1D (day).
        /// </summary>
        public string TimeGrain
        {
            get { return this._timeGrain; }
            set { this._timeGrain = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the
        /// WebSiteGetHistoricalUsageMetricsParameters class.
        /// </summary>
        public WebSiteGetHistoricalUsageMetricsParameters()
        {
            this.MetricNames = new List<string>();
        }
    }
}
