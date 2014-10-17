using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LO30.Models
{
  public class DataProcessingModel
  {
    public string action { get; set; }

    public int seasonId { get; set; }

    public bool playoff { get; set; }

    public int startingGameId { get; set; }

    public int endingGameId { get; set; }

    public DataProcessingModel()
    {
    }
  }
}
