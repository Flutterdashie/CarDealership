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
    
    public partial class Purchase
    {
        public int PurchaseID { get; set; }
        public int CarID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string PurchaseState { get; set; }
        public string Zipcode { get; set; }
        public decimal Price { get; set; }
        public string PurchaseType { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public string SellerID { get; set; }
    
        public virtual Car Car { get; set; }
    }
}