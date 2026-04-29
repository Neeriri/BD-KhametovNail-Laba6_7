namespace LabaBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Campaigns
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Campaigns()
        {
            this.Expenses = new HashSet<Expenses>();
            this.Results = new HashSet<Results>();
            this.Tasks = new HashSet<Tasks>();
        }

        [Key] 
        public int id_кампании { get; set; }

        public string название { get; set; }
        public Nullable<int> id_клиента { get; set; }
        public Nullable<decimal> бюджет { get; set; }
        public Nullable<System.DateTime> дата_начала { get; set; }
        public Nullable<System.DateTime> дата_окончания { get; set; }
        public string статус { get; set; }

        [ForeignKey("id_клиента")] 
        public virtual Clients Clients { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expenses> Expenses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Results> Results { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}