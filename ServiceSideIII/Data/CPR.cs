using System;
using System.Collections.Generic;

namespace ServerSideIII.Data;

public partial class Cpr
{
    public int Id { get; set; }

    public string User { get; set; } = null!;

    public string CprNr { get; set; } = null!;

    public virtual ICollection<Todolist> Todolists { get; set; } = new List<Todolist>();
}