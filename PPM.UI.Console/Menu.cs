using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Linq;
using PPM.Domain;
using PPM.Model;

namespace PPM.UI.Console
{
    public class Menu
    {
        readonly ProjectRepo objProjectRepo = new();
        readonly EmployeeRepo objEmployeeRepo = new();
        readonly RoleRepo objRoleRepo = new();
        readonly SaveStateRepo objSaveRepo = new();

        //-----------------------------    MAIN MENU     -----------------------------------------


        public int ModuleChoices()
        {
            System.Console.ForegroundColor = ConsoleColor.DarkGreen;

            System.Console.WriteLine("\n\n\n");
            System.Console.WriteLine("-----------------------------------------              ");
            System.Console.WriteLine("  WELCOME TO PROLIFICS PROJECT MANAGER               ");
            System.Console.WriteLine("-----------------------------------------              ");

            int moduleChoice;

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("--------------- ");
            System.Console.WriteLine("  MAIN  MENU :    ");
            System.Console.WriteLine("--------------- ");
            System.Console.WriteLine("\n1. Project Module");
            System.Console.WriteLine("\n2. Employee Module");
            System.Console.WriteLine("\n3. Role Module");
            System.Console.WriteLine("\n4. Save");
            System.Console.WriteLine("\n5. Quit");
            System.Console.WriteLine();
            System.Console.WriteLine();

            System.Console.ResetColor();

            System.Console.WriteLine("Choose a  module :");
            moduleChoice = int.Parse(System.Console.ReadLine()!);
            System.Console.WriteLine();

            if (moduleChoice == 1)
            {
                System.Console.WriteLine("\n\n");
                System.Console.ForegroundColor = ConsoleColor.DarkGreen;
                System.Console.WriteLine("----   PROJECT MODULE   ----");
                System.Console.ResetColor();
            }
            if (moduleChoice == 2)
            {
                System.Console.WriteLine("\n\n");
                System.Console.ForegroundColor = ConsoleColor.DarkGreen;
                System.Console.WriteLine("----   EMPLOYEE MODULE   ----");
                System.Console.ResetColor();
            }
            if (moduleChoice == 3)
            {
                System.Console.WriteLine("\n\n");
                System.Console.ForegroundColor = ConsoleColor.DarkGreen;
                System.Console.WriteLine("----   ROLE MODULE   ----");
                System.Console.ResetColor();
            }


            return moduleChoice;
        }

        public int CaseChoices()
        {
            System.Console.ForegroundColor = ConsoleColor.DarkGreen;

            int choice;

            System.Console.WriteLine();
            System.Console.WriteLine("Module Options  :");
            System.Console.WriteLine("\n1. Add ");
            System.Console.WriteLine("\n2. List all");
            System.Console.WriteLine("\n3. List by ID");
            System.Console.WriteLine("\n4. Delete");
            System.Console.WriteLine("\n5. Return to Main Menu");
            System.Console.WriteLine();
            System.Console.WriteLine();

            System.Console.ResetColor();

            System.Console.WriteLine("Enter your choice for this module :");
            choice = int.Parse(System.Console.ReadLine()!);
            System.Console.WriteLine();

            return choice;
        }

        //-----------------------------------------------------------------------------------------------






        //-----------------------------    PROJECT MODULE     -----------------------------------------




