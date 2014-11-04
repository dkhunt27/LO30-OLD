using LO30.Data.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LO30.Models
{
  public class SettingsModel
  {
    public List<Setting> settings { get; set; }

    public SettingsModel()
    {
    }
  }
}
