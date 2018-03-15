using BaseEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountExternalEntity
{
    [Table("Credential")]
    public class ECredential : EBase
    {
        public bool IsActive { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CredentialId { get; set; }
        
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Salt { get; set; }

        [Required]
        [StringLength(100)]
        [Index("Credential_Unique", 1, IsUnique = true)]
        public string Username { get; set; }

        public ICollection<ECredentialRole> CredentialRoles { get; set; }
    }
}
