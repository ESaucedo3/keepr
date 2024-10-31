


namespace keepr.Services;
public class KeepsService
{
  private readonly KeepsRepository _repository;

  public KeepsService(KeepsRepository repository)
  {
    _repository = repository;
  }

  internal Keep CreateKeep(string userId, KeepCreationDTO keepData)
  {
    return _repository.CreateKeep(userId, keepData);
  }


  internal List<Keep> GetAllKeeps()
  {
    return _repository.GetAllKeeps();
  }

  internal Keep GetSpecificKeep(int keepId)
  {
    Keep keep = _repository.GetSpecificKeep(keepId);
    if (keep == null) throw new Exception($"Invalid ID: {keepId}");
    return keep;
  }

  internal Keep UpdateKeep(string userId, int keepId, KeepCreationDTO keepData)
  {
    Keep keep = GetSpecificKeep(keepId);
    if (keep.CreatorId != userId) throw new Exception("You cannot update something that isn't yours");
    keep.Name = keepData.Name ?? keep.Name;
    keep.Description = keepData.Description ?? keep.Description;
    keep.Img = keepData.Img ?? keep.Img;

    return _repository.UpdateKeep(keepId, keep);
  }
  internal string DeleteKeep(string userId, int keepId)
  {
    Keep keep = GetSpecificKeep(keepId);
    if (keep.CreatorId != userId) throw new Exception("You cannot delete a keep that doesn't belong to you!");

    _repository.DeleteKeep(keepId);
    return $"{keep.Name} successfully deleted";
  }

  internal List<Keep> GetProfileKeeps(string profileId)
  {
    return _repository.GetProfileKeeps(profileId);
  }

  internal List<Keep> GetAccountKeeps(string userId)
  {
    return _repository.GetAccountKeeps(userId);
  }
}