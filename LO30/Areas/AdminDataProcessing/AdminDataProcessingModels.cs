using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LO30.Areas.AdminDataProcessing
{
  public class AdminDataProcessingModel
  {
    public string action { get; set; }

    public int seasonId { get; set; }

    public bool playoff { get; set; }

    public int startingGameId { get; set; }

    public int endingGameId { get; set; }

    public AdminDataProcessingModel()
    {
    }
  }

  public class AdminDataProcessingResultModel
  {
    public int toProcess { get; set; }

    public int modified { get; set; }

    public string time { get; set; }

    public AdminDataProcessingResultModel()
    {
    }
  }
}
