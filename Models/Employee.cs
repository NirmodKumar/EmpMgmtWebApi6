using System;
using System.Collections.Generic;
using EmpMgmtWebApi6.Models;

namespace EmpMgmtWebApi6.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Department { get; set; }
}