using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Controllers;

[ApiController, Route("api/[controller]")]
public class VaultsController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly VaultsService _vaultsService;
  private readonly VaultKeepsService _vaultKeepsService;

  public VaultsController(Auth0Provider auth0Provider, VaultsService vaultsService, VaultKeepsService vaultKeepsService)
  {
    _auth0Provider = auth0Provider;
    _vaultsService = vaultsService;
    _vaultKeepsService = vaultKeepsService;
  }

  [Authorize, HttpPost]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] VaultCreationDTO vaultData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.CreateVault(userInfo.Id, vaultData);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}")]
  public ActionResult<Vault> GetSpecificVault(int vaultId)
  {
    try
    {
      Vault vault = _vaultsService.GetSpecificVault(vaultId);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize, HttpPut("{vaultId}")]
  public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] VaultCreationDTO vaultData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultsService.UpdateVault(userInfo.Id, vaultId, vaultData);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize, HttpDelete("{vaultId}")]
  public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string deletedMsg = _vaultsService.DeleteVault(userInfo.Id, vaultId);
      return Ok(deletedMsg);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{vaultId}/keeps")]
  public ActionResult<List<VaultKeepProfile>> GetPublicVaultKeeps(int vaultId)
  {
    try
    {
      List<VaultKeepProfile> vaultKeeps = _vaultKeepsService.GetPublicVaultKeeps(vaultId);
      return Ok(vaultKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}