using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PPM.Model 
{

    public class Role
    {

        public int RoleId { get; set;}
        public string? RoleName { get; set;}
    
        public override string ToString()
        {
            return string.Format($"Role Id : {RoleId} \nRole Name : {RoleName} \n");
        }
    
    }

    
}