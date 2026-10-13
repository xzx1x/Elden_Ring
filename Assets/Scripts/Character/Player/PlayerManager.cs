using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        PlayerLocomotionManager playerLocomotionManager;

        public CharacterController characterController;

        protected override void Awake()
        {
            base.Awake();

            // 只做玩家专属的事情
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            characterController = GetComponent<CharacterController>();
        }

        protected override void Update()
        {
            base.Update();

            // 处理移动
            playerLocomotionManager.HandleAllMovement();
        }
    }
}
