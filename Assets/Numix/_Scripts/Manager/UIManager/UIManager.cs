using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private float popUpDelay = 1f;
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
    private GamePlayView gamePlayView;

    // 🔥 STACK BASED NAVIGATION
    private Stack<ViewBase> panelStack = new Stack<ViewBase>();
    private ViewBase currentPanel;

    void Awake()
    {
        uiBootStrap = ServiceLocator.Get<IUIBootStrap>();
        presenterList = uiBootStrap.GetPresenterList();
        viewList = uiBootStrap.GetViewsList();
        eventBus = ServiceLocator.Get<IEventBus>();

        CacheViews();

        currentPanel = mainMenuView;
        panelStack.Clear();
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

        foreach (var view in viewList)
        {
            if (view is MainMenuView) mainMenuView = (MainMenuView)view;
            else if (view is SettingView) settingView = (SettingView)view;
            else if (view is GamePauseView) pauseView = (GamePauseView)view;
            else if (view is GameWinView) winView = (GameWinView)view;
            else if (view is GameLostView) loseView = (GameLostView)view;
            else if (view is LevelSelectorView) levelSelectorView = (LevelSelectorView)view;
            else if (view is GamePlayView) gamePlayView = (GamePlayView)view;
        }

        // Default state
        mainMenuView.Show();
        levelSelectorView.Hide();
        settingView.Hide();
        pauseView.Hide();
        winView.Hide();
        loseView.Hide();
        gamePlayView.Hide();
    }

    // 🔥 PUSH PANEL
    private void SwitchPanel(ViewBase targetPanel)
    {
        if (targetPanel == null)
        {
            Debug.LogWarning("Target panel is null!");
            return;
        }

        if (currentPanel == targetPanel)
            return;

        if (currentPanel != null)
        {
            panelStack.Push(currentPanel);
            currentPanel.Hide();
        }

        currentPanel = targetPanel;
        currentPanel.Show();
    }

    // 🔙 POP PANEL
    private void GoBack()
    {
        if (panelStack.Count == 0)
        {
            Debug.Log("No previous panel in stack");
            return;
        }

        if (currentPanel != null)
            currentPanel.Hide();

        currentPanel = panelStack.Pop();
        currentPanel.Show();
    }

    private void SubscribeEvents()
    {
        eventBus.Subscribe<Events.OnGameStarted>(OnGameStarted);
        eventBus.Subscribe<Events.OnSettingButtonClicked>(OnSettingButtonClicked);
        eventBus.Subscribe<Events.OnExitButtonClicked>(OnExitButtonClicked);
        eventBus.Subscribe<Events.OnLoadLevel>(OnLevelLoaded);
        eventBus.Subscribe<Events.OnLevelComplete>(OnLevelComplete);
        eventBus.Subscribe<Events.OnNextLevelLoaded>(OnNextLevelLoaded);
        eventBus.Subscribe<Events.OnGamePaused>(OnPauseButtonClicked);
        eventBus.Subscribe<Events.OnHomeClicked>(ReturnToHome);
        eventBus.Subscribe<Events.OnLevelRestart>(OnLevelReload);
        eventBus.Subscribe<Events.OnLevelFailed>(OnLevelFailed);
    }

    private void UnSubscribeEvents()
    {
        eventBus.Unsubscribe<Events.OnGameStarted>(OnGameStarted);
        eventBus.Unsubscribe<Events.OnSettingButtonClicked>(OnSettingButtonClicked);
        eventBus.Unsubscribe<Events.OnExitButtonClicked>(OnExitButtonClicked);
        eventBus.Unsubscribe<Events.OnLoadLevel>(OnLevelLoaded);
        eventBus.Unsubscribe<Events.OnLevelComplete>(OnLevelComplete);
        eventBus.Unsubscribe<Events.OnNextLevelLoaded>(OnNextLevelLoaded);
        eventBus.Unsubscribe<Events.OnGamePaused>(OnPauseButtonClicked);
        eventBus.Unsubscribe<Events.OnHomeClicked>(ReturnToHome);
        eventBus.Unsubscribe<Events.OnLevelRestart>(OnLevelReload);
        eventBus.Unsubscribe<Events.OnLevelFailed>(OnLevelFailed);
    }

    // 🎮 EVENTS

    private void OnGameStarted(Events.OnGameStarted obj)
    {
        SwitchPanel(levelSelectorView);
    }

    private void OnSettingButtonClicked(Events.OnSettingButtonClicked obj)
    {
        SwitchPanel(settingView);
    }

    private void OnExitButtonClicked(Events.OnExitButtonClicked obj)
    {
        GoBack();
    }

    private void OnLevelLoaded(Events.OnLoadLevel obj)
    {
        SwitchPanel(gamePlayView);
    }

    private void OnLevelComplete(Events.OnLevelComplete obj)
    {
        StartCoroutine(ShowPanelWithDelay(winView, popUpDelay));
    }

    private void OnNextLevelLoaded(Events.OnNextLevelLoaded obj)
    {
        SwitchPanel(gamePlayView);
    }

    private void OnPauseButtonClicked(Events.OnGamePaused obj)
    {
        SwitchPanel(pauseView);
    }

    private void ReturnToHome(Events.OnHomeClicked obj)
    {
        panelStack.Clear();

        if (currentPanel != null)
            currentPanel.Hide();

        currentPanel = mainMenuView;
        currentPanel.Show();
    }
    private void OnLevelReload(Events.OnLevelRestart evt)
    {
        SwitchPanel(gamePlayView);
    }
    private void OnLevelFailed(Events.OnLevelFailed evt)
    {
        StartCoroutine(ShowPanelWithDelay(loseView, popUpDelay));
    }
    private IEnumerator ShowPanelWithDelay(ViewBase panel, float delay)
    {
        yield return new WaitForSeconds(delay);
        SwitchPanel(panel);
    }
}