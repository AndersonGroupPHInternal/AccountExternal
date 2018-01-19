using AccountExternalEntity;
using BaseData;
using System.Collections.Generic;

namespace AccountExternalData
{
    public interface IDCredential : IDBase
    {
        #region Read
        List<ECredential> Read();
        #endregion
    }
}
    