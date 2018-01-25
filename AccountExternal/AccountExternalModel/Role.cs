using BaseModel;
using System.Collections.Generic;

namespace AccountExternalModel
{
    public class Role : Base
    {
        public int RoleId { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public ICollection<CredentialRole> CredentialRoles { get; set; }
    }
}