        // ADDING  PROJECT DETAILS
        public void AddProjectDetails()
        {
            Project objProject = new();

            System.Console.WriteLine();

            ProjectRepo projectRepo = new();

            //provide the details of the project

            //Project id
            System.Console.WriteLine("Enter project id : ");
            int projectId = int.Parse(System.Console.ReadLine()!);

            //check if the project already exists
            if (projectRepo.ListAll().Exists(proj => proj.ProjectId == projectId))
            {
                System.Console.WriteLine("Id already exists ");
                return;
            }
            else
            {
                objProject.ProjectId = projectId;
            }

            //Project name
            System.Console.WriteLine("Enter project name : ");
            string projectName = System.Console.ReadLine()!;
            objProject.ProjectName = projectName;

            //Project Startdate and Enddate
            System.Console.WriteLine("Enter start date of project : ");
            DateTime startDate = Convert.ToDateTime(System.Console.ReadLine()!);

            System.Console.WriteLine("Enter end date of project : ");
            DateTime endDate = Convert.ToDateTime(System.Console.ReadLine()!);

            //check that the end date is not prior to the start date of the project
            if (startDate > endDate)
            {
                System.Console.WriteLine("Please provide proper start and end date.");
                return;
            }
            else
            {
                objProject.StartDate = startDate;
                objProject.EndDate = endDate;
            }

            //passing the deatils of project to the AddProject method
            projectRepo.AddOperation(objProject);

            //asking if we want to add employee to project
            string addEmp = "yes";
            while (addEmp == "yes")
            {
                System.Console.WriteLine(
                    "Do you want to add Employee and their Role to the project?(yes/no)"
                );
                addEmp = System.Console.ReadLine()!;

                if (addEmp == "yes")
                {
                    MenuAddEmployeeToProject(projectId);
                }
                else
                {
                    break;
                }
            }
        }

        //adding employee to project if mentione

        //   ADD EMPLOYEE TO A PROJECT
        public void MenuAddEmployeeToProject(int projectId)
        {
            Employee objEmployee = new();

            //Employee id to be added to project
            System.Console.WriteLine("Enter the employee id to be added to project :");
            int employeeId = int.Parse(System.Console.ReadLine()!);
            var employee = EmployeeRepo.employeeList.Find(e => e.EmployeeId == employeeId);
            employee!.ProjectId = projectId;
            System.Console.WriteLine();

            //check if the employee is present in the employee list
            if (objEmployeeRepo.ListAll().Exists(empid => empid.EmployeeId == employeeId))
            {
                //project id in the employee class
                //objEmployee.ProjectId = projectId;

                objProjectRepo.AddEmployeeToProject(projectId, employee!);
                

                System.Console.ForegroundColor = ConsoleColor.DarkBlue;

                System.Console.WriteLine("Employee added to the project");
                System.Console.WriteLine();

                System.Console.ResetColor();
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.DarkBlue;
                System.Console.WriteLine("Invalid Employee id");
                System.Console.ResetColor();

                return;
            }
        }

        //  VIEWING  ALL  PROJECTS
        public void ViewProject()
        {
            System.Console.ForegroundColor = ConsoleColor.DarkBlue;

            if (objProjectRepo.ListAll().Count != 0)
            {
                System.Console.WriteLine("Project details ----------------------");

                foreach (Project p in objProjectRepo.ListAll())
                {
                    System.Console.WriteLine(p.ToString());
                    System.Console.WriteLine();
                }
                System.Console.WriteLine();
            }
            else
            {
                System.Console.WriteLine("No projects exist.");
            }

            System.Console.ResetColor();
        }

