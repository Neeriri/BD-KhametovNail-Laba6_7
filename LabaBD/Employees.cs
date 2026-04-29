

namespace LabaBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class Employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employees()
        {
            this.Tasks = new HashSet<Tasks>();
        }

        [Key]
        public int ID_сотрудника { get; set; }
        public string ФИО { get; set; }
        public string Должность { get; set; }
        public string Email { get; set; }
        public Nullable<decimal> Cтавка_в_час { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
