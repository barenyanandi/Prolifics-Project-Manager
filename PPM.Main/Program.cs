using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPM.Domain;
using PPM.UI.Console;

namespace PPM.Main
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Menu objMenu = new Menu();

            int choice,
                moduleChoice;

            do
            {
                moduleChoice = objMenu.ModuleChoices();

                switch (moduleChoice)
                {
                    //Project Module
                    case 1:
                        //choose cases in project module
                        choice = objMenu.CaseChoices();

                        switch (choice)
                        {
                            case 1:
                                //adding project details
                                objMenu.AddProjectDetails();
                                break;

                            case 2:
                                //list all project details
                                objMenu.ViewProject();
                                break;

                            case 3:
                                //list project details by id
                                objMenu.ViewProjectById();
                                break;

                            case 4:
                                //delete project
                                objMenu.MenuDeleteProject();
                                break;

                            case 5:
                                break;

                            default:
                                System.Console.WriteLine("Invalid input");
                                break;
                        }
                        break;

                    //Employee Module
                    case 2:
                        //choose cases in employee module
                        choice = objMenu.CaseChoices();

                        switch (choice)
                        {
                            case 1:
                                //adding employee details
                                objMenu.AddEmployeeDetails();
                                break;

                            case 2:
                                //list all employee details
                                objMenu.ViewEmployee();
                                break;

                            case 3:
                                //list employee details by id
                                objMenu.ViewEmployeeById();
                                break;

                            case 4:
                                //delete employee
                                objMenu.MenuDeleteEmployee();
                                break;

                            case 5:
                                break;

                            default:
                                System.Console.WriteLine("Invalid input");
                                break;
                        }
                        break;

                    //Role Module
                    case 3:
                        //choose cases in role module
                        choice = objMenu.CaseChoices();
                        

                        switch (choice)
                        {
                            case 1:
                                //adding role details
                                objMenu.RoleDetails();
                                break;

                            case 2:
                                //list all role details
                                objMenu.ViewRole();
                                break;

                            case 3:
                                //list role details by id
                                objMenu.ViewRoleById();
                                break;

                            case 4:
                                //delete role
                                objMenu.MenuDeleteRole();
                                break;

                            case 5:
                                break;

                            default:
                                System.Console.WriteLine("Invalid input");
                                break;
                        }
                        break;

                    case 4:
                        //save state
                        objMenu.MenuSaveState();
                        break;

                    case 5:
                        //return to Main Menu
                        break;

                    default:
                        System.Console.WriteLine("Wrong choice");
                        break;
                }
            } while (moduleChoice != 5);
        }
    }
}
