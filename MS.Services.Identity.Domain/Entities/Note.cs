
using MS.Services.Core.Data.Data.Attributes;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities
{
    public class Note : BaseSoftDeleteEntity
    {
        [QuerySearch]
        public string? Content { get; set; }
        [QuerySearch]
        public string? Status { get; set; } //onlarda durum hakkında string bir alan var ne yazdıklarını bilmiyorum
        public UserEmployee? CreatedByUser { get; set; }
        //public NoteTypeEnum? NoteType { get; set; }
        //public Guid RelationID { get; set; }
    }
}
