using keepr.Interfaces;

namespace keepr.Models;

// WHAT THE SERVER WILL ONLY ACCEPT
public class KeepCreationDTO
{
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
}

// WHAT THE SERVER WILL SEND BACK
public class Keep : IRepoItem<int>
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public int Views { get; set; }

  // RELATIONSHIP PROPERTIES
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public int Kept { get; set; }
}
