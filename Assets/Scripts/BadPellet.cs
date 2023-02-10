using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OwenGibson
{
    public class BadPellet : MonoBehaviour
    {
        private float lifespan = 3.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EventManager.BadPelletEaten?.Invoke(1);
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (lifespan > 0)
            {
                lifespan -= Time.deltaTime;
            }
            else Destroy(gameObject);
        }
    }
}