using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Domain.Entities;

public class CurrentAccountNote : BaseGuidEntity
{
    public Guid? CurrentAccountId { get; set; }
    public CurrentAccount? CurrentAccount { get; set; }
    public Guid? NoteId { get; set;}
    public Note? Note { get; set; }
}
