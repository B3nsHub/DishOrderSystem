//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DishOrderSystemDAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DishType
    {
        public DishType()
        {
            this.Dish_DishType = new HashSet<Dish_DishType>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Dish_DishType> Dish_DishType { get; set; }
    }
}