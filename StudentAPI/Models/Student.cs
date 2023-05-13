using System;
using System.Collections.Generic;

namespace StudentAPI.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public int StudentAge { get; set; }

    public string StudentAddress { get; set; } = null!;

    public int ClassId { get; set; }
}
