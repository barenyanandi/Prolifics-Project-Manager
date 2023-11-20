using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PPM.Model;

namespace PPM.Domain
{
    public class ProjectRepo : IEntityOperation<Project>
    {
        string connStr = "Server = RHJ-9F-D206\SQLEXPRESS; Database = PPM; Integrated Security = true";

        List<Project> pdtList = new();

        public void AddOperation(Project project)
        {
            //storing the project details in the list
            projectList.Add(project);
        }


        public void AddEmployeeToProject(int projectId, Employee employee)
        {
            var projobj = projectList.SingleOrDefault(p => p.ProjectId == projectId);
            //adding the employee id to the list linked to project details
            projobj!.EmployeeAddList.Add(employee);
        }



        public List<Project> ListAll()
        {
            return projectList;
        }


        public Project ListById(int id)
        {
            var details = projectList.Find(e => e.ProjectId == id);
            return details!;
        }





        public void DeleteOperation(int id)
        {
            //details of the project to be removed
            var projectToRemove = projectList.Find(e => e.ProjectId == id);

            projectList.Remove(projectToRemove!);
        }


    }
}
