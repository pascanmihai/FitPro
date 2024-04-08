﻿using System;
using System.Collections.Generic;

namespace FitProDB.Models;

public partial class User
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }
}
