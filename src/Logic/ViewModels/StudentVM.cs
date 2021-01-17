using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.ViewModels
{
    public class StudentVM : Entity
    {
        public string StudentName { get; set; }
        public string StudentDisplayName { get; set; }
        public string StudentEmail {get; set;}
        public decimal? StartingLevelId {get; set;}
        public decimal? StartingSubLevelId {get; set;}
        public decimal? CurrentLevelId {get; set;}
        public decimal? CurrentSubLevelId {get; set;}
        public string Password {get; set;}
        public DateTime? LastLoginOn{get; set;}
        public DateTime? LastLogOut{get; set;}
        public bool IsLockedOut{get; set;}
    }
}
