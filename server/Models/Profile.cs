using keepr.Interfaces;

namespace keepr.Models;

public class AccountEditDTO
{
  public string Name { get; set; }
  public string Picture { get; set; }
  public string CoverImg { get; set; }
}

public class Profile : IRepoItem<string>
{
  public string Id { get; set; }
  public string Name { get; set; }
  public string Picture { get; set; }
  public string CoverImg { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}