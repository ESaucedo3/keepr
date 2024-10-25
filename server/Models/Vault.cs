using keepr.Interfaces;

namespace keepr.Models;

// WHAT THE SERVER WILL ONLY ACCEPT
public class VaultCreationDTO
{
  public string Name { get; set; }
  public string Description { get; set; }
  public bool? IsPrivate { get; set; }
  public string Img { get; set; }
}

// WHAT THE SERVER WILL SEND BACK
public class Vault : IRepoItem<int>
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public bool IsPrivate { get; set; }

  // RELATIONSHIP PROPERTIES
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}