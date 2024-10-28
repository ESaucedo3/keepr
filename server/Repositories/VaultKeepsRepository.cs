



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

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    string sql = "DELETE FROM vault_keeps WHERE id = @vaultKeepId LIMIT 1;";
    int rowsAffected = _db.Execute(sql, new { vaultKeepId });

    if (rowsAffected == 0)
    {
      throw new Exception("Nothing was deleted something went wrong");
    }
    else if (rowsAffected > 1)
    {
      throw new Exception("More than 1 Vault Keep was unfavorited not good at all!");
    }
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
    SELECT * 
    FROM vault_keeps
    WHERE id = @vaultKeepId;";

    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
  }

  internal List<VaultKeepKeep> GetVaultKeeps(int vaultId)
  {
    string sql = @"
    SELECT 
      vault_keeps.id AS vaultKeepId,
      keeps.*,
      accounts.*
    FROM
      vault_keeps
      JOIN keeps ON keeps.id = vault_keeps.keepId
      JOIN accounts ON accounts.id = vault_keeps.creatorId
    WHERE
      vault_keeps.vaultId = @vaultId;";

    return _db.Query(sql, (VaultKeepKeep vk, Profile p) =>
    {
      vk.Creator = p;
      return vk;
    }, new { vaultId }).ToList();
  }
}