using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PPM.Model;

namespace PPM.Domain
{
    public class SaveStateRepo
    {
        public void SaveState()
        {
            ProjectRepo objProjectRepo = new();


            //project along with the employee present in them
            XmlSerializer serializer = new XmlSerializer(typeof(List<Project>));
            using (var writer = new StreamWriter("project.xml"))
            {
                serializer.Serialize(writer, objProjectRepo.ListAll());
            }

            //employee
            serializer = new XmlSerializer(typeof(List<Employee>));
            using (var writer = new StreamWriter("employee.xml"))
            {
                serializer.Serialize(writer, EmployeeRepo.employeeList);
            }

            //role
            serializer = new XmlSerializer(typeof(List<Role>));
            using (var writer = new StreamWriter("role.xml"))
            {
                serializer.Serialize(writer, RoleRepo.roleList);
            }

            
        }
    }
}
