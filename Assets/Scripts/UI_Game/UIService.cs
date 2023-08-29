using UnityEngine;
using BattleTank.Generics;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace BattleTank.UI
{
    public class UIService : Singleton<UIService>
    {
        [SerializeField] private List<UIPage> Pages;
        private UIPage CurrentPage;

        private void Start()
        {
            CurrentPage = Pages.Find(newPage => newPage.pageName == PageName.MainMenu);
            CurrentPage.gameObject.SetActive(true);
            EventSystem.EventService.Instance.OnGameOver += EndGamePage;
        }

        private void EndGamePage()
        {
            Switch(PageName.ResultScreen);
        }

        public void Switch(PageName nextPageName)
        {
            if (CurrentPage != null)
                CurrentPage.gameObject.SetActive(false);

            CurrentPage = Pages.Find(newPage => newPage.pageName == nextPageName);
        }

    }
}