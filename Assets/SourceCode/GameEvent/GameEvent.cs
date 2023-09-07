using System;
using UnityEngine.Events;

[Serializable]
public class GameEvent : UnityEvent { }

[Serializable]
public class GameEvent<T> : UnityEvent<T> { }

[Serializable]
public class GameEvent<T0, T1> : UnityEvent<T0, T1> { }
