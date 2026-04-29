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
        eventBus.Subscribe<Events.OnHintUsed>(OnHintUsed);
    }
    private void OnDisable()
    {
        eventBus.Unsubscribe<Events.OnHintRequested>(OnHintRequested);
        eventBus.Unsubscribe<Events.OnHintUsed>(OnHintUsed);
    }
    private void OnHintRequested(Events.OnHintRequested evt)
    {
        hintService.IsHintActive = !hintService.IsHintActive;
        bool isHintActive = hintService.IsHintActive;
        // Here also check that whether there are number of hint available or not 
        if (isHintActive)
        {
            eventBus.Publish(new Events.OnHintModeStarted());
        }
        else
        {
            eventBus.Publish(new Events.OnHintModeEnded());
        }
    }
    private void OnHintUsed(Events.OnHintUsed evt)
    {
        // here decrease the number of available hint to the user and also fire an event to disable the glow
        hintService.IsHintActive = false;
        eventBus.Publish(new Events.OnHintModeEnded());
    }   
}
