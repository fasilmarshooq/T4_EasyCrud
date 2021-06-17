using Bridge.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Data
{
   
    public class Department : AbstractEntity
    {

        public string Name { get; set; }

    }

    public class Student : AbstractEntity
    {
  
        public string Name { get; set; }

        [ForeignKey("Department")]
        public long DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual StudentDetail StudentDetail { get; set; }


    }

    public class StudentDetail : AbstractEntity
    {
        [ForeignKey("Student")]
        public new long Id { get; set; }
        public DateTime Admissiondate { get; set; }
        public virtual Student Student { get; set; }
    }


}
