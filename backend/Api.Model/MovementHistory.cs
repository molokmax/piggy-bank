using System;

namespace Api.Model
{
    public class MovementHistory
    {
        public long Id { get; set; }
        public long? SourceStoreItemId { get; set; }
        public long? SourceStorageId { get; set; }
        public string SourceStorageName { get; set; }
        public long? SourcePurposeId { get; set; }
        public string SourcePurposeName { get; set; }
        public long? DestinationStoreItemId { get; set; }
        public long? DestinationStorageId { get; set; }
        public string DestinationStorageName { get; set; }
        public long? DestinationPurposeId { get; set; }
        public string DestinationPurposeName { get; set; }
        public double Quantity { get; set; }
        public string Comment { get; set; }
        public DateTime MoveDate { get; set; }
    }
}
