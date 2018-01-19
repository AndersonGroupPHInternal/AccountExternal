using BaseEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountExternalEntity
{
    [Table("Role")]
    public class ERole : EBase
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(100)]
        [Index("Role_Unique", 1, IsUnique = true)]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public ICollection<ECredentialRole> CredentialRoles { get; set; }
    }
}
