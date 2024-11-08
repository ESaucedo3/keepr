namespace keepr.Services;
public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;
  private readonly VaultsService _vaultsService;
  private readonly KeepsService _keepsService;

  public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService, KeepsService keepsService)
  {
    _repository = repository;
    _vaultsService = vaultsService;
    _keepsService = keepsService;
  }

  internal VaultKeep CreateVaultKeep(string userId, VaultKeepCreationDTO vaultKeepData)
  {
    Vault vault = _vaultsService.GetSpecificVault(vaultKeepData.VaultId);
    if (vault.CreatorId != userId) throw new Exception("You cannot create a vault keep under as someone else!");
    return _repository.CreateVaultKeep(userId, vaultKeepData);
  }

  internal List<VaultKeepKeep> GetVaultKeeps(string userId, int vaultId)
  {
    Vault vault = _vaultsService.GetSpecificVault(vaultId);
    if (vault.IsPrivate == true && vault.CreatorId != userId) throw new Exception("Vault is currently private therefore you cannot gain access to it only it's creator may gain access!");
    return _repository.GetVaultKeeps(vault.Id);
  }

  internal string DeleteVaultKeep(string userId, int vaultKeepId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
    if (vaultKeep.CreatorId != userId) throw new Exception("You cannot delete this vault keep!");
    _repository.DeleteVaultKeep(vaultKeep.Id);
    return "VaultKeep successfully deleted..";
  }

  private VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
    if (vaultKeep == null) throw new Exception($"Invalid ID: {vaultKeepId}");
    return vaultKeep;
  }
}