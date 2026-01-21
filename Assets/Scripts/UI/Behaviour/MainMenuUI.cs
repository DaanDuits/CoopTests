using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DaanBanaan.UI.Behaviour
{
    public class MainMenuUI : UIBehaviour
    {
        private TabView _tabView;
        private Dictionary<string, Tab> _menus = new();

        private void Awake()
        {
            _tabView = RootVisualElement.Q<TabView>();
            _menus.Add("MainMenu", _tabView.GetTab(0));
            _menus.Add("Host", _tabView.GetTab(1));
        }

        private void OnEnable()
        {
            RootVisualElement.Q<Button>("host-button").clicked += () => ChangeTab(_menus["Host"]);
            RootVisualElement.Q<Button>("join-button");
        }

        private void ChangeTab(Tab tab)
        {
            _tabView.activeTab = tab;
        }
    }
}
