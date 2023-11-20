using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PPM.Model 
{

    public class Employee
    {

        public int EmployeeId { get; set;}
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? Email { get; set;}
        public string? Mobile { get; set;}
        public string? Address { get; set;}
        public int RoleId { get; set;}

        public int ProjectId {get; set;}


        public override string ToString()
        {
            return string.Format($"Employee id : {EmployeeId} \nEmployee Firstname : {FirstName} \nEmployee Lastname : {LastName} \nEmployee Email : {Email} \nMobile : {Mobile} \nAddress : {Address} \nRole id : {RoleId}\n");
        }
       
    }



    
}