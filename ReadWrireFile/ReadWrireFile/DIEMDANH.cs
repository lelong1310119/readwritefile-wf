//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReadWrireFile
{
    using System;
    using System.Collections.Generic;
    
    public partial class DIEMDANH
    {
        public string MaLichHoc { get; set; }
        public string MaHocSinh { get; set; }
        public string DiemDanh1 { get; set; }
    
        public virtual HOCSINH HOCSINH { get; set; }
        public virtual LICHHOC LICHHOC { get; set; }
    }
}
