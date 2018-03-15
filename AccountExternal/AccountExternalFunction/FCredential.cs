using AccountExternalData;
using AccountExternalEntity;
using AccountExternalModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountExternalFunction
{
    public class FCredential : IFCredential
    {
        private IDCredential _iDCredential;

        public FCredential(IDCredential iDCredential)
        {
            _iDCredential = iDCredential;
        }
       
        public FCredential()
        {
            _iDCredential = new DCredential();
        }

        #region Create
        public Credential Create(int createdBy, Credential credential)
        {

            var eCredential = ECredential(credential);
            eCredential.CreatedDate = DateTime.Now;
            eCredential.CreatedBy = createdBy;
            eCredential.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            eCredential.Password = BCrypt.Net.BCrypt.HashPassword(credential.Password + eCredential.Salt);
            eCredential = _iDCredential.Insert(eCredential);
            return Credential(eCredential);
        }
        #endregion

        #region Read
        public bool IsMethodAccessible(int credentialId, List<string> allowedRoles)
        {
            if (allowedRoles.Count == 0)
            {
                return _iDCredential.Exists<ECredential>(a => a.CredentialId == credentialId && a.IsActive);
            }
            else
            {
                return _iDCredential.Exists<ECredential>(a => a.CredentialId == credentialId && a.IsActive && a.CredentialRoles.Any(b => allowedRoles.Contains(b.Role.Name)));
            }
        }

        public Credential Login(Credential credential)
        {
            ECredential eCredential = _iDCredential.Read<ECredential>(a => a.Username == credential.Username && a.IsActive == true);
            if (eCredential != null && BCrypt.Net.BCrypt.Verify(credential.Password + eCredential.Salt, eCredential.Password))
            {
                return Credential(eCredential);
            }
            else
            {
                return credential;
            }
        }

        public Credential Read(int credentialId)
        {
            var eCredential = _iDCredential.Read<ECredential>(a => a.CredentialId == credentialId);
            return Credential(eCredential);
        }

        public Credential Read(string credentialname)
        {
            var eCredential = _iDCredential.Read<ECredential>(a => a.Username == credentialname);
            return Credential(eCredential);
        }

        public List<Credential> Read()
        {
            var eCredentials = _iDCredential.Read();
            return Credentials(eCredentials);
        }
        #endregion

        #region Update
        public bool ChangePassword(int updatedBy, Credential credential)
        {
            ECredential eCredential = _iDCredential.Read<ECredential>(a => a.Username == credential.Username && a.IsActive == true);
            if (eCredential != null && BCrypt.Net.BCrypt.Verify(credential.Password + eCredential.Salt, eCredential.Password))
            {
                eCredential.Salt = BCrypt.Net.BCrypt.GenerateSalt();
                eCredential.Password = BCrypt.Net.BCrypt.HashPassword(credential.NewPassword + eCredential.Salt);
                eCredential = _iDCredential.Update(eCredential);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Credential Update(int updatedBy, Credential credential)
        {
            var eCredential = ECredential(credential);
            eCredential.UpdatedDate = DateTime.Now;
            eCredential.UpdatedBy = updatedBy;

            var oldECredential = _iDCredential.Read<ECredential>(a => a.CredentialId == credential.CredentialId);
            eCredential.Salt = oldECredential.Salt;
            eCredential.Password = oldECredential.Password;

            //eCredential.Salt = BCrypt.Net.BCrypt.GenerateSalt();
            //eCredential.Password = BCrypt.Net.BCrypt.HashPassword(credential.Password + eCredential.Salt);

            eCredential = _iDCredential.Update(eCredential);
            return Credential(eCredential);

        }
        #endregion

        #region Delete
        public void Delete(int credentialId)
        {
            _iDCredential.Delete<ECredential>(a => a.CredentialId == credentialId);
        }
        #endregion

        #region Other Function
        private ECredential ECredential(Credential credential)
        {
            return new ECredential
            {
                IsActive = credential.IsActive,

                CreatedDate = credential.CreatedDate,
                UpdatedDate = credential.UpdatedDate,

                CreatedBy = credential.CreatedBy,
                UpdatedBy = credential.UpdatedBy,
                CredentialId = credential.CredentialId,

                Email = credential.Email,
                Password = credential.Password,
                Username = credential.Username
            };
        }

        private Credential Credential(ECredential eCredential)
        {
            return new Credential
            {
                IsActive = eCredential.IsActive,

                CreatedDate = eCredential.CreatedDate,
                UpdatedDate = eCredential.UpdatedDate,

                CreatedBy = eCredential.CreatedBy,
                UpdatedBy = eCredential.UpdatedBy,
                CredentialId = eCredential.CredentialId,

                Email = eCredential.Email,
                Username = eCredential.Username,
                Password = eCredential.Password,
            };
        }

        private List<Credential> Credentials(List<ECredential> eCredentials)
        {
            return eCredentials.Select(a => new Credential
            {
                IsActive = a.IsActive,

                CreatedDate = a.CreatedDate,
                UpdatedDate = a.UpdatedDate,

                CreatedBy = a.CreatedBy,
                UpdatedBy = a.UpdatedBy,
                CredentialId = a.CredentialId,

                Email = a.Email,
                Username = a.Username,
                Password = a.Password,

                CredentialRoles = a.CredentialRoles.Select(b =>
                    new CredentialRole
                    {
                        CreatedDate = b.Role.CreatedDate,
                        UpdatedDate = b.UpdatedDate,

                        CreatedBy = b.CreatedBy,
                        RoleId = b.RoleId,
                        UpdatedBy = b.UpdatedBy,
                        CredentialRoleId = b.CredentialRoleId,
                        CredentialId = b.CredentialId,

                        Role = new Role
                        {
                            CreatedDate = b.Role.CreatedDate,
                            UpdatedDate = b.Role.UpdatedDate,

                            CreatedBy = b.Role.CreatedBy,
                            RoleId = b.Role.RoleId,
                            UpdatedBy = b.Role.UpdatedBy,

                            Name = b.Role.Name,
                            //Description = b.Role.Description,
                        }
                    }).ToList()
            }).ToList();
        }
        #endregion
    }
}
