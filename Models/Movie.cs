using System;
using System.Collections.Generic;

namespace EmpMgmtWebApi6.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }
}
