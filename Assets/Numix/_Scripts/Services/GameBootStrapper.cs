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
    private IPathHintService pathHintService;
    private IHintService hintService;
    void Awake()
    {
        if (!PlayerPrefs.HasKey(Utility.LEVEL_KEY))
        {
            PlayerPrefs.SetInt(Utility.LEVEL_KEY, 1);
        }
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

        var gameService = new GameService();
        ServiceLocator.Register<IGameServices>(gameService);

        var puzzleValidation = new PuzzleValidationService(gridData, stepTracker, eventBus, gameService);
        ServiceLocator.Register<IPuzzleValidationService>(puzzleValidation);

        var audio = new AudioService(audioInstaller);
        ServiceLocator.Register<IAudioService>(audio);


        var uiBoot = new UIBootStrapService(eventBus, audio);
        ServiceLocator.Register<IUIBootStrap>(uiBoot);

        var pathHintService = new PathHintService();
        ServiceLocator.Register<IPathHintService>(pathHintService);

        var hintService = new HintService();
        ServiceLocator.Register<IHintService>(hintService);

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
        pathHintService = ServiceLocator.Get<IPathHintService>();
        hintService = ServiceLocator.Get<IHintService>();
    }
    private void InitializeServices()
    {
        moveValidationService.Initialize(eventBus);
        stepTrackerService.Initialize(eventBus);
        puzzleValidationService.Initialize(eventBus);
        gridDataService.Initialize(eventBus);
        float savedBGM = PlayerPrefs.GetFloat("BGM", 1f);
        float savedSFX = PlayerPrefs.GetFloat("SFX", 1f);

        audioService.SetBGMVolume(savedBGM);
        audioService.SetSFXVolume(savedSFX);

        audioService.PlayBGM(SoundType.BGM);
        levelLoader.Initialize(eventBus, moveValidationService, gridDataService, gameServices, puzzleValidationService, pathHintService);

        uiBootStrap.Initialize();
    }
}
