using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SG
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        // 把目标拆成步骤来思考
        // 2. 根据这些值移动角色

        PlayerControls playerControls;

        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

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

            // 场景切换时，执行此逻辑
            SceneManager.activeSceneChanged += OnSceneChange;

            instance.enabled = false;
        }

        private void OnSceneChange(Scene oldScene, Scene newScene)
        {
            // 如果正在加载到世界场景，启用玩家控制
            if (newScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            // 否则我们一定在主菜单，禁用玩家控制
            // 这样即使进入角色创建菜单等界面，玩家也不会到处移动
            else
            {
                instance.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }

        private void Update()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            // 返回绝对值（即不带负号的数，所以始终为正）
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            // 将值限制为 0、0.5 或 1（可选）
            if (moveAmount <= 0.5f && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5f && moveAmount <= 1)
            {
                moveAmount = 1;
            }
        }

        private void OnDestroy()
        {
            // 如果销毁此对象，取消订阅该事件
            SceneManager.activeSceneChanged -= OnSceneChange;
        }
    }
}
