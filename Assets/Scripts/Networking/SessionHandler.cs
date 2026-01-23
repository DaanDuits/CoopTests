using FishNet.Managing;
using UnityEngine;

namespace DaanBanaan.Networking
{
    [RequireComponent(typeof(NetworkManager))]
    public class SessionHandler : MonoBehaviour
    {
        [SerializeField] private SessionEventsSO sessionEvents;

        private NetworkManager _networkManager;

        private void Awake()
        {
            _networkManager = GetComponent<NetworkManager>();
        }

        private void OnEnable()
        {
            sessionEvents.HostSessionEvent += HostSession;
            sessionEvents.JoinSessionEvent += JoinSession;
        }

        private void OnDisable()
        {
            sessionEvents.HostSessionEvent -= HostSession;
            sessionEvents.JoinSessionEvent -= JoinSession;
        }

        private void HostSession()
        {
            if (!_networkManager.ServerManager.StartConnection())
                sessionEvents.OnSessionError(SessionEventsSO.SessionError.HostError);
            if (!_networkManager.ClientManager.StartConnection())
                sessionEvents.OnSessionError(SessionEventsSO.SessionError.HostError);
        }

        private void JoinSession(string address)
        {
            if (_networkManager.ClientManager.StartConnection(address))
                sessionEvents.OnSessionError(SessionEventsSO.SessionError.JoinError);
        }
    }
}
