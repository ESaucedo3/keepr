using keepr.Interfaces;
namespace keepr.Models;

public class VaultKeepCreationDTO
{
  public int KeepId { get; set; }
  public int VaultId { get; set; }
}

public class VaultKeep : IRepoItem<int>
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int KeepId { get; set; }
  public int VaultId { get; set; }
  public string CreatorId { get; set; }
}

public class VaultKeepKeep : Keep
{
  public int VaultKeepId { get; set; }
}
