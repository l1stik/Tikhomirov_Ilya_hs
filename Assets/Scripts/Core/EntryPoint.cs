using Core.Map;
using Core.Player;
using UI.Mediators.HUD;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private MapController _mapController;
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _goMenuButton;
        [SerializeField] private GameObject _goMenuCanvas;

        private void Start()
        {
            _startGameButton.onClick.AddListener(() =>
            {
                _hud.StartGame();
                _mapController.StartGame();
                _playerController.StartGame();
                _goMenuCanvas.SetActive(false);
            });
        
            _goMenuButton.onClick.AddListener(() =>
            {
                _goMenuCanvas.SetActive(true);
            });
        }
    }
}