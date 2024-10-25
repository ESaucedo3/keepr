

namespace keepr.Services;
public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repository;

  public VaultKeepsService(VaultKeepsRepository repository)
  {
    _repository = repository;
  }

  internal VaultKeep CreateVaultKeep(string userId, VaultKeepCreationDTO vaultKeepData)
  {
    return _repository.CreateVaultKeep(userId, vaultKeepData);
  }

  internal List<VaultKeepProfile> GetPublicVaultKeeps(int vaultId)
  {
    throw new NotImplementedException();
  }
}