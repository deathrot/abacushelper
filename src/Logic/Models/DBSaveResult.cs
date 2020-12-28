using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class DBSaveResult
    {

        public int TotalRecordSentToSave { get; set; }

        public int TotalRecordInserted { get; set; }

        public int TotalRecordDeleted { get; set; }

        public int TotalRecordUpdated { get; set; }

        public bool Success { get; set; }

    }
}
