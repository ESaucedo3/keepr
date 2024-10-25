using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Controllers;

[ApiController, Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly KeepsService _keepsService;

  public KeepsController(Auth0Provider auth0Provider, KeepsService keepsService)
  {
    _auth0Provider = auth0Provider;
    _keepsService = keepsService;
  }

  [Authorize, HttpPost]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody] KeepCreationDTO keepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.CreateKeep(userInfo.Id, keepData);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _keepsService.GetAllKeeps();
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{keepId}")]
  public ActionResult<Keep> GetSpecificKeep(int keepId)
  {
    try
    {
      Keep keep = _keepsService.GetSpecificKeep(keepId);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize, HttpPut("{keepId}")]
  public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] KeepCreationDTO keepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Keep keep = _keepsService.UpdateKeep(userInfo.Id, keepId, keepData);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize, HttpDelete("{keepId}")]
  public async Task<ActionResult<string>> DeleteKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string deletedMsg = _keepsService.DeleteKeep(userInfo.Id, keepId);
      return Ok(deletedMsg);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}