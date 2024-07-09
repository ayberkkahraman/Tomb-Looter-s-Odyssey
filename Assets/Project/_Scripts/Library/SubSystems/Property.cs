using System;
using System.Linq;
using JetBrains.Annotations;

namespace Project._Scripts.Library.SubSystems
{
  public record Property<T> 
  {
    public Delegate PropertyDelegate;
    public Property(Action<T> delegateHandler) => PropertyDelegate = delegateHandler;
    public Property<T> Invoke([NotNull]params object[] args)
    {
      if (args.Length == 0) args = new object[] { "Invoke" };
      
      PropertyDelegate.DynamicInvoke(args);
      return this;
    }
    public Property<T> Subscribe([NotNull] params Action<T>[] methods)
    {
      methods.ToList().ForEach(x => { PropertyDelegate = Delegate.Combine(PropertyDelegate, x); });
      return this;
    }
    public Property<T> UnSubscribe([NotNull] params Action<T>[] methods)
    {
      methods.ToList().ForEach(x => { PropertyDelegate = Delegate.Remove(PropertyDelegate, x); });
      return this;
    }
  }
}