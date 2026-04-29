using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HintSystem : MonoBehaviour
{
    private IEventBus eventBus;
    private IHintService hintService;
    private void Awake()
    {
        eventBus = ServiceLocator.Get<IEventBus>();
        hintService = ServiceLocator.Get<IHintService>();
    }
    private void OnEnable()
    {
        eventBus.Subscribe<Events.OnHintRequested>(OnHintRequested);
        // eventBus.Subscribe<Events.OnHintModeStarted>();
        // eventBus.Subscribe<Events.OnHintUsed>();
        // eventBus.Subscribe<Events.OnHintModeEnded>();
    }
    private void OnDisable()
    {
        eventBus.Unsubscribe<Events.OnHintRequested>(OnHintRequested);
        // eventBus.Unsubscribe<Events.OnHintModeStarted>();
        // eventBus.Unsubscribe<Events.OnHintUsed>();
        // eventBus.Unsubscribe<Events.OnHintModeEnded>();
    }
    private void OnHintRequested(Events.OnHintRequested evt)
    {
        bool isHintActive = !hintService.IsHintActive;
        if (isHintActive)
        {
            eventBus.Publish(new Events.OnHintModeStarted());
        }
        else
        {
            eventBus.Publish(new Events.OnHintModeEnded());
        }
    }

}
