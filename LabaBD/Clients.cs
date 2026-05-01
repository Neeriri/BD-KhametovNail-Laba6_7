

namespace LabaBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            this.Campaigns = new HashSet<Campaigns>();
        }

        [Key] 
        public int Id_Клиента { get; set; }
        public string Фио { get; set; }
        public string email { get; set; }
        public string Телефон { get; set; }
        public string адрес { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Campaigns> Campaigns { get; set; }
    }
}