using System;
using EntityState = Logic.Enums.EntityState;

namespace Logic.ViewModels
{
    public class Entity
    {
        public string id {get; set;}

        public DateTime modified_on {get; set;}

        public EntityState EntityState {get; set;}
        
    }
}