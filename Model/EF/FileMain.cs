//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class FileMain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileMain()
        {
            this.ItemMains = new HashSet<ItemMain>();
        }
    
        public int file_id { get; set; }
        public Nullable<int> file_circular { get; set; }
        public Nullable<int> file_form { get; set; }
        public Nullable<System.DateTime> file_startday { get; set; }
        public Nullable<System.DateTime> file_endday { get; set; }
        public Nullable<System.DateTime> file_datecreate { get; set; }
        public Nullable<int> file_status { get; set; }
        public string file_key { get; set; }
        public string file_img { get; set; }
        public string file_company { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemMain> ItemMains { get; set; }
    }
}
