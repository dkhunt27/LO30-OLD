using LO30.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LO30.Services
{
  public class ErrorHandlingService
  {
    public static void PrintFullErrorMessage(Exception ex)
    {
      Debug.Print("PrintFullErrorMessage:" + ex.Message);
      var innerEx = ex.InnerException;

      while (innerEx != null)
      {
        Debug.Print("PrintFullErrorMessage: With inner exception of:");
        Debug.Print(innerEx.Message);

        innerEx = innerEx.InnerException;
      }

      Debug.Print(ex.StackTrace);
    }
  }
}