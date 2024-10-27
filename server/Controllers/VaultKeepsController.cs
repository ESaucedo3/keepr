using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Controllers;

[ApiController, Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultKeepsService _vaultKeepsService;

  public VaultKeepsController(Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService)
  {
    _auth0Provider = auth0Provider;
    _vaultKeepsService = vaultKeepsService;
  }

  [Authorize, HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeepCreationDTO vaultKeepData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(userInfo.Id, vaultKeepData);
      return Ok(vaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize, HttpDelete("{vaultKeepId}")]
  public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string deletedMsg = _vaultKeepsService.DeleteVaultKeep(userInfo.Id, vaultKeepId);
      return Ok(deletedMsg);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}