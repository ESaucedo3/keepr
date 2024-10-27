namespace keepr.Repositories;
public class VaultsRepository
{
  private readonly IDbConnection _db;

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(string creatorId, VaultCreationDTO vaultData)
  {
    string sql = @"
    INSERT INTO vaults(name, description, img, isPrivate, creatorId)
    VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);

    SELECT *
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.id = LAST_INSERT_ID();";

    return _db.Query(sql, (Vault v, Profile p) =>
    {
      v.Creator = p;
      return v;
    }, new { vaultData.Name, vaultData.Description, vaultData.Img, vaultData.IsPrivate, creatorId }).FirstOrDefault();
  }


  internal Vault GetSpecificVault(int vaultId)
  {
    string sql = @"
    SELECT *
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.id = @vaultId;";

    return _db.Query(sql, (Vault v, Profile p) =>
    {
      v.Creator = p;
      return v;
    }, new { vaultId }).FirstOrDefault();
  }

  internal Vault UpdateVault(int vaultId, Vault vault)
  {
    string sql = @"
    UPDATE vaults
    SET
    name = @Name,
    description = @Description,
    isPrivate = @IsPrivate,
    img = @Img
    WHERE id = @vaultId
    LIMIT 1;

    SELECT *
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.id = @vaultId;";

    return _db.Query(sql, (Vault v, Profile p) =>
    {
      v.Creator = p;
      return v;
    }, new { vault.Name, vault.Description, vault.IsPrivate, vault.Img, vaultId }).FirstOrDefault();
  }

  internal void DeleteVault(int vaultId)
  {
    string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
    int rowsAffected = _db.Execute(sql, new { vaultId });

    if (rowsAffected == 0)
    {
      throw new Exception("Something went wrong, nothing was deleted");
    }
    else if (rowsAffected > 1)
    {
      throw new Exception("More than 1 vault was deleted not very good at all!");
    }
  }

  internal List<Vault> GetProfileVaults(string profileId)
  {
    string sql = @"
    SELECT * FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.creatorId = @profileId AND isPrivate = false
    ;";

    return _db.Query(sql, (Vault v, Profile p) =>
    {
      v.Creator = p;
      return v;
    }, new { profileId }).ToList();
  }

  internal List<Vault> GetAccountVaults(string accountId)
  {
    string sql = @"
    SELECT * FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
    WHERE vaults.creatorId = @accountId;";

    return _db.Query(sql, (Vault v, Profile p) =>
    {
      v.Creator = p;
      return v;
    }, new { accountId }).ToList();
  }
}