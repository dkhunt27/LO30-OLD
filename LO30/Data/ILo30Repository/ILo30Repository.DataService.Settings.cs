using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<Setting> GetSettings();
    int SaveOrUpdateSettings(List<Setting> settings);
    Setting GetSettingBySettingId(int settingId);
    Setting DeleteSettingBySettingId(int settingId);
  }
}
