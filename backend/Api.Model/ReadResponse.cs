using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Model
{
    public class ReadResponse<TApiModel>
    {
        public ReadResponse() { }
        public ReadResponse(List<TApiModel> data, int total)
        {
            Records = data;
            Total = total;
        }

        public int Total { get; set; }
        public List<TApiModel> Records { get; set; }
    }
}
