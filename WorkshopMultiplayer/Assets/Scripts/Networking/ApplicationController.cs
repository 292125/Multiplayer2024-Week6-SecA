using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    [SerializeField] private ClientSingleton clientprefab;
    [SerializeField] private HostSingleton hostprefab;
    private async void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        await LaunchInMode(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
    }

    private async Task LaunchInMode(bool isDedicatedServer)
    {
        if (isDedicatedServer)
        {
            
        }
        else
        {
            ClientSingleton clientSigleton = Instantiate(clientprefab);
            bool authenticated = await clientSigleton.CreateClient();

            HostSingleton hostSingleton = Instantiate(hostprefab);
            hostSingleton.CreateHost();

            if (authenticated)
            {
                clientSigleton.GameManager.GotoMenu();
            }
        }
    }
}
