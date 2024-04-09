namespace Project._Scripts.Runtime.Interfaces
{
  public interface IInteractable
  {
    public bool Activated { get; set; }
    void StartInteraction();
    void EndInteraction();
  }
}
