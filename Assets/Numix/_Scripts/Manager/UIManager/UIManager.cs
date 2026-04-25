using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private IUIBootStrap uiBootStrap;
    private List<IPresenter> presenterList;
    private List<ViewBase> viewList;
    private IEventBus eventBus;

    private MainMenuView mainMenuView;
    private SettingView settingView;
    private GamePauseView pauseView;
    private GameWinView winView;
    private GameLostView loseView;
    private LevelSelectorView levelSelectorView;
    private ViewBase previousPanel;
    private ViewBase currentPanel;
    void Awake()
    {
        uiBootStrap = ServiceLocator.Get<IUIBootStrap>();
        presenterList = uiBootStrap.GetPresenterList();
        viewList = uiBootStrap.GetViewsList();
        eventBus = ServiceLocator.Get<IEventBus>();
        previousPanel = mainMenuView;
        currentPanel = mainMenuView;
        CacheViews();
    }
    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnSubscribeEvents();
    }



    private void CacheViews()
    {
        Debug.Log("Caching Views in UIManager");
        foreach (var view in viewList)
        {
            if (view is MainMenuView) mainMenuView = (MainMenuView)view;
            if (view is SettingView) settingView = (SettingView)view;
            if (view is GamePauseView) pauseView = (GamePauseView)view;
            if (view is GameWinView) winView = (GameWinView)view;
            if (view is GameLostView) loseView = (GameLostView)view;
            if (view is LevelSelectorView) levelSelectorView = (LevelSelectorView)view;
        }
        levelSelectorView.Hide();
        settingView.Hide();
        pauseView.Hide();
        winView.Hide();
        loseView.Hide();
    }
    private void SubscribeEvents()
    {
        eventBus.Subscribe<Events.OnGameStarted>(OnGameStarted);
        eventBus.Subscribe<Events.OnSettingButtonClicked>(OnSettingButtonClicked);
        eventBus.Subscribe<Events.OnExitButtonClicked>(OnExitButtonClicked);
        eventBus.Subscribe<Events.OnLoadLevel>(OnLevelLoaded);
        eventBus.Subscribe<Events.OnLevelComplete>(OnLevelComplete);
    }
    private void UnSubscribeEvents()
    {
        eventBus.Unsubscribe<Events.OnGameStarted>(OnGameStarted);
        eventBus.Unsubscribe<Events.OnSettingButtonClicked>(OnSettingButtonClicked);
        eventBus.Unsubscribe<Events.OnExitButtonClicked>(OnExitButtonClicked);
        eventBus.Unsubscribe<Events.OnLoadLevel>(OnLevelLoaded);
        eventBus.Unsubscribe<Events.OnLevelComplete>(OnLevelComplete);
    }
    private void OnGameStarted(Events.OnGameStarted obj)
    {
        mainMenuView.Hide();
        levelSelectorView.Show();
        settingView.Hide();
        pauseView.Hide();
        winView.Hide();
        loseView.Hide();
    }
    private void OnSettingButtonClicked(Events.OnSettingButtonClicked obj)
    {
        previousPanel = mainMenuView;
        currentPanel = settingView;
        mainMenuView.Hide();
        settingView.Show();
    }
    private void OnExitButtonClicked(Events.OnExitButtonClicked obj)
    {
        settingView.Hide();
        previousPanel.Show();
        previousPanel = settingView;
        currentPanel = mainMenuView;
    }
    private void OnLevelLoaded(Events.OnLoadLevel obj)
    {
        levelSelectorView.Hide();
        mainMenuView.Hide();
    }
    private void OnLevelComplete(Events.OnLevelComplete obj)
    {
        winView.Show();
    }
}
