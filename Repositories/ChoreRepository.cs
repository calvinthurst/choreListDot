using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace choreListDot.Repositories;
public class ChoreRepository
{
  private readonly IDbConnection _db;
  public ChoreRepository(IDbConnection db)
  {
    _db = db;
  }
  internal List<Chore> Get()
  {
    string sql = @"
    SELECT
    ch.*,
    ac.*
    FROM chores ch
    JOIN accounts ac ON ac.id = ch.creatorId;
    ";
    List<Chore> chores = _db.Query<Chore, Account, Chore>(sql, (chore, account) =>
    {
      chore.Creator = account;
      return chore;
    }).ToList();
    return chores;
  }
  internal Chore Create(Chore choreData)
  {
    string sql = @"
    INSERT INTO chores
    (chore)
    VALUES
    (@chore);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, choreData);
    choreData.id = id;
    return choreData;
  }
}