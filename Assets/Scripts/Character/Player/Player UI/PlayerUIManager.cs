using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SG
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;

        [Header("NETWORK JOIN")]
        [SerializeField] bool startGameAsClient;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false;
                // 必须先把当前网络会话关闭，因为在标题界面我们已经以 Host 身份启动了
                NetworkManager.Singleton.Shutdown();
                // 然后再以 Client 身份重新连接
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
