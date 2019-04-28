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
    
    public partial class ProductBrand
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductBrand()
        {
            this.EntryExitProduct = new HashSet<EntryExitProduct>();
            this.SaleDetail = new HashSet<SaleDetail>();
            this.StockProduct = new HashSet<StockProduct>();
        }
    
        public int ProductBrandId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public string Status { get; set; }
    
        public virtual Brand Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntryExitProduct> EntryExitProduct { get; set; }
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SaleDetail> SaleDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockProduct> StockProduct { get; set; }
    }
}
