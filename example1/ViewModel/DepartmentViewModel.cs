using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication8.Context;

namespace WebApplication8.ViewModel
{
    public class DepartmentViewModel
    {
        public department NewDepartment { get; set; }
        public List<department> Departments { get; set; }
    }
}