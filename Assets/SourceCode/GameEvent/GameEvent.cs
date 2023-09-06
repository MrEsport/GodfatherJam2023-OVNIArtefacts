using System;
using UnityEngine.Events;

[Serializable]
public class GameEvent : UnityEvent { }

[Serializable]
public class GameEvent<T> : UnityEvent<T> { }
