using FishNet.Example.CustomSyncObject;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace DaanBanaan.UI.Behaviour
{
    public class MainMenuUI : UIBehaviour
    {
        #region [UI Elements]
        private TabView _tabView;
        private Tab _mainMenu;
        private Tab _hostingTab, _joiningTab;

        private Button _hostTabButton, _joinTabButton;

        private Toggle _privacyToggle;
        private TextField _passwordField;
        #endregion

        private void Awake()
        {
            _tabView = RootVisualElement.Q<TabView>();
            _mainMenu = _tabView.GetTab(0);
            _hostingTab = _tabView.GetTab(1);
            _joiningTab = _tabView.GetTab(2);

            _hostTabButton = _mainMenu.Q<Button>("main-menu__host-button");
            _joinTabButton = _mainMenu.Q<Button>("main-menu__join-button");

            _privacyToggle = _joiningTab.Q<Toggle>();

            _passwordField = _joiningTab.Q<TextField>();
            _passwordField.SetEnabled(_privacyToggle.value);
        }

        private void OnEnable()
        {
            _privacyToggle.RegisterValueChangedCallback(OnSessionPrivacyChanged);

            _hostTabButton.clicked += OpenHostingTab;
            _joinTabButton.clicked += OpenJoiningTab;
        }

        private void OnDisable()
        {
            _privacyToggle.UnregisterValueChangedCallback(OnSessionPrivacyChanged);

            _hostTabButton.clicked -= OpenHostingTab;
            _joinTabButton.clicked -= OpenJoiningTab;
        }

        private void OnSessionPrivacyChanged(ChangeEvent<bool> privacy)
        {
            _passwordField.SetEnabled(privacy.newValue);
        }

        #region [Tab Opening Methods]
        private void OpenMainMenu()
        {
            _tabView.activeTab = _mainMenu;
        }
        private void OpenHostingTab()
        {
            _tabView.activeTab = _hostingTab;
        }
        private void OpenJoiningTab()
        {
            _tabView.activeTab = _joiningTab;
        }
        #endregion
    }
}
