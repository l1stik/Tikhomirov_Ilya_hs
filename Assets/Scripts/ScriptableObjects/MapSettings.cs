using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class MapSettings : ScriptableObject
    {
        [SerializeField] private int _numberPatrolPoints;
        [SerializeField] private float _minDistancePatrolPoints;
        
        [SerializeField] private Vector3 _startMapPoint;
        [SerializeField] private Vector3 _endMapPoint;

        public int NumberPatrolPoints => _numberPatrolPoints;
        public float MinDistancePatrolPoints => _minDistancePatrolPoints;
        
        public Vector3 StartMapPoint => _startMapPoint;
        public Vector3 EndMapPoint => _endMapPoint;
    }
}