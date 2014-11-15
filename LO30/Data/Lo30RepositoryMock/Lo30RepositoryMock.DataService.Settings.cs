using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    public List<Setting> GetSettings()
    {
      throw new NotImplementedException();
    }

    public int SaveOrUpdateSettings(List<Setting> settings)
    {
      throw new NotImplementedException();
    }

    public Setting GetSettingBySettingId(int settingId)
    {
      throw new NotImplementedException();
    }

    public Setting DeleteSettingBySettingId(int settingId)
    {
      throw new NotImplementedException();
    }
  }
}