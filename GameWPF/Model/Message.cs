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
    
    public partial class Message
    {
        public System.Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsNew { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}