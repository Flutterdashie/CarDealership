//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarDealership.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            this.Purchases = new HashSet<Purchase>();
        }
    
        public int CarID { get; set; }
        public string VIN { get; set; }
        public string BodyStyle { get; set; }
        public string Transmission { get; set; }
        public string Interior { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalePrice { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public int CarYear { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public string CarDescription { get; set; }
        public bool IsNew { get; set; }
        public bool IsFeatured { get; set; }
        public string ImgExtension { get; set; }
    
        public virtual Make Make { get; set; }
        public virtual Model Model { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
