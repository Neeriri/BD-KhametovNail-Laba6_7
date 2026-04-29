

namespace LabaBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Results
    {
        [Key]
        public int id_результата { get; set; }
        public Nullable<int> id_кампании { get; set; }
        public Nullable<int> id_канала { get; set; }
        public Nullable<int> показы { get; set; }
        public Nullable<int> клики { get; set; }
        public Nullable<decimal> конверсия__ { get; set; }
        public Nullable<System.DateTime> дата_отчёта { get; set; }
    
        public virtual Campaigns Campaigns { get; set; }
        public virtual Channels Channels { get; set; }
    }
}
