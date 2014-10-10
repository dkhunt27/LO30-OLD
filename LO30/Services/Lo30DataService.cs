using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace LO30.Services
{
  public class Lo30DataService
  {
    public Lo30DataService()
    {
    }

    public void ToJsonNewtonsoft(dynamic obj, string destPath)
    {
      var output = JsonConvert.SerializeObject(obj, Formatting.Indented);

      StringBuilder sb = new StringBuilder();
      sb.Append(output);

      using (StreamWriter outfile = new StreamWriter(destPath))
      {
        outfile.Write(sb.ToString());
      }
    }

    public T FromJsonNewtonsoft<T>(string srcPath)
    {
      string contents = File.ReadAllText(srcPath);
      T parsedJson = (T)JsonConvert.DeserializeObject(contents);
      return parsedJson;
    }

    public void ToJsonToFile<T>(T obj, string destPath)
    {
      string json = ToJson<T>(obj);

      StringBuilder sb = new StringBuilder();
      sb.Append(json);

      using (StreamWriter outfile = new StreamWriter(destPath))
      {
        outfile.Write(sb.ToString());
      }
    }

    public string ToJson<T>(T obj)
    {
      DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
      MemoryStream ms = new MemoryStream();
      ser.WriteObject(ms, obj);
      string jsonString = Encoding.UTF8.GetString(ms.ToArray());
      ms.Close();
      return jsonString;
    }

    public T FromJsonFromFile<T>(string srcPath)
    {
      string contents = File.ReadAllText(srcPath);
      return FromJson<T>(contents);
    }

    public T FromJson<T>(string jsonString)
    {
      DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
      MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
      T obj = (T)serializer.ReadObject(ms);
      return obj;
    }
  }
}

