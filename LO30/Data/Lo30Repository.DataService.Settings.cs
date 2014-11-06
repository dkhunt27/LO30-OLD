using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30Repository
  {
    public List<Setting> GetSettings()
    {
      return _ctx.Settings.OrderBy(x=>x.SettingName).ToList();
    }

    public int SaveOrUpdateSettings(List<Setting> settings)
    {
      int results = _contextService.SaveOrUpdateSetting(settings);
      return results;
    }

    public Setting GetSettingBySettingId(int settingId)
    {
      var results = _contextService.FindSetting(settingId);
      return results;
    }

    public Setting DeleteSettingBySettingId(int settingId)
    {
      var results = _contextService.FindSetting(settingId);

      _ctx.Settings.Remove(results);
      _contextService.ContextSaveChanges();

      return results;
    }


  }
}