using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _damage;

        public float Speed => _speed;
        public float MaxHealth => _maxHealth;
        public float Damage => _damage;
    }
}