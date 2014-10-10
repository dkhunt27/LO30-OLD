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

    [Required, ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, ForeignKey("PlayerStatusType")]
    public int PlayerStatusTypeId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    
    [Required]
    public bool Archive { get; set; }

    public virtual Player Player { get; set; }
    public virtual PlayerStatusType PlayerStatusType { get; set; }
  }
}