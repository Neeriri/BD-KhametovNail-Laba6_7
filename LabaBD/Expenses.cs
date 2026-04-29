

namespace LabaBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Expenses
    {
        [Key]
        public int id_расхода { get; set; }
        public Nullable<int> id_кампании { get; set; }
        public string статья_расхода { get; set; }
        public Nullable<decimal> сумма { get; set; }
        public Nullable<System.DateTime> дата { get; set; }
    
        public virtual Campaigns Campaigns { get; set; }
    }
}
