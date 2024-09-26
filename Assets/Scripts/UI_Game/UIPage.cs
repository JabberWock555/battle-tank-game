using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using BattleTank.EventSystem;

namespace BattleTank.UI {
    [Serializable]
    public class UIPage : MonoBehaviour
    {
        public PageName pageName;
        [SerializeField] private List<UIButton> Buttons;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            foreach (UIButton i in Buttons)
            {
                i.Button.onClick.AddListener(() => SwitchPage(i));
            }
        }

        private void SwitchPage(UIButton button)
        {
            if (button.ButtonName == ButtonName.Quit)
            {
                Application.Quit();
                return;
            }
            else if (button.ButtonName == ButtonName.Play)
            {
                EventService.Instance.InvokeGameStart();
            }

            UIService.Instance.Switch(button.MoveTo);
        }
    }

    [Serializable]
    public class UIButton
    {
        public Button Button;
        public ButtonName ButtonName;
        public PageName MoveTo;
    }

    public enum PageName
    {
        None,
        MainMenu,
        Settings,
        PauseMenu,
        ResultScreen
    }

    public enum ButtonName
    {
        None,
        Play,
        Pause,
        Settings,
        Back,
        Quit,
        Exit,
        TryAgain
    } }