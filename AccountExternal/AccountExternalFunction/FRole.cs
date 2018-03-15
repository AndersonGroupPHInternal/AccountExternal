using AccountExternalData;
using AccountExternalEntity;
using AccountExternalModel;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AccountExternalFunction
{
    public class FRole : IFRole
    {
        private IDRole _iDRole;
        public FRole(IDRole iDRole)
        {
            _iDRole = iDRole;
        }

        public FRole()
        {
            _iDRole = new DRole();

        }

        #region Create
        public Role Create(int createBy, Role role)
        {
            var eRole = ERole(role);
            eRole.CreatedDate = DateTime.Now;
            eRole.CreatedBy = createBy;
            eRole = _iDRole.Insert(eRole);
            return Role(eRole);
        }
        #endregion

        #region Read
        public bool HasRole(int credentialId, string[] roles)
        {
            var eRoles = _iDRole.List<ERole>(a => a.CredentialRoles.Any(b => b.CredentialId == credentialId));
            return eRoles.Any(a => roles.Contains(a.Name));
        }

        public Role Read(int roleId)
        {
            var eRole = _iDRole.Read<ERole>(a => a.RoleId == roleId);
            return Role(eRole);
        }

        public List<Role> Read(string sortBy)
        {
            var eRoles = _iDRole.Read<ERole>(a => true, sortBy);
            return Roles(eRoles);
        }

        public List<Role> Read(int credentialId, string sortBy)
        {
            var eRoles = _iDRole.Read<ERole>(a => a.CredentialRoles.Any(b => b.Credential.CredentialId == credentialId), sortBy);
            return Roles(eRoles);
        }
        #endregion

        #region Update
        public Role Update(int updatedBy, Role role)
        {
            var eRole = ERole(role);
            eRole.UpdatedDate = DateTime.Now;
            eRole.UpdatedBy = updatedBy;
            eRole = _iDRole.Update(eRole);
            return Role(eRole);
        }
        #endregion

        #region Delete
        public void Delete(int roleId)
        {
            _iDRole.Delete<ERole>(a => a.RoleId == roleId);
        }
        #endregion

        #region Other Function
        private ERole ERole(Role role)
        {
            return new ERole
            {
                CreatedDate = role.CreatedDate,
                UpdatedDate = role.UpdatedDate,

                CreatedBy = role.CreatedBy,
                UpdatedBy = role.UpdatedBy,

                RoleId = role.RoleId,
                Name = role.Name,
                Description = role.Description,
            };
        }

        private Role Role(ERole eRole)
        {
            return new Role
            {
                CreatedDate = eRole.CreatedDate,
                UpdatedDate = eRole.UpdatedDate,

                CreatedBy = eRole.CreatedBy,
                UpdatedBy = eRole.UpdatedBy,

                RoleId = eRole.RoleId,
                Name = eRole.Name,
                Description = eRole.Description,
            };
        }
        private List<Role> Roles(List<ERole> eRoles)
        {
            return eRoles.Select(a => new Role
            {
                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,

                CreatedBy = a.CreatedBy,
                RoleId = a.RoleId,
                UpdatedBy = a.UpdatedBy,

                Name = a.Name,
                Description = a.Description
            }).ToList();
        }
        #endregion

    }
}
