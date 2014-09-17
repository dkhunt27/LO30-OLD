using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Article
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string ImagePath { get; set; }
    public DateTime Created { get; set; }

  }
}