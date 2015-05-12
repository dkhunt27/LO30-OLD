using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class Article
  {
    [Required, Key, Column(Order = 0)]
    public int ArticleId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }

    public string ImagePath { get; set; }

    [Required]
    public DateTime Created { get; set; }
  }
}