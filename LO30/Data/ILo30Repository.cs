using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LO30.Data
{
    public interface ILo30Repository
    {
        IQueryable<Article> GetArticles();

        bool Save();

        bool AddArticle(Article newArticle);
    }
}
