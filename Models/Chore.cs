using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace choreListDot.Models;
public class Chore
{
  public int id { get; set; }
  [Required]
  public string createdAt { get; set; }
  public string chore { get; set; }
  public bool finished { get; set; }
  public string creatorId { get; set; }
  public Account Creator { get; set; }
}