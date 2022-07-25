using System;
using System.Collections;
using Core.Map;
using ScriptableObjects;
using UI.Mediators.Views;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Core.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private PlayerInfoView _playerInfoView;
        [SerializeField] private MapController _mapController;
        [SerializeField] private Animator _animator;
        
        private IEnumerator _coroutine;
        
        private bool _playerDied;
        private float _health;
        
        private static readonly int Play = Animator.StringToHash("Play");
        private static readonly int Stop = Animator.StringToHash("Stop");
        
        public UnityEvent OnPlayerDied { get; } = new UnityEvent();

        public void StartGame()
        {
            PlayAnimation();
            
            _playerInfoView.SetHealth(_playerSettings.MaxHealth);
            _playerInfoView.SetMaxHealth(_playerSettings.MaxHealth);

            _health = _playerSettings.MaxHealth;
            _playerDied = false;
            
            transform.position = _mapController.BasePoint;
        }

        public void Idle()
        {
            StopAllActivity();
            PlayAnimation();
        }

        public void Patrolling()
        {
            StopAllActivity();
            
            var nextPatrolPoint = _mapController.PatrolPoints[Random.Range(0, _mapController.PatrolPoints.Count)];
            while (transform.position == nextPatrolPoint)
            {
                nextPatrolPoint = _mapController.PatrolPoints[Random.Range(0, _mapController.PatrolPoints.Count)];
            }
            
            _coroutine = GoToCoroutine(nextPatrolPoint, Patrolling);
            StartCoroutine(_coroutine);
        }
        
        public void ToBase()
        {
            StopAllActivity();
            
            _coroutine = GoToCoroutine(_mapController.BasePoint, PlayAnimation);
            StartCoroutine(_coroutine);
        }

        private IEnumerator GoToCoroutine(Vector3 endPosition, Action then = null)
        {
            var time = 0f;
            
            while(time < 1)
            {
                time += Time.deltaTime / _playerSettings.Speed;
                transform.position = Vector3.Lerp(transform.position, endPosition, time);
                
                yield return null;
            }
            
            _coroutine = null;
            then?.Invoke();
        }
        
        private void PlayAnimation()
        {
            _animator.SetTrigger(Play);
        }
        
        private void StopAnimation()
        {
            _animator.SetTrigger(Stop);
        }
        
        private void OnMouseDown()
        {
            ClickObjectListener();
        }

        private void ClickObjectListener()
        {
            if (_playerDied)
            {
                return;
            }
            
            _health -= _playerSettings.Damage;
            _playerInfoView.SetHealth(_health);
            
            if (_health <= 0)
            {
                _playerDied = true;
                OnPlayerDied?.Invoke();
                
                StopAllActivity();
            }
        }

        private void StopAllActivity()
        {
            StopAnimation();
            
            if (_coroutine != null)
            {
                StopAllCoroutines();
                _coroutine = null;
            }
        }
    }
}