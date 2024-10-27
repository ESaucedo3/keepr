namespace keepr.Repositories;
public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(string creatorId, KeepCreationDTO keepData)
  {
    string sql = @"
    INSERT INTO keeps(name, description, img, creatorId)
    VALUES(@Name, @Description, @Img, @CreatorId);

    SELECT * 
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.id = LAST_INSERT_ID();";

    return _db.Query(sql, (Keep k, Profile p) =>
    {
      k.Creator = p;
      return k;
    }, new { keepData.Name, keepData.Description, keepData.Img, creatorId }).FirstOrDefault();
  }


  internal List<Keep> GetAllKeeps()
  {
    string sql = @"
    SELECT * 
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId;";

    return _db.Query(sql, (Keep k, Profile p) =>
    {
      k.Creator = p;
      return k;
    }).ToList();
  }

  internal Keep GetSpecificKeep(int keepId)
  {
    string sql = @"
    UPDATE keeps SET views = views + 1 WHERE keeps.id = @keepId;
    
    SELECT * 
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.id = @keepId;
    ;";

    return _db.Query(sql, (Keep k, Profile p) =>
    {
      k.Creator = p;
      return k;
    }, new { keepId }).FirstOrDefault();
  }

  internal Keep UpdateKeep(int keepId, Keep keep)
  {
    string sql = @"
      UPDATE keeps
      SET 
      name = @Name,
      description = @Description,
      img = @Img
      WHERE id = @keepId
      LIMIT 1;

      SELECT *
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creatorId
      WHERE keeps.id = @keepId;";

    return _db.Query(sql, (Keep k, Profile p) =>
    {
      k.Creator = p;
      return k;
    }, new { keep.Name, keep.Description, keep.Img, keepId }).FirstOrDefault();
  }

  internal void DeleteKeep(int keepId)
  {
    string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
    int rowsAffected = _db.Execute(sql, new { keepId });

    if (rowsAffected == 0)
    {
      throw new Exception("Something went wrong, nothing was deleted");
    }
    else if (rowsAffected > 1)
    {
      throw new Exception("More than 1 keep was deleted not very good at all!");
    }
  }

  internal List<Keep> GetProfileKeeps(string profileId)
  {
    string sql = @"
    SELECT * FROM keeps
    JOIN 
      accounts ON accounts.id = keeps.creatorId
    WHERE
      keeps.creatorId = @profileId;
    ;";

    return _db.Query(sql, (Keep k, Profile p) =>
    {
      k.Creator = p;
      return k;
    }, new { profileId }).ToList();
  }
}