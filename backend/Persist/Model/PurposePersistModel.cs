using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persist.Model
{
    [Table("Purpose")]
    public class PurposePersistModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }

        public bool Disabled { get; set; }
        public DateTime? DisableDate { get; set; }
    }
}
