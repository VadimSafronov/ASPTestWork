using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SafronovText.Models
{
    
        [DisplayName("Работник")]
        public class PersonModel
        {
            //[ReadOnly(true)]
            [DisplayName("Идентификатор")]
            public int PersonID { get; set; }
            [Required]
            [DisplayName("Имя")]
            public string Name { get; set; }
            [Required]
            [DisplayName("Фамилия")]
            public string Surname { get; set; }
            [Required]
            [DisplayName("Отчество")]
            public string Patronymic { get; set; }
            [DisplayName("Дата приема на работу")]
            public DateTime StartDate { get; set; }
            [DisplayName("Должность")]
            public PositionModel Position { get; set; }
            [DisplayName("Компания")]
            public CompanyModel Company { get; set; }

        }
}