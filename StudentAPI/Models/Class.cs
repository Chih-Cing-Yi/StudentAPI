using System;
using System.Collections.Generic;

namespace StudentAPI.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;
}
