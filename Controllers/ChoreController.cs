using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace choreListDot.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ChoreController : ControllerBase
{
  private readonly ChoreService _choreService;
  private readonly Auth0Provider _auth0Provider;
  public ChoreController(ChoreService choreService, Auth0Provider auth0Provider)
  {
    _choreService = choreService;
    _auth0Provider = auth0Provider;
  }
  [HttpGet]
  public async Task<ActionResult<List<Chore>>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Chore> chores = _choreService.Get(userInfo?.Id);
      return Ok(chores);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<List<Chore>>> Create([FromBody] Chore choreData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      choreData.creatorId = userInfo.Id;
      Chore chore = _choreService.Create(choreData);
      chore.Creator = userInfo;
      return Ok(chore);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}