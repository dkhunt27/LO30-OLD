using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Lo30Repository : ILo30Repository
  {
    Lo30Context _ctx;
    public Lo30Repository(Lo30Context ctx)
    {
      _ctx = ctx;
    }

    public IQueryable<Article> GetArticles()
    {
      return _ctx.Articles;
    }

    public bool Save()
    {
      try
      {
        return _ctx.SaveChanges() > 0;
      }
      catch (Exception ex)
      {
        // TODO log this error
        return false;
      }
    }

    public bool AddArticle(Article newArticle)
    {
      try
      {
          _ctx.Articles.Add(newArticle);
        return true;
      }
      catch (Exception ex)
      {
        // TODO log this error
        return false;
      }
    }
  }
}