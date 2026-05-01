using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBootStrapService : IUIBootStrap
{
    private IEventBus eventBus;
    private MainMenuView mainMenuView;
    private SettingView settingView;
    private GameWinView gameWinView;
    private GameLostView gameLostView;
    private GamePauseView gamePauseView;
    private LevelSelectorView levelSelectorView;
    private GamePlayView gamePlayView;
    private InfoPanelView infoPanelView;
    private MainMenuPresenter mainMenuPresenter;
    private SettingPresenter settingPresenter;
    private GameWinPresenter gameWinPresenter;
    private GameLostPresenter gameLostPresenter;
    private GamePausePresenter gamePausePresenter;
    private LevelSelectorPresenter levelSelectorPresenter;
    private InfoPanelPresenter infoPanelPresenter;
    private GamePlayPresenter gamePlayPresenter;
    private IAudioService audioService;
    private List<ViewBase> viewBaseList = new List<ViewBase>();
    private List<IPresenter> presenterBaseList = new List<IPresenter>();

    public UIBootStrapService(IEventBus eventBus, IAudioService audioService)
    {
        this.eventBus = eventBus;
        this.audioService = audioService;
    }
    public void Initialize()
    {
        InstantiateViews();
        InstantiatePresenters();
        InitializePresenters();
        AddViewsToList();
        AddPresenterToList();
    }

    public List<IPresenter> GetPresenterList()
    {
        return presenterBaseList;
    }

    public List<ViewBase> GetViewsList()
    {
        return viewBaseList;
    }
    private void InstantiateViews()
    {
        mainMenuView = new MainMenuView();
        settingView = new SettingView();
        gameWinView = new GameWinView();
        gameLostView = new GameLostView();
        gamePauseView = new GamePauseView();
        levelSelectorView = new LevelSelectorView();
        gamePlayView = new GamePlayView();
        infoPanelView = new InfoPanelView();
    }
    private void InstantiatePresenters()
    {
        mainMenuPresenter = new MainMenuPresenter(mainMenuView, eventBus, audioService);
        settingPresenter = new SettingPresenter(settingView, eventBus, audioService);
        gameWinPresenter = new GameWinPresenter(gameWinView, eventBus, audioService);
        gameLostPresenter = new GameLostPresenter(gameLostView, eventBus, audioService);
        gamePausePresenter = new GamePausePresenter(gamePauseView, eventBus, audioService);
        levelSelectorPresenter = new LevelSelectorPresenter(levelSelectorView, eventBus, audioService);
        gamePlayPresenter = new GamePlayPresenter(gamePlayView, eventBus, audioService);
        infoPanelPresenter = new InfoPanelPresenter(infoPanelView, eventBus, audioService);
    }
    private void InitializePresenters()
    {
        mainMenuPresenter.Initialize();
        settingPresenter.Initialize();
        gameWinPresenter.Initialize();
        gameLostPresenter.Initialize();
        gamePausePresenter.Initialize();
        levelSelectorPresenter.Initialize();
        gamePlayPresenter.Initialize();
        infoPanelPresenter.Initialize();
    }
    private void AddViewsToList()
    {
        viewBaseList.Add(mainMenuView);
        viewBaseList.Add(settingView);
        viewBaseList.Add(gameLostView);
        viewBaseList.Add(gameWinView);
        viewBaseList.Add(gamePauseView);
        viewBaseList.Add(levelSelectorView);
        viewBaseList.Add(gamePlayView);
        viewBaseList.Add(infoPanelView);
    }
    private void AddPresenterToList()
    {
        presenterBaseList.Add(mainMenuPresenter);
        presenterBaseList.Add(settingPresenter);
        presenterBaseList.Add(gameLostPresenter);
        presenterBaseList.Add(gameWinPresenter);
        presenterBaseList.Add(gamePausePresenter);
        presenterBaseList.Add(levelSelectorPresenter);
        presenterBaseList.Add(gamePlayPresenter);
        presenterBaseList.Add(infoPanelPresenter);
    }

}
