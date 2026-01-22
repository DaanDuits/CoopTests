using FishNet.Example.CustomSyncObject;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace DaanBanaan.UI.Behaviour
{
    public class MainMenuUI : UIBehaviour
    {
        private TabView _tabView;
        private Dictionary<string, Tab> _menus = new();

        private TextField _passwordField;

        private void Awake()
        {
            _tabView = RootVisualElement.Q<TabView>();
            _menus.Add("MainMenu", _tabView.GetTab(0));
            _menus.Add("Host", _tabView.GetTab(1));
            _menus.Add("Join", _tabView.GetTab(2));

            Toggle privateToggle = _menus["Host"].Q<Toggle>();
            privateToggle.RegisterValueChangedCallback(OnSessionPrivacyChanged);

            _passwordField = _menus["Host"].Q<TextField>();
            _passwordField.SetEnabled(privateToggle.value);
        }

        private void OnEnable()
        {
            RootVisualElement.Q<Button>("host-button").clicked += () => ChangeTab(_menus["Host"]);
            RootVisualElement.Q<Button>("join-button").clicked += () => ChangeTab(_menus["Join"]);
        }

        private void OnDisable()
        {
            RootVisualElement.Q<Button>("host-button").clicked -= () => ChangeTab(_menus["Host"]);
            RootVisualElement.Q<Button>("join-button").clicked -= () => ChangeTab(_menus["Join"]);
        }

        private void OnSessionPrivacyChanged(ChangeEvent<bool> privacy)
        {
            _passwordField.SetEnabled(privacy.newValue);
        }

        private void ChangeTab(Tab tab)
        {
            _tabView.activeTab = tab;
        }
    }
}
