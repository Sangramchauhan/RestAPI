using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestFulAPI.Models
{
    public class EmployeeModel
    {
        public string EmpId { get; set; }
        public string EmpName { get; set; }
    }

    public class EmployeeList
    {
        public Responses status { get; set; }
        public List<EmployeeModel> EmpList { get; set; }

        public EmployeeList()
        {
            EmpList = new List<EmployeeModel>();
        }
    }

    public class Responses
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string time { get; set; }
    }
}
