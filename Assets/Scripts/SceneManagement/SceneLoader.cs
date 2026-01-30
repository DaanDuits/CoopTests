using UnityEngine;
using FishNet.Managing.Scened;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Transporting;

namespace DaanBanaan.SceneManagement
{
    public class SceneLoader : NetworkBehaviour
    {
        [SerializeField] private string[] sceneNames;

        public override void OnStartServer()
        {
            base.OnStartServer();

            ServerManager.OnRemoteConnectionState += OnRemoteConnectionState;           
        }

        private void OnRemoteConnectionState(NetworkConnection conn, RemoteConnectionStateArgs args)
        {
            if (args.ConnectionState == RemoteConnectionState.Started)
            {
                int id = conn.ClientId;
                SceneLoadData sld = new SceneLoadData(sceneNames[id]);
                SceneManager.LoadConnectionScenes(conn, sld);
            }
        }
    }
}
