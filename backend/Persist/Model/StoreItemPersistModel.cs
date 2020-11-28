using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persist.Model
{
    [Table("StoreItem")]
    public class StoreItemPersistModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public long StorageId { get; set; }
        public long PurposeId { get; set; }
        public double Quantity { get; set; }

        public bool Disabled { get; set; }
        public DateTime? DisableDate { get; set; }

        public virtual StoragePersistModel Storage { get; set; }
        public virtual PurposePersistModel Purpose { get; set; }
    }
}
