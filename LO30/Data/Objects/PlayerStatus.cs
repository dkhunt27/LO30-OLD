using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class PlayerStatus
  {
    [Required, Key]
    public int PlayerStatusId { get; set; }

    // TODO figure out PK2
    //[Required, ForeignKey("Player"), Index("PK2", 1, IsUnique = true)]
    [Required, ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, ForeignKey("PlayerStatusType")]
    public int PlayerStatusTypeId { get; set; }

    [Required]
    public int StartYYYYMMDD { get; set; }

    //[Required, Index("PK2", 2, IsUnique = true)]
    [Required]
    public int EndYYYYMMDD { get; set; }

    [Required]
    public bool CurrentStatus { get; set; }

    public virtual Player Player { get; set; }
    public virtual PlayerStatusType PlayerStatusType { get; set; }
  }
}