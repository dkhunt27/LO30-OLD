using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Player
  {
    [Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int PlayerId { get; set; }

    [Required, MaxLength(35)]
    public string FirstName { get; set; }

    [Required, MaxLength(35)]
    public string LastName { get; set; }

    [MaxLength(5)]
    public string Suffix { get; set; }

    [Required, MaxLength(1)]
    public string PreferredPosition { get; set; }

    [Required, MaxLength(1)]
    public string Shoots { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Profession { get; set; }

    public string WifesName { get; set; }

    public static List<Player> LoadListFromAccessDbJsonFile(string filePath)
    {
      string className = "Player";
      string functionName = "LoadFromJsonFile";
      List<Player> output = new List<Player>();

      Debug.Print(string.Format("{0}: {1} Loading...", functionName, className));
      var start = DateTime.Now;

      string contents = File.ReadAllText(filePath);
      dynamic parsedJson = JsonConvert.DeserializeObject(contents);
      int count = parsedJson.Count;
      Debug.Print(string.Format("{0}: {1} Count:", functionName, className, count));

      for (var d = 0; d < parsedJson.Count; d++)
      {
        if (d > 0 && d % 100 == 0) Debug.Print(string.Format("{0}: {1} Processed:", functionName, className, d));

        var json = parsedJson[d];

        #region build object

        int playerId = json["PLAYER_ID"];

        string firstName = json["PLAYER_FIRST_NAME"];
        if (string.IsNullOrWhiteSpace(firstName))
        {
          firstName = "_";
        };

        string lastName = json["PLAYER_LAST_NAME"];
        if (string.IsNullOrWhiteSpace(lastName))
        {
          lastName = "_";
        };

        string position, positionMapped;

        position = json["PLAYER_POSITION"];

        if (string.IsNullOrWhiteSpace(position))
        {
          position = "X";
        }

        switch (position.ToLower())
        {
          case "forward":
            positionMapped = "F";
            break;
          case "defense":
            positionMapped = "D";
            break;
          case "goalie":
            positionMapped = "G";
            break;
          default:
            positionMapped = "X";
            break;
        }

        string shoots, shootsMapped;
        shoots = json["SHOOTS"];
        if (string.IsNullOrWhiteSpace(shoots))
        {
          shoots = "X";
        }

        switch (shoots.ToLower())
        {
          case "l":
            shootsMapped = "L";
            break;
          case "r":
            shootsMapped = "R";
            break;
          default:
            shootsMapped = "X";
            break;
        }

        DateTime? birthDate = null;

        if (json["BIRTHDATE"] != null)
        {
          birthDate = json["BIRTHDATE"];
        }
        #endregion

        // add to output
        output.Add(new Player()
        {
          PlayerId = playerId,
          FirstName = firstName,
          LastName = lastName,
          Suffix = json["PLAYER_SUFFIX"],
          PreferredPosition = positionMapped,
          Shoots = shootsMapped,
          BirthDate = birthDate,
          Profession = json["PROFESSION"],
          WifesName = json["WIFES_NAME"]
        });
      }

      Debug.Print(string.Format("{0}: {1} Loaded", functionName, className));
      var end = DateTime.Now - start;
      Debug.Print("TimeToProcess: " + end.ToString());

      return output;
    }
  }
}