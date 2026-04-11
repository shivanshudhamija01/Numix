using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootStrapper : MonoBehaviour
{
    void Awake()
    {
        ServiceLocator.Register<IInputService>(new InputService());
        ServiceLocator.Register<IEventBus>(new EventBus());
    }
}
