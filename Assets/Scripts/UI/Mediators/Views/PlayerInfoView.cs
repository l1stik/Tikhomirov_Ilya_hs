using UnityEngine;
using UnityEngine.UI;

namespace UI.Mediators.Views
{
    public class PlayerInfoView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        public void SetHealth(float value)
        {
            _healthSlider.value = value;
        }
        
        public void SetMaxHealth(float value)
        {
            _healthSlider.maxValue = value;
        }
    }
}