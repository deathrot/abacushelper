using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class SessionValidResult
    {

        public bool IsValid { get; set; }

        public DateTime? NextLoginTimeout { get; set; }

    }
}
