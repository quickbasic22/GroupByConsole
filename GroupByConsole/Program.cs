
using GroupByConsole;
using System.Text;

var employeeGroup = from employee in Employee.GetAllEmployees()
                    group employee by employee.Department;
Console.WriteLine("Employee count by Department");
foreach (var group in employeeGroup)
{
    Console.WriteLine("{0} - {1}", group.Key, group.Count());
    Console.WriteLine("-------------------------------");
    foreach (var employee in group)
    {
        Console.WriteLine(employee.Name);
    }
}

Console.WriteLine("-----------------------------------------------------");
Console.WriteLine("Department Max Salary");

var employeeGroup2 = from employee in Employee.GetAllEmployees()
                     group employee by employee.Department into eGroup
                     orderby eGroup.Key
                     select new
                     {
                         Key = eGroup.Key,
                         Employee = eGroup,
                         MaxSalary = eGroup.Max(x => x.Salary)
                     };
Console.WriteLine($"Max salary is : {employeeGroup2.First().MaxSalary}");

foreach (var group in employeeGroup2)
{
    Console.WriteLine("{0} - {1}", group.Key, group.Employee.Max(x => x.Salary));
    Console.WriteLine("-------------------------------");
}



Console.WriteLine("----------------------------------------------------------");
Console.WriteLine("Female Employee count by Department");
var employeeGroup3 = from employee in Employee.GetAllEmployees()
                     group new
                     {
                         employee.Department,
                         employee.Gender
                     }
                     by employee.Department into eGroup
                     select eGroup;


foreach (var group in employeeGroup3)
{
    Console.WriteLine("-------------------------------");
    Console.WriteLine("{0} has {1} {2}", group.Key, group.Count(f => f.Gender == "Female"), group.Count(f => f.Gender == "Female") > 1 ? "Females" : "Female");
    Console.WriteLine("{0} has {1} {2}", group.Key, group.Count(f => f.Gender == "Male"), group.Count(m => m.Gender == "Male") > 1 ? "Males" : "Male");
   
    foreach (var em in group)
    {
        Console.WriteLine("Department {0} Gender {1}", em.Department,  em.Gender);
    }
}


var employees = Employee.GetAllEmployees().OrderBy(x => x.Department).ThenBy(s => s.Salary);
StringBuilder sb = new StringBuilder();

Console.WriteLine("{0} {1, 30} {2, 30}", "Department", "Name", "Salary");
Console.WriteLine();
int changer = 0;
foreach (var employee in employees)
{
    if (employee.Department == "HR")
    {
        sb.AppendLine(string.Format("{0} {1, 40} {2, 30}", employee.Department, employee.Name, employee.Salary));
        Console.WriteLine(sb.ToString());
        sb.Clear();
    }
    else
    {
        changer++;
        if (changer <= 1)
            Console.WriteLine();
        sb.Append(String.Format("{0} {1, 40} {2, 30}", employee.Department, employee.Name, employee.Salary));
        Console.WriteLine(sb.ToString());
        sb.Clear();
    }

    
}


Console.ReadKey();

