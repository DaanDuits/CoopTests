using UnityEngine;
using UnityEngine.UIElements;
using DaanBanaan.Networking;
using UnityEngine.InputSystem;

namespace DaanBanaan.UI.Behaviour
{
    public class MainMenuUI : UIBehaviour
    {
        [SerializeField] private SessionEventsSO sessionEvents;

        #region [UI Elements]
        private TabView _tabView;
        private Tab _mainMenu;
        private Tab _hostingTab, _joiningTab;

        private Button _hostTabButton, _joinTabButton;

        private Toggle _privacyToggle;
        private TextField _passwordField;

        private Button _hostSessionButton;

        private TextField _ipField;

        private Button _joinSessionButton;
        #endregion
        
        private void Awake()
        {
            _tabView = RootVisualElement.Q<TabView>();
            _mainMenu = _tabView.GetTab(0);
            _hostingTab = _tabView.GetTab(1);
            _joiningTab = _tabView.GetTab(2);

            _hostTabButton = _mainMenu.Q<Button>("main-menu__host-button");
            _joinTabButton = _mainMenu.Q<Button>("main-menu__join-button");

            _privacyToggle = _hostingTab.Q<Toggle>();

            _passwordField = _hostingTab.Q<TextField>();
            _passwordField.SetEnabled(_privacyToggle.value);

            _hostSessionButton = _hostingTab.Q<Button>("host-tab__host-button");

            _ipField = _joiningTab.Q<TextField>();

            _joinSessionButton = _joiningTab.Q<Button>("join-tab__join-button");
        }

        private void OnEnable()
        {
            _privacyToggle.RegisterValueChangedCallback(OnSessionPrivacyChanged);

            _hostTabButton.clicked += OpenHostingTab;
            _joinTabButton.clicked += OpenJoiningTab;

            _hostSessionButton.clicked += HostSession;
            _joinSessionButton.clicked += JoinSession;

            sessionEvents.SessionErrorEvent += OnSessionError;
        }

        private void OnDisable()
        {
            _privacyToggle.UnregisterValueChangedCallback(OnSessionPrivacyChanged);

            _hostTabButton.clicked -= OpenHostingTab;
            _joinTabButton.clicked -= OpenJoiningTab;

            _hostSessionButton.clicked -= HostSession;
            _joinSessionButton.clicked -= JoinSession;

            sessionEvents.SessionErrorEvent -= OnSessionError;
        }

        private void OnSessionPrivacyChanged(ChangeEvent<bool> privacy)
        {
            _passwordField.SetEnabled(privacy.newValue);
        }

        private void HostSession()
        {
            sessionEvents.OnHostSession();
        }
        private void JoinSession()
        {
            sessionEvents.OnJoinSession(_ipField.value);
        }

        private void OnSessionError(SessionEventsSO.SessionError error)
        {
            Debug.LogError("A Session Error Occured!");
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
