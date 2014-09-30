﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Article
  {
    [Key, Column(Order = 0)]
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string ImagePath { get; set; }
    public DateTime Created { get; set; }

  }
}