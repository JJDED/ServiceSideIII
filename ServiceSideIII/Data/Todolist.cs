using ServerSideIII.Data;
using System;
using System.Collections.Generic;

namespace ServerSideIII.Data;

public partial class Todolist
{
    public int Int { get; set; }

    public int UserId { get; set; }

    public string Item { get; set; } = null!;

    public virtual Cpr User { get; set; } = null!;
}
