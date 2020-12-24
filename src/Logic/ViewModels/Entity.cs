using System;
using EntityState = Logic.Enums.EntityState;

namespace Logic.ViewModels
{
    public class Entity
    {
        public string Id {get; set;}

        public DateTime ModifiedOn {get; set;}

        public EntityState EntityState {get; set;}
        
    }
}