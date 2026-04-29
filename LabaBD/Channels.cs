

namespace LabaBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 

    public partial class Channels
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Channels()
        {
            this.Results = new HashSet<Results>();
        }

        [Key] 
        public int Id_канала_размещенения { get; set; }

        public string Название { get; set; }
        public string Тип { get; set; }
        public Nullable<decimal> Стоимость_размещения { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Results> Results { get; set; }
    }
}