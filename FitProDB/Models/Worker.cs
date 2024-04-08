using System;
using System.Collections.Generic;

namespace FitProDB.Models;

public partial class Worker
{
    public long? Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public int? PhoneNumber { get; set; }
}
