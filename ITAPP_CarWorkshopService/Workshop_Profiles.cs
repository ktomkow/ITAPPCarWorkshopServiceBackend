//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITAPP_CarWorkshopService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Workshop_Profiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Workshop_Profiles()
        {
            this.Car_Services = new HashSet<Car_Services>();
            this.Workshop_Brand_Connections = new HashSet<Workshop_Brand_Connections>();
            this.Workshop_Comments = new HashSet<Workshop_Comments>();
            this.Workshop_Employees = new HashSet<Workshop_Employees>();
            this.Workshop_Opening_Hours = new HashSet<Workshop_Opening_Hours>();
        }
    
        public int Workshop_ID { get; set; }
        public string Workshop_NIP { get; set; }
        public string Workshop_address_city { get; set; }
        public string Workshop_address_streer { get; set; }
        public string Workshop_address_zip_code { get; set; }
        public string Workshop_description { get; set; }
        public string Workshop_email_address { get; set; }
        public string Workshop_phone_number { get; set; }
        public string Workshop_URL { get; set; }
        public string Workshop_logo_URL { get; set; }
        public Nullable<double> Workshop_average_rating { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car_Services> Car_Services { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workshop_Brand_Connections> Workshop_Brand_Connections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workshop_Comments> Workshop_Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workshop_Employees> Workshop_Employees { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workshop_Opening_Hours> Workshop_Opening_Hours { get; set; }
    }
}
