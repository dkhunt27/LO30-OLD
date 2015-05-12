using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class ProcessingResult
  {
    public int toProcess { get; set; }

    public int modified { get; set; }

    public string time { get; set; }

    public string error { get; set; }

    public ProcessingResult()
    {
    }
  }
}