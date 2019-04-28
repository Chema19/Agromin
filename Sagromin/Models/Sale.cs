//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sagromin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            this.EntryExitProduct = new HashSet<EntryExitProduct>();
            this.SaleDetail = new HashSet<SaleDetail>();
            this.SalePay = new HashSet<SalePay>();
        }
    
        public int SaleId { get; set; }
        public Nullable<decimal> General_Price { get; set; }
        public Nullable<System.DateTime> Creation_Date { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Status { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> LocalId { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
        public string CodeVoucher { get; set; }
        public string StatusPayment { get; set; }
        public string StatusDelivery { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntryExitProduct> EntryExitProduct { get; set; }
        public virtual Local Local { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePay> SalePay { get; set; }
    }
}
