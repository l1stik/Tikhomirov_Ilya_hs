using Core.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Mediators.HUD
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        
        [SerializeField] private Button _idleButton;
        [SerializeField] private Button _toBaseButton;
        [SerializeField] private Button _patrolButton;
        
        public void StartGame()
        {
            _playerController.OnPlayerDied.AddListener(Unsubscribe);
            
            _idleButton.onClick.AddListener(_playerController.Idle);
            _toBaseButton.onClick.AddListener(_playerController.ToBase);
            _patrolButton.onClick.AddListener(_playerController.Patrolling);
        }

        private void Unsubscribe()
        {
            _playerController.OnPlayerDied.RemoveListener(Unsubscribe);

            _idleButton.onClick.RemoveListener(_playerController.Idle);
            _toBaseButton.onClick.RemoveListener(_playerController.ToBase);
            _patrolButton.onClick.RemoveListener(_playerController.Patrolling);
        }
    }
}