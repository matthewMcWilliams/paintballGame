// vis2k: GUILayout instead of spacey += ...; removed Update hotkeys to avoid
// confusion if someone accidentally presses one.
using UnityEngine;

namespace Mirror
{
    [RequireComponent(typeof(NetworkManager))]
    public class HostClientManageButtons : MonoBehaviour
    {
        NetworkManager _manager;

        void Awake()
        {
            _manager = GetComponent<NetworkManager>();
        }

        void OnGUI()
        {

            // client ready
            if (NetworkClient.isConnected && !NetworkClient.ready)
            {
                if (GUILayout.Button("Client Ready"))
                {
                    NetworkClient.Ready();
                    if (NetworkClient.localPlayer == null)
                    {
                        NetworkClient.AddPlayer();
                    }
                }
            }
        }

        public void Host()
        {
            if (!NetworkClient.active && Application.platform != RuntimePlatform.WebGLPlayer)
            {
                _manager.StartHost();
            }
        }

        public void Client()
        {
            if (!NetworkClient.active)
            {
                _manager.StartClient();

            }
        }

        public void SetIP(string ipAddress)
        {
            if (!NetworkClient.active)
            {
                _manager.networkAddress = ipAddress; 
            }
        }



    }
}