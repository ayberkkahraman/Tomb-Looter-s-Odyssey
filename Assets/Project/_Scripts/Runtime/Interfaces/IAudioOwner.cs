using Project._Scripts.Runtime.Managers.Manager;
using Project._Scripts.Runtime.Managers.ManagerClasses;

namespace Project._Scripts.Runtime.Interfaces
{
  public interface IAudioOwner
  {
    public IAudioOwner AudioOwner { get; set; }
    public static AudioManager AudioManager => ManagerContainer.Instance.GetInstance<AudioManager>();
    public void Play(string audioName) => AudioManager.PlayAudio(audioName);
  }
}