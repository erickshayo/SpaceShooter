using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    // data typr (int , float, bool, string)
    public float speed = 5f;
    private float speedMultiplier = 2f;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.15f;
    private float canFire = -1f;
    [SerializeField]
    private int lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject shieldVisualizer;
    // Start is called before the first frame update
    void Start()
    {
        // take the current position = new position
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        // _spawnManager.OnPlayerDeath();

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            FireLaser();
        }


    }

    void CalculateMovement()

    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //new Vector3(-5, 0, 0) * 5 * realtime
        // transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        // transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.y > 6.5)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }
        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x, 6.5f, 0);
        }

        // the above can also use the mathf.clamp(x,y,z)


        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }

    }

    void FireLaser()
    {

        canFire = Time.time + fireRate;

        if (_isTripleShotActive == true)
        {
            //instantiate for the tripple shot
            Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);

        }
        else
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

        }


    }

    public void Damage()
    {
        //if shield is active
        //do nothing
        //deactivate shields
        //return
        if(_isShieldActive == true){
            _isShieldActive = false;
            //disable the visualizer
            shieldVisualizer.SetActive(false);
            return;
        }
        lives--;

        if (lives < 1)
        {
            //Communicate with Spawn Manager
            _spawnManager.OnPlayerDeath();

            //Let them know to stop spawning
            Destroy(this.gameObject);
        }

    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());

    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        speed /= speedMultiplier;
    }

    public void ShieldsActive()
    {
        _isShieldActive = true;
        shieldVisualizer.SetActive(true);
    }



}
