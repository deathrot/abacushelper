using System;

namespace Logic.ViewModels
{
    public class Entity
    {
        public string id {get; set;}

        public DateTime modified_on {get; set;}
        
        public int sort {get; set;}

        public string name {get; set;}

        public string desc {get; set;}
    }
}