using BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountExternalEntity
{
    [Table("CredentialRole")]
    public class ECredentialRole : EBase
    {
        [ForeignKey("Credential")]
        [Index("CredentialRole_Unique", 1, IsUnique = true)]
        public int CredentialId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CredentialRoleId { get; set; }
        [ForeignKey("Role")]
        [Index("CredentialRole_Unique", 2, IsUnique = true)]
        public int RoleId { get; set; }

        public ECredential Credential { get; set; }
        public ERole Role { get; set; }
    }
}
