using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Domain.Entities;

public class UserNote : BaseGuidEntity
{
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public Guid? NoteId { get; set; }
    public Note? Note { get; set; }
}
