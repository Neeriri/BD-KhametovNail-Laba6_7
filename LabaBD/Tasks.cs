

namespace LabaBD
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Tasks
    {
        [Key]
        public int id_задачи { get; set; }
        public Nullable<int> id_кампании { get; set; }
        public string описание { get; set; }
        public Nullable<int> id_исполнителя { get; set; }
        public Nullable<System.DateTime> срок_выполнения { get; set; }
        public string статус { get; set; }
    
        public virtual Campaigns Campaigns { get; set; }
        public virtual Employees Employees { get; set; }
    }
}
