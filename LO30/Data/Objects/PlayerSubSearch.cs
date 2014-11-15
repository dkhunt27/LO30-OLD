using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class PlayerSubSearch
  {
    [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int PlayerId { get; set; }

    [Required, MaxLength(35)]
    public string FirstName { get; set; }

    [Required, MaxLength(35)]
    public string LastName { get; set; }

    [MaxLength(5)]
    public string Suffix { get; set; }

    [Required, Key, Column(Order = 5), MaxLength(1)]
    public string Position { get; set; }

    [MaxLength(35)]
    public string TeamName { get; set; }

    [Required, MaxLength(35)]
    public string Rating { get; set; }
  }
}