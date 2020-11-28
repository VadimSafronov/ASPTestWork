using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
namespace SafronovText.Models
{
    [DisplayName("Должность")]
    public class PositionModel
    {
        public int PositionID { get; set; }
        [DisplayName("Должность")]
        public string Position { get; set; }


        
    }
}