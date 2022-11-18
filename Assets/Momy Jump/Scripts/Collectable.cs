using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MommyJump
{
    public class Collectable : MonoBehaviour
    {
        public int scoreBouns;
        public GameObject explosionEffPb;

        public void Trigger()
        {
            if (explosionEffPb)
                Instantiate(explosionEffPb, transform.position, Quaternion.identity);

            Destroy(gameObject);

            if (GameManager.Ins)
                GameManager.Ins.AddScore(scoreBouns);

            if (AudioController.Ins)
                AudioController.Ins.PlaySound(AudioController.Ins.gotCollectable);
        }
    }
}
