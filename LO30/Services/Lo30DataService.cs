using LO30.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace LO30.Services
{
  class TableList
  {
    public string QueryBegin { get; set; }
    public string QueryEnd { get; set; }
    public string TableName { get; set; }
    public string FileName { get; set; }
  }

  public class Lo30DataService
  {
    private string _folderPath;
    private string _connString;

    public Lo30DataService()
    {
      _folderPath = "C:\\git\\LO30\\LO30\\Data\\Json\\";
    }

    public void ToJson<T>(T obj, string destPath)
    {
      var output = JsonConvert.SerializeObject(obj, Formatting.Indented);

      StringBuilder sb = new StringBuilder();
      sb.Append(output);

      using (StreamWriter outfile = new StreamWriter(destPath))
      {
        outfile.Write(sb.ToString());
      }
    }

    public T FromJson<T>(string srcPath)
    {
      string contents = File.ReadAllText(srcPath);
      T parsedJson = (T)JsonConvert.DeserializeObject(contents);
      return parsedJson;
    }
  }
}
