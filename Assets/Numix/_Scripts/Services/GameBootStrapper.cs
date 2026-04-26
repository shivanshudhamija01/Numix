using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootStrapper : MonoBehaviour
{
    [SerializeField] private AudioInstaller audioInstaller;
    [SerializeField] private LevelLoader levelLoader;

    private IEventBus eventBus;
    private IInputService inputService;
    private IMoveValidationService moveValidationService;
    private IGridDataService gridDataService;
    private IStepTrackerService stepTrackerService;
    private IPuzzleValidationService puzzleValidationService;
    private IAudioService audioService;
    private IGameServices gameServices;
    private IUIBootStrap uiBootStrap;

    void Awake()
    {
        RegisterServices();
        SetServiceReferences();
        InitializeServices();
    }
    private void RegisterServices()
{
    var eventBus = new EventBus();
    ServiceLocator.Register<IEventBus>(eventBus);

    var inputService = new InputService();
    ServiceLocator.Register<IInputService>(inputService);

    var moveValidation = new MoveValidationService();
    ServiceLocator.Register<IMoveValidationService>(moveValidation);

    var gridData = new GridDataService();
    ServiceLocator.Register<IGridDataService>(gridData);

    var stepTracker = new StepTrackerService();
    ServiceLocator.Register<IStepTrackerService>(stepTracker);

    var puzzleValidation = new PuzzleValidationService(gridData, stepTracker, eventBus);
    ServiceLocator.Register<IPuzzleValidationService>(puzzleValidation);

    var audio = new AudioService(audioInstaller);
    ServiceLocator.Register<IAudioService>(audio);

    var gameService = new GameService();
    ServiceLocator.Register<IGameServices>(gameService);

    var uiBoot = new UIBootStrapService(eventBus);
    ServiceLocator.Register<IUIBootStrap>(uiBoot);

    this.eventBus = eventBus; 
}
    private void SetServiceReferences()
    {
        inputService = ServiceLocator.Get<IInputService>();     
        moveValidationService = ServiceLocator.Get<IMoveValidationService>();
        gridDataService = ServiceLocator.Get<IGridDataService>();
        stepTrackerService = ServiceLocator.Get<IStepTrackerService>();
        puzzleValidationService = ServiceLocator.Get<IPuzzleValidationService>();
        audioService = ServiceLocator.Get<IAudioService>();
        gameServices = ServiceLocator.Get<IGameServices>();
        uiBootStrap = ServiceLocator.Get<IUIBootStrap>();
    }
    private void InitializeServices()
    {
        moveValidationService.Initialize(eventBus);
        stepTrackerService.Initialize(eventBus);
        puzzleValidationService.Initialize(eventBus);
        gridDataService.Initialize(eventBus);
    
        levelLoader.Initialize(eventBus, moveValidationService, gridDataService, gameServices, puzzleValidationService);

        uiBootStrap.Initialize();
    }
}
