using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace SafronovText.Models
{ 
    public class CompanyModel
    {
        [DisplayName("Идентификатор")]
        public int CompanyID { get; set; }
        [DisplayName("Наименование")]
        public string Title { get; set; }
        [DisplayName("Организационно-правовая форма")]
        public string CompType {get; set; }
        [DisplayName("Размер компании")]
        public int Workers { get; set; }

       
    }
}