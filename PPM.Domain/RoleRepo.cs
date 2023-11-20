using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPM.Model;

namespace PPM.Domain
{

    public class RoleRepo : IEntityOperation<Role>
    {
        public readonly static List<Role> roleList = new();
        readonly EmployeeRepo employeeRepo = new();

        public void AddOperation(Role role)
        {
            //storing role details in list
            roleList.Add(role);
        }




        public List<Role> ListAll()
        {
            return roleList;
        }



        public Role ListById(int id)
        {
            var details = roleList.Find(r => r.RoleId == id);
            return details!;
        }



        public void DeleteOperation(int id)
        {
            //details of the role to be removed
            var roleToRemove = roleList.Find(r => r.RoleId == id);

            //checks if the role we want to delete is associated to any employee, thus removes it from employee
            if(employeeRepo.ListAll().Exists(er => er.RoleId == id))
            {
                var emp = employeeRepo.ListAll().Find(er => er.RoleId == id);
                emp!.RoleId = 0;
            }

            roleList.Remove(roleToRemove!);
        }

       
    }
}