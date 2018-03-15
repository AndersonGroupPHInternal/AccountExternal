using AccountExternalModel;
using System.Collections.Generic;

namespace AccountExternalFunction
{
    public interface IFCredential
    {
        #region Create
        Credential Create(int createdBy, Credential credential);
        #endregion

        #region Read
        bool IsMethodAccessible(int credentialId, List<string> allowedRoles);
        Credential Login(Credential credential);
        Credential Read(int credentialId);
        Credential Read(string username);
        List<Credential> Read();
        #endregion

        #region Update
        bool ChangePassword(int updatedBy, Credential credential);
        Credential Update(int updatedBy, Credential credential);
        #endregion

        #region Delete
        void Delete(int credentialId);
        #endregion
    }
}
