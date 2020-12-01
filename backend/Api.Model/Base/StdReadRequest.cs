using System;
using System.Collections.Generic;
using System.Text;
using Api.Model.Base;

namespace Api.Model
{
    public class StdReadRequest
    {
        public List<Sorting> Order { get; set; }
        public List<Filter> Where { get; set; }
    }
}
