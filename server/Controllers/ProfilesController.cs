namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly AccountService _accountService;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;

  public ProfilesController(Auth0Provider auth0Provider, AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
  {
    _auth0Provider = auth0Provider;
    _accountService = accountService;
    _keepsService = keepsService;
    _vaultsService = vaultsService;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetProfile(string profileId)
  {
    try
    {
      Profile profile = _accountService.GetProfile(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetProfileKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.GetProfileKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public ActionResult<List<Vault>> GetProfileVaults(string profileId)
  {
    try
    {
      List<Vault> vaults = _vaultsService.GetProfileVaults(profileId);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
