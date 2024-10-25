
namespace keepr.Repositories;
public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeep CreateVaultKeep(string creatorId, VaultKeepCreationDTO vaultKeepData)
  {
    string sql = @"
    INSERT INTO vault_keeps(keepId, vaultId, creatorId)
    VALUES(@KeepId, @VaultId, @CreatorId);

    SELECT * FROM vault_keeps
    WHERE id = LAST_INSERT_ID();";

    return _db.Query<VaultKeep>(sql, new { vaultKeepData.KeepId, vaultKeepData.VaultId, creatorId }).FirstOrDefault();
  }
}