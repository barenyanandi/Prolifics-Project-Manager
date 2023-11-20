using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPM.Model;

namespace PPM.Domain
{

    public class EmployeeRepo : IEntityOperation<Employee>
    {
        //Employee list
        public static List<Employee> employeeList = new List<Employee>();
        readonly ProjectRepo projectRepo = new();
        Project objProject = new();



        public void AddOperation(Employee employee)
        {
            //storing employee details into list
            employeeList.Add(employee);
        }


        public List<Employee> ListAll()
        {
            return employeeList;
        }

        public Employee ListById(int id)
        {
            var details = employeeList.Find(e => e.EmployeeId == id);
            return details!;
        }

        public void DeleteOperation(int id)
        {
            //details of the employee to be removed
            var employeeToRemove = employeeList.FirstOrDefault(e => e.EmployeeId == id);


            //checks if the employee we want to delete is associated to any role, thus removes them from project
            if (employeeToRemove?.ProjectId != 0)
            {
                // //details of the project which is associated with the employee
                DeleteEmployeeFromProject(employeeToRemove!.ProjectId, id);
            }


            //deleting employee from employee list
            employeeList.Remove(employeeToRemove!);
        }


        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            var projobj = projectRepo.ListAll().SingleOrDefault(p => p.ProjectId == projectId);
            var employeeobj = EmployeeRepo.employeeList.Find(e => e.EmployeeId ==employeeId);
            
            
            //removing employee from the project
            projobj!.EmployeeAddList.Remove(employeeobj!);
        }


    }


}