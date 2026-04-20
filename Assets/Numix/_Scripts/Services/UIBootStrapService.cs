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
    private MainMenuPresenter mainMenuPresenter;
    private SettingPresenter settingPresenter;
    private GameWinPresenter gameWinPresenter;
    private GameLostPresenter gameLostPresenter;
    private GamePausePresenter gamePausePresenter;
    private List<ViewBase> viewBaseList = new List<ViewBase>();
    private List<IPresenter> presenterBaseList = new List<IPresenter>();

    public UIBootStrapService(IEventBus eventBus)
    {
        this.eventBus = eventBus;
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
    }
    private void InstantiatePresenters()
    {
        mainMenuPresenter = new MainMenuPresenter(mainMenuView, eventBus);
        settingPresenter = new SettingPresenter(settingView, eventBus);
        gameWinPresenter = new GameWinPresenter(gameWinView, eventBus);
        gameLostPresenter = new GameLostPresenter(gameLostView, eventBus);
        gamePausePresenter = new GamePausePresenter(gamePauseView, eventBus);
    }
    private void InitializePresenters()
    {
        mainMenuPresenter.Initialize();
        settingPresenter.Initialize();
        gameWinPresenter.Initialize();
        gameLostPresenter.Initialize();
        gamePausePresenter.Initialize();
    }
    private void AddViewsToList()
    {
        viewBaseList.Add(mainMenuView);
        viewBaseList.Add(settingView);
        viewBaseList.Add(gameLostView);
        viewBaseList.Add(gameWinView);
        viewBaseList.Add(gamePauseView);
    }
    private void AddPresenterToList()
    {
        presenterBaseList.Add(mainMenuPresenter);
        presenterBaseList.Add(settingPresenter);
        presenterBaseList.Add(gameLostPresenter);
        presenterBaseList.Add(gameWinPresenter);
        presenterBaseList.Add(gamePausePresenter);
    }

}
