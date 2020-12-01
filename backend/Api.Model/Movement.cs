using System;

namespace Api.Model
{
    public class Movement
    {
        public long Id { get; set; }
        public long? SourceStorageId { get; set; }
        public long? SourcePurposeId { get; set; }
        public long? DestinationStorageId { get; set; }
        public long? DestinationPurposeId { get; set; }
        public double Quantity { get; set; }
        public string Comment { get; set; }
    }
}
