using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Domain.Entities;

public class BusinessNote : BaseGuidEntity
{
    public Guid? BusinessId { get; set; }
    public Business? Business { get; set; }
    public Guid? NoteId { get; set; }
    public Note? Note { get; set; }
}
