using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private int powerupID;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                // if (powerupID == 0)
                // {
                //     player.TripleShotActive();
                // }
                // else if (powerupID == 1)
                // {
                //     Debug.Log("Collected speed boost");
                // }
                // else if (powerupID == 2)
                // {
                //     Debug.Log("Shields collected");
                // }

                switch (powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Shields collected");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