        //  VIEW ENTIRE PROJECT DETAILS ACC. TO ID
        public void ViewProjectById()
        {
            System.Console.WriteLine("Enter the project id :");
            int projectId = int.Parse(System.Console.ReadLine()!);
            System.Console.WriteLine();

            //storing the project details
            var projectDetails = objProjectRepo
                .ListAll()
                .SingleOrDefault(p => p.ProjectId == projectId);

            System.Console.ForegroundColor = ConsoleColor.DarkBlue;

            try
            {
                //check if the project exists
                if (objProjectRepo.ListAll().Exists(p => p.ProjectId == projectId))
                {
                    //printing the project details
                    System.Console.WriteLine("Project details ------------------------------\n");
                    System.Console.WriteLine(
                        $"Project Id : {projectDetails!.ProjectId} \nProject Name : {projectDetails.ProjectName} \nStart Date : {projectDetails.StartDate} \nEnd Date : {projectDetails.EndDate}"
                    );
                    System.Console.WriteLine();

                    //checking if the project has any employee
                    if (projectDetails.EmployeeAddList.Count == 0)
                    {
                        System.Console.WriteLine("No employee added to the project.");
                    }
                }
                else
                {
                    System.Console.WriteLine("Sorry! Project does not exist");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Exception occured :{ex.Message} ");
            }

            System.Console.ResetColor();

            //displaying the employee and their roles for this particular project
            foreach (var i in projectDetails!.EmployeeAddList)
            {
                System.Console.ForegroundColor = ConsoleColor.DarkBlue;

                //storing employee details corresponding to a particular project
                // var employeeDetails = objEmployeeRepo.ListAll().SingleOrDefault(p => p.ProjectId == projectId);

                //printing the employee details for this project
                System.Console.WriteLine(
                    "Employee details ----------------------------------------------------\n"
                );
                System.Console.WriteLine(
                    $"Employee id : {i!.EmployeeId} \nEmployee Firstname : {i.FirstName} \nEmployee Lastname : {i.LastName} "
                    + $"\nEmployee Email : {i.Email} \nMobile : {i.Mobile} " +
                    $"\nAddress : {i.Address} \nRole id : {i.RoleId}\n"
                );

                //printing the role details of the employee
                System.Console.WriteLine(
                    "Role details ----------------------------------------------------\n"
                );
                var roleDetails = objRoleRepo
                    .ListAll()
                    .SingleOrDefault(r => r.RoleId == i.RoleId);
                System.Console.WriteLine(
                    $"Role Id : {roleDetails!.RoleId} \nRole Name : {roleDetails.RoleName} \n"
                );

                System.Console.ResetColor();
            }
        }

        // DELETE PROJECT
        public void MenuDeleteProject()
        {
            System.Console.WriteLine("Enter the project id to be deleted : ");
            int projectId = int.Parse(System.Console.ReadLine()!);

            if (objProjectRepo.ListAll().Exists(p => p.ProjectId == projectId))
            {
                objProjectRepo.DeleteOperation(projectId);
                System.Console.WriteLine($"Project {projectId} is deleted.");
            }
            else
            {
                System.Console.WriteLine("Project does not exist.");
            }
        }

        //-----------------------------------------------------------------------------------------------








        //-----------------------------    EMPLOYEE MODULE     -----------------------------------------





        // ADDING EMPLOYEE DETAILS
        public void AddEmployeeDetails()
        {
            Employee objEmployee = new Employee();

            System.Console.WriteLine();

            //provide the details of the employee

            //Employee id
            System.Console.WriteLine("Enter employee id : ");
            int employeeId = int.Parse(System.Console.ReadLine()!);

            //check if the project already exists
            if (objEmployeeRepo.ListAll().Exists(emp => emp.EmployeeId == employeeId))
            {
                System.Console.WriteLine("Id already exists ");
                return;
            }
            else
            {
                objEmployee.EmployeeId = employeeId;
            }

            //Employee name
            System.Console.WriteLine("Enter employee firtsname : ");
            string firstName = System.Console.ReadLine()!;

            //check that the name is non-numeric
            //Regular expression pattern to match non-numeric strings
            string pattern = @"^[^\d]+$";

            if (!Regex.IsMatch(firstName, pattern))
            {
                System.Console.WriteLine(
                    "Invalid name! Employee First Name should not contain numeric characters"
                );
                return;
            }
            else
            {
                objEmployee.FirstName = firstName;
            }

            System.Console.WriteLine("Enter employee lastsname : ");
            string lastName = System.Console.ReadLine()!;

            //check that the name is non-numeric
            if (!Regex.IsMatch(lastName, pattern))
            {
                System.Console.WriteLine(
                    "Invalid name! Employee Last Name should not contain numeric characters"
                );
                return;
            }
            else
            {
                objEmployee.LastName = lastName;
            }

            //Employee email
            System.Console.WriteLine("Enter employee email : ");
            string email = System.Console.ReadLine()!;
            string emailValid =
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            //check if the email is right
            if (!Regex.IsMatch(email, emailValid))
            {
                System.Console.WriteLine("Please enter proper email address.");
                return;
            }
            else
            {
                objEmployee.Email = email;
            }

            //Employee mobile number
            System.Console.WriteLine("Enter employee mobile no. : ");
            string mobile = System.Console.ReadLine()!;

            //check if the mobile number is 10 digit long
            if (!Regex.Match(mobile, @"^[0-9]{10}$").Success)
            {
                System.Console.WriteLine("Please enter proper mobile number.");
                return;
            }
            else
            {
                objEmployee.Mobile = mobile;
            }

            //Employee address
            System.Console.WriteLine("Enter employee address : ");
            string address = System.Console.ReadLine()!;
            objEmployee.Address = address;

            //Employee Role id
            System.Console.WriteLine("Enter employee role id : ");
            int roleId = int.Parse(System.Console.ReadLine()!);

            //check if the role already exists
            if (!objRoleRepo.ListAll().Exists(role => role.RoleId == roleId))
            {
                System.Console.WriteLine("Id does not exists. Please check the roles available!");
                return;
            }
            else
            {
                objEmployee.RoleId = roleId;
            }

            //passing the deatils of project to the AddEmployee method
            objEmployeeRepo.AddOperation(objEmployee);
        }

        //  VIEWING EMPLOYEE DETAILS
        public void ViewEmployee()
        {
            System.Console.ForegroundColor = ConsoleColor.DarkBlue;

            if (objEmployeeRepo.ListAll().Count != 0)
            {
                System.Console.WriteLine("The employee details--------------------- ");

                foreach (Employee obj in objEmployeeRepo.ListAll())
                {
                    System.Console.WriteLine(obj.ToString());
                    System.Console.WriteLine();
                }
            }
            else
            {
                System.Console.WriteLine("No Employee exists.");
                return;
            }

            System.Console.ResetColor();
        }

        //  VIEW EMPLOYEE BY ID
        public void ViewEmployeeById()
        {
            System.Console.WriteLine("Enter the employee id :");
            int employeeId = int.Parse(System.Console.ReadLine()!);
            System.Console.WriteLine();

            //storing the project details
            var employeeDetails = objEmployeeRepo.ListAll().Find(e => e.EmployeeId == employeeId);

            System.Console.ForegroundColor = ConsoleColor.DarkBlue;

            //check if the employee exists
            if (objEmployeeRepo.ListAll().Exists(e => e.EmployeeId == employeeId))
            {
                //printing the project details
                System.Console.WriteLine("Employee details ------------------------------\n");
                System.Console.WriteLine($"nEmployee Id : {employeeDetails!.EmployeeId} \nEmployee First Name : {employeeDetails.FirstName} \nEmployee Last Name : {employeeDetails.LastName} \nEmail : {employeeDetails.Email} \nMobile : {employeeDetails.Mobile} \nAddress : {employeeDetails.Address} \nRoleId : {employeeDetails.RoleId}");
                System.Console.WriteLine();
            }
            else
            {
                System.Console.WriteLine("Sorry! Employee does not exist");
            }

            System.Console.ResetColor();
        }

        // DELETE EMPLOYEE
        public void MenuDeleteEmployee()
        {
            System.Console.WriteLine("Enter the employee id to be deleted : ");
            int employeeId = int.Parse(System.Console.ReadLine()!);

            if (objEmployeeRepo.ListAll().Exists(p => p.EmployeeId == employeeId))
            {
                objEmployeeRepo.DeleteOperation(employeeId);
                System.Console.ForegroundColor = ConsoleColor.DarkBlue;
                System.Console.WriteLine($"Employee {employeeId} is deleted.");
                System.Console.ResetColor();
            }
            else
            {
                System.Console.ForegroundColor = ConsoleColor.DarkBlue;
                System.Console.WriteLine("Employee does not exist.");
                System.Console.ResetColor();
                return;
            }
        }

        //  DELETE EMPLOYEE FROM PROJECT
        public void MenuDeleteEmployeeFromProject()
        {
            System.Console.WriteLine("Enter the project id :");
            int projectId = int.Parse(System.Console.ReadLine()!);
            System.Console.WriteLine();

            System.Console.WriteLine("Enter the employee id :");
            int employeeId = int.Parse(System.Console.ReadLine()!);
            System.Console.WriteLine();

            objEmployeeRepo.DeleteEmployeeFromProject(projectId, employeeId);

            System.Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("Employee removed from the project.\n");
            System.Console.ResetColor();
        }

        //-----------------------------------------------------------------------------------------------









        //-----------------------------   ROLE MODULE     -----------------------------------------





        // ADDING  ROLE DETAILS
        public void RoleDetails()
        {
            Role objRole = new();

            //provide the details of the roles
            //Role id
            System.Console.WriteLine("Enter role id : ");
            int roleId = int.Parse(System.Console.ReadLine()!);

            //check if the role already exists
            if (objRoleRepo.ListAll().Exists(role => role.RoleId == roleId))
            {
                System.Console.WriteLine("Id already exists!");
                return;
            }
            else
            {
                objRole.RoleId = roleId;
            }

            //Role name
            System.Console.WriteLine("Enter role name : ");
            string roleName = System.Console.ReadLine()!;
            objRole.RoleName = roleName;

            //check that the name is non-numeric
            //Regular expression pattern to match non-numeric strings
            string pattern = @"^[^\d]+$";

            if (!Regex.IsMatch(roleName, pattern))
            {
                System.Console.WriteLine(
                    "Invalid name! Role Name should not contain numeric characters"
                );
            }

            //passing the details of role to the AddRole method
            objRoleRepo.AddOperation(objRole);
        }

        //   VIEWING ALL ROLE DETAILS
        public void ViewRole()
        {
            System.Console.ForegroundColor = ConsoleColor.DarkBlue;

            if (objRoleRepo.ListAll().Count != 0)
            {
                System.Console.WriteLine("The role details---------------------------- ");

                foreach (Role obj in objRoleRepo.ListAll())
                {
                    System.Console.WriteLine(obj.ToString());
                }
            }
            else
            {
                System.Console.WriteLine("No Role exists.");
            }

            System.Console.ResetColor();
        }

        //  VIEW ROLE BY ID
        public void ViewRoleById()
        {
            System.Console.WriteLine("Enter the role id :");
            int roleId = int.Parse(System.Console.ReadLine()!);
            System.Console.WriteLine();

            //storing the project details
            var roleDetails = objRoleRepo.ListAll().SingleOrDefault(r => r.RoleId == roleId);

            System.Console.ForegroundColor = ConsoleColor.DarkBlue;

            //check if the role exists
            if (objRoleRepo.ListAll().Exists(role => role.RoleId == roleId))
            {
                //printing the project details
                System.Console.WriteLine("Role details ------------------------------\n");
                System.Console.WriteLine(
                    $"Role Id : {roleDetails!.RoleId} \nRole Name : {roleDetails.RoleName} "
                );
                System.Console.WriteLine();
            }
            else
            {
                System.Console.WriteLine("Sorry! Role does not exist");
            }

            System.Console.ResetColor();
        }

        // DELETE ROLE
        public void MenuDeleteRole()
        {
            System.Console.WriteLine("Enter the role id to be deleted : ");
            int roleId = int.Parse(System.Console.ReadLine()!);

            if (objRoleRepo.ListAll().Exists(r => r.RoleId == roleId))
            {
                objRoleRepo.DeleteOperation(roleId);
                System.Console.WriteLine($"Role {roleId} is deleted.");
            }
            else
            {
                System.Console.WriteLine("Role does not exist.");
            }
        }

        //-----------------------------------------------------------------------------------------------









        //-----------------------------    SAVE  STATE     -----------------------------------------



        public void MenuSaveState()
        {
            objSaveRepo.SaveState();
            System.Console.ForegroundColor = ConsoleColor.DarkBlue;
            System.Console.WriteLine("App state saved sucessfully.");
            System.Console.ResetColor();
        }
    }

    //-------------------------------------------------------------------------------------------
}
