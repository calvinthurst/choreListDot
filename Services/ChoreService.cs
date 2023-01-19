using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace choreListDot.Services;
public class ChoreService
{
  private readonly ChoreRepository _repo;
  public ChoreService(ChoreRepository repo)
  {
    _repo = repo;
  }
  internal List<Chore> Get(string userId)
  {
    List<Chore> chores = _repo.Get();
    List<Chore> filtered = chores.FindAll(a => a.finished == false || a.creatorId == userId);
    return filtered;
  }
  internal Chore Create(Chore choreData)
  {
    Chore chore = _repo.Create(choreData);
    return chore;
  }
}