using System;
using System.Collections.Generic;

namespace StudentAPI.Models;

public partial class CourseM
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string Instructor { get; set; } = null!;
}
