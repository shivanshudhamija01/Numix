using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootStrapper : MonoBehaviour
{
    [SerializeField] private AudioInstaller audioInstaller;
    void Awake()
    {
        ServiceLocator.Register<IInputService>(new InputService());
        ServiceLocator.Register<IEventBus>(new EventBus());
        ServiceLocator.Register<IMoveValidationService>(new MoveValidationService());
        ServiceLocator.Register<IGridDataService>(new GridDataService());
        ServiceLocator.Register<IStepTrackerService>(new StepTrackerService());
        ServiceLocator.Register<IPuzzleValidationService>(new PuzzleValidationService(ServiceLocator.Get<IGridDataService>(), ServiceLocator.Get<IStepTrackerService>(), ServiceLocator.Get<IEventBus>()));
        ServiceLocator.Register<IAudioService>(new AudioService(audioInstaller));
    }
}
