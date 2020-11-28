using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persist.Model
{
    [Table("Movement")]
    public class MovementPersistModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public long SourceStoreItemId { get; set; }
        public long DestinationStoreItemId { get; set; }
        public double Quantity { get; set; }
        public string Comment { get; set; }
        public DateTime MoveDate { get; set; }

        public virtual StoreItemPersistModel SourceStoreItem { get; set; }
        public virtual StoreItemPersistModel DestinationStoreItem { get; set; }
    }
}
