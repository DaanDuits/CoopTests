using UnityEngine;

namespace DaanBanaan.Networking
{ 
    public delegate void HostSessionEventHandler();
    public delegate void JoinSessionEventHandler(string address);
    public delegate void SessionErrorEventHandler(SessionEventsSO.SessionError error);
    [CreateAssetMenu(fileName = "NewSessionEvents", menuName = "DaanBanaan/Events/SessionEvents")]
    public class SessionEventsSO : ScriptableObject
    {
        public event HostSessionEventHandler HostSessionEvent;
        public event JoinSessionEventHandler JoinSessionEvent;
        public event SessionErrorEventHandler SessionErrorEvent;

        public enum SessionError
        {
            HostError,
            JoinError
        }

        public void OnHostSession()
        {
            HostSessionEvent?.Invoke();
        }
        public void OnJoinSession(string address)
        {
            JoinSessionEvent?.Invoke(address);
        }
        public void OnSessionError(SessionError error)
        {
            SessionErrorEvent?.Invoke(error);
        }
    }
}
