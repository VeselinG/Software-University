namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using TeisterMask.DataProcessor.ImportDto;
    using System.Xml.Serialization;
    using System.Text;
    using System.IO;
    using TeisterMask.Data.Models;
    using System.Globalization;
    using Castle.Core.Internal;
    using TeisterMask.Data.Models.Enums;
    using Newtonsoft.Json;
    using System.Linq;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var xmlSerializer = new XmlSerializer(typeof(ImportProjectDto[]), new XmlRootAttribute("Projects"));

            var sb = new StringBuilder();

            using (var reader = new StringReader(xmlString))
            {
                var projectsDto = (ImportProjectDto[])xmlSerializer.Deserialize(reader);

                var projects = new List<Project>();

                foreach (var projectDto in projectsDto)
                {
                    //If all prop are valid in DTOProject
                    if (!IsValid(projectDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    //If OpenDate can parse to wanted date and format
                    DateTime projectOpenDate;

                    bool isProjectOpenDateValid = DateTime
                        .TryParseExact(projectDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out projectOpenDate);

                    if (!isProjectOpenDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    //If DueDate can parse to wanted date and format, else if is NullOrEmpty then null
                    DateTime? projectDueDate;

                    if (!projectDto.DueDate.IsNullOrEmpty())
                    {
                        DateTime correctProjectDueDate;

                        bool isProjectDueDateValid = DateTime
                        .TryParseExact(projectDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out correctProjectDueDate);

                        if (!isProjectDueDateValid)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        projectDueDate = correctProjectDueDate;
                    }
                    else
                    {
                        projectDueDate = null;
                    }

                    //Create new Project
                    var currentProject = new Project()
                    {
                        Name = projectDto.Name,
                        OpenDate = projectOpenDate,
                        DueDate = projectDueDate
                    };

                    //Validate all Tasks
                    foreach (var task in projectDto.Tasks)
                    {
                        //If all prop are valid in DTOTask
                        if (!IsValid(task))
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        //If TaskOpenDate can parse to wanted date and format
                        DateTime correctTaskOpenDate;

                        bool isCorrectTaskOpenDate = DateTime
                        .TryParseExact(task.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out correctTaskOpenDate);

                        if (!isCorrectTaskOpenDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        //If TaskDueDate can parse to wanted date and format
                        DateTime correctTaskDueDate;

                        bool isCorrectTaskDueDate = DateTime
                        .TryParseExact(task.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out correctTaskDueDate);

                        if (!isCorrectTaskDueDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        //If TaskOpenDate is before ProjectOpenDate
                        if (correctTaskOpenDate < projectOpenDate)
                        {
                            sb.AppendLine(ErrorMessage);
                            continue;
                        }

                        //First check id ProjectOpenDate is not null, then if TaskDueDate is after ProjectDueDate
                        if (projectDueDate.HasValue)
                        {
                            if (correctTaskDueDate > projectDueDate)
                            {
                                sb.AppendLine(ErrorMessage);
                                continue;
                            }
                        }

                        //Create new Task
                        var currentTask = new Task()
                        {
                            Name = task.Name,
                            OpenDate = correctTaskOpenDate,
                            DueDate = correctTaskDueDate,
                            ExecutionType = (ExecutionType)task.ExecutionType,
                            LabelType = (LabelType)task.LabelType,
                        };

                        //Add it in created(current) Project
                        currentProject.Tasks.Add(currentTask);
                    }

                    //Add the project in the list of projects and append the message
                    projects.Add(currentProject);
                    sb.AppendLine(String.Format(SuccessfullyImportedProject, currentProject.Name, currentProject.Tasks.Count));
                }

                //Add projects in DB (table Projects)
                context.Projects.AddRange(projects);
                context.SaveChanges();

                //Return all messages
                return sb.ToString().TrimEnd();
            }
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var emoployeesDto = JsonConvert.DeserializeObject<ImportEmployeeDto[]>(jsonString);

            var sb = new StringBuilder();

            var employees = new List<Employee>();
            
            foreach (var employeeDto in emoployeesDto)
            {
                //If all prop are valid in DtoEmployee
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                //Create new Employee
                var currentEmployee = new Employee()
                {
                    Username = employeeDto.Username,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone
                };

                //Check if alldistinct tasks are valid
                foreach (var tasks in employeeDto.Tasks.Distinct())
                {

                    var findTaskInDb = context.Tasks.FirstOrDefault(t => t.Id == tasks);

                    if (findTaskInDb == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    //Add valid task in Employee
                    currentEmployee.EmployeesTasks.Add(new EmployeeTask
                    {
                        Employee = currentEmployee,
                        Task = findTaskInDb
                    });
                }

                //Add created employee in the list of employees
                employees.Add(currentEmployee);
                sb.AppendLine(String.Format(SuccessfullyImportedEmployee, currentEmployee.Username, currentEmployee.EmployeesTasks.Count));
            }

            //Add in DB the list of employees
            context.Employees.AddRange(employees);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}