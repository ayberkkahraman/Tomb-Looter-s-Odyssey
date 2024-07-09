using System;
using System.Collections;
using UnityEngine;

namespace Project._Scripts.Global.ManagerSystem.ManagerClasses
{
  public class BehaviourManager : MonoBehaviour
  {
    public void RunAfterSeconds(float delayTime, Action action)
    {
      StartCoroutine(RunAfterSecondsCoroutine(delayTime, action));
    }
    
    public IEnumerator RunAfterSecondsCoroutine(float delayTime, Action action)
    {
      yield return new WaitForSeconds(delayTime);
      action?.Invoke();
    }
  }
}
