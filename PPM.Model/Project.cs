using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM.Model 
{

    public class Project
    {
        public int ProjectId { get; set;}
        public string? ProjectName { get; set;}
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set;}
        public List<Employee> EmployeeAddList { get; set;} = new List<Employee>(); 

        


        public override string ToString()
        {
            return string.Format($"\nProject id : {ProjectId} \nProject name : {ProjectName} \nStart date : {StartDate} \nEnd date : {EndDate}");
        }

    }

    

    
}