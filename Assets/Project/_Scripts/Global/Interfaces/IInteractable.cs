namespace Project._Scripts.Global.Interfaces
{
  public interface IInteractable
  {
    public bool Activated { get; set; }
    void StartInteraction();
    void EndInteraction();
  }
}
