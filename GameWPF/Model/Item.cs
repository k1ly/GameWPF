//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameWPF.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public int Stat { get; set; }
        public string Image { get; set; }
    
        public virtual CharacterClass ItemClass { get; set; }
        public virtual ItemType ItemType { get; set; }
    }
}
