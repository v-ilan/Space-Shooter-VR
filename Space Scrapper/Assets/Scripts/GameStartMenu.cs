using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameStartMenu : MonoBehaviour
{
    private enum SubMenu { Main, Options, About, None }

    [Header("UI Pages")]
    [SerializeField] private GameObject mainMenuPage;
    [SerializeField] private GameObject optionsPage;
    [SerializeField] private GameObject aboutPage;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private List<Button> returnButtons;

    private void Awake()
    {
        startButton.onClick.AddListener(() => {
            ShowPage(SubMenu.None); // Hide UI for transition
            SceneTransitionUI.Instance.FadeOut(() => {
                SceneLoader.Load(SceneLoader.Scene.MainSpaceshipScene);
            });
        });

        optionsButton.onClick.AddListener(() => ShowPage(SubMenu.Options));
        aboutButton.onClick.AddListener(() => ShowPage(SubMenu.About));
        quitButton.onClick.AddListener(Application.Quit);

        foreach (var btn in returnButtons)
        {
            btn.onClick.AddListener(() => ShowPage(SubMenu.Main));
        }
    }

    private void Start()
    {
        ShowPage(SubMenu.Main);
    }

    private void ShowPage(SubMenu targetPage)
    {
        mainMenuPage.SetActive(targetPage == SubMenu.Main);
        optionsPage.SetActive(targetPage == SubMenu.Options);
        aboutPage.SetActive(targetPage == SubMenu.About);
    }
}
