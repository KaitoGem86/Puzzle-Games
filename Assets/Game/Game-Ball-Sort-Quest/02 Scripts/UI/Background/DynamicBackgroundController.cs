using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public class DynamicBackgroundController : MonoBehaviour
    {
        private Vector3 firstParticlePosition = new Vector3(640, 960, 0);
        [SerializeField] private Image _particlePrefab;
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _maxReachableXAxe;
        private Sprite _particleBackground;
        private Vector3 _direction = new Vector3(1, 1);
        private List<Image> _queueActiveParticle;
        private float _width;
        private float _height;


        public void SetParticleBackground(Sprite particleBackground)
        {
            _particleBackground = particleBackground;
        }

        public void InitParticleBackground()
        {
            _queueActiveParticle = new List<Image>();
            _width = _particlePrefab.rectTransform.rect.width;
            _height = _particlePrefab.rectTransform.rect.height;
            Vector3 pivotTripleGroup = firstParticlePosition;
            for (int i = 0; i < 5; i++)
            {
                var firstParticle = SimplePool.Spawn(_particlePrefab.gameObject, pivotTripleGroup, Quaternion.identity);
                firstParticle.transform.SetParent(transform);
                firstParticle.GetComponent<RectTransform>().localPosition = pivotTripleGroup;
                firstParticle.GetComponent<RectTransform>().localScale = Vector3.one;
                firstParticle.GetComponent<Image>().sprite = _particleBackground;
                //firstParticle.GetComponent<Image>().SetNativeSize();
                _queueActiveParticle.Add(firstParticle.GetComponent<Image>());
                for (int j = 1; j < 7; j++)
                {
                    firstParticle = SimplePool.Spawn(_particlePrefab.gameObject, pivotTripleGroup, Quaternion.identity);
                    firstParticle.transform.SetParent(transform);
                    firstParticle.GetComponent<RectTransform>().localPosition = pivotTripleGroup - j * new Vector3(0, _height, 0);
                    firstParticle.GetComponent<RectTransform>().localScale = Vector3.one;
                    firstParticle.GetComponent<Image>().sprite = _particleBackground;
                    //firstParticle.GetComponent<Image>().SetNativeSize();
                    _queueActiveParticle.Add(firstParticle.GetComponent<Image>());
                }
                pivotTripleGroup = pivotTripleGroup - new Vector3(_width, 0, 0);
            }
            StopAllCoroutines();
            StartCoroutine(PlayEndlessDynamicBackground());
        }

        public System.Collections.IEnumerator PlayEndlessDynamicBackground()
        {
            while (true)
            {
                foreach (var particle in _queueActiveParticle)
                {
                    particle.transform.position += _direction * Time.deltaTime * _speed;
                    // if (particle.transform.position.x > _maxReachableXAxe)
                    // {
                    //     particle.transform.position = new Vector3(firstParticlePosition.x, particle.transform.position.y, particle.transform.position.z);
                    // }
                    if (particle.transform.position.x > _maxReachableXAxe.x || particle.transform.position.y > _maxReachableXAxe.y)
                    {
                        if (Vector3.Distance(particle.transform.position, _maxReachableXAxe) < 1)
                        {
                            particle.transform.position = new Vector3(particle.transform.position.x - 5 * _width, particle.transform.position.y - 7 * _height, firstParticlePosition.z);
                        }
                        else if (particle.transform.position.x > _maxReachableXAxe.x)
                        {
                            particle.transform.position = new Vector3(particle.transform.position.x - 5 * _width, particle.transform.position.y, particle.transform.position.z);
                        }
                        else if (particle.transform.position.y > _maxReachableXAxe.y)
                        {
                            particle.transform.position = new Vector3(particle.transform.position.x, particle.transform.position.y - 7 * _height, particle.transform.position.z);
                        }
                    }
                    yield return new WaitForEndOfFrame();
                    //Do something
                }
            }
        }
    }
}