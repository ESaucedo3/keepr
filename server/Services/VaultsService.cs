

namespace keepr.Services;
public class VaultsService
{
  private readonly VaultsRepository _repository;

  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }

  internal Vault CreateVault(string userId, VaultCreationDTO vaultData)
  {
    return _repository.CreateVault(userId, vaultData);
  }

  internal string DeleteVault(string userId, int vaultId)
  {
    Vault vault = GetSpecificVault(vaultId);
    if (vault.CreatorId != userId) throw new Exception("You cannot delete something that isn't yours!");
    _repository.DeleteVault(vaultId);
    return $"{vault.Name} successfully deleted";
  }

  internal Vault GetSpecificVault(int vaultId)
  {
    Vault vault = _repository.GetSpecificVault(vaultId);
    if (vault == null) throw new Exception($"Invalid ID: {vaultId}");
    return vault;
  }

  internal Vault UpdateVault(string userId, int vaultId, VaultCreationDTO vaultData)
  {
    Vault vault = GetSpecificVault(vaultId);
    if (vault.CreatorId != userId) throw new Exception("You cannot update a vault that isn't yours!");
    vault.Name = vaultData.Name ?? vault.Name;
    vault.Description = vaultData.Description ?? vault.Description;
    vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;
    vault.Img = vaultData.Img ?? vault.Img;

    return _repository.UpdateVault(vaultId, vault);
  }
}