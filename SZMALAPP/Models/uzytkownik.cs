//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SZMALAPP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class uzytkownik
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> dataUrodzenia { get; set; }
        public string telefon { get; set; }
        public string rola { get; set; }
        public string haslo { get; set; }
        public Nullable<decimal> liczba_punktow { get; set; }
        public Nullable<int> fk_id_instytucji { get; set; }

        public override string ToString()
        {
            return login;
        }
    }
}
