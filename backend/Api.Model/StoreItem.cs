using System;

namespace Api.Model
{
    public class StoreItem
    {
        public long Id { get; set; }
        public long StorageId { get; set; }
        public long PurposeId { get; set; }
        public double Quantity { get; set; }
    }
}
