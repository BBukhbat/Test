using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CharacterController : MonoBehaviour
{
    private Animator _mainAnimation;
    private Transform _mainPar;
    public CinemachineVirtualCamera _aimcam;
    public int _cameraIndex = 9;
    public GameObject magic;
    public GameObject _bulletPrefab;
    public Transform _shootingPoint;
    // Start is called before the first frame update
    void Start()
    {
        _mainAnimation = transform.GetComponent<Animator>();
        _mainPar = transform.parent.GetComponent<Transform>();
        _aimcam.Priority = _cameraIndex;
    }

    private void Update()
    {
        Move();
        _aimcam.Priority = _cameraIndex;

    }
    private void Move()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        transform.parent.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * 0.5f * Time.deltaTime);
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * 2f * Time.deltaTime);
            transform.Rotate(Vector3.down, 0.45f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right);
            transform.Rotate(Vector3.up, 0.45f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            // rb.AddForce(Vector3.forward);
            transform.position += Movement * 2f * Time.deltaTime;

            _mainAnimation.SetBool("Walk", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // rb.AddForce(-Vector3.forward);
            transform.position += Movement * 2f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.RightAlt))
        {

        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D))
        {
            _mainAnimation.SetBool("Walk", true);
            _mainAnimation.SetBool("isBackWalking", false);
            _cameraIndex = 9;
            magic.SetActive(false);
            _mainAnimation.SetBool("Shoot", false);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            _mainAnimation.SetBool("Walk", false);
            _mainAnimation.SetBool("isBackWalking", true);
            _cameraIndex = 9;
            magic.SetActive(false);
            _mainAnimation.SetBool("Shoot", false);

        }
        else if (Input.GetKey(KeyCode.Slash))
        {
            _mainAnimation.SetBool("Standing", true);

            _cameraIndex = 11;
            magic.SetActive(true);
            if (Input.GetKey(KeyCode.Space))
            {
                magic.SetActive(false);
                _mainAnimation.SetBool("Shoot", true);
            }

        }
        else
        {
            _mainAnimation.SetBool("Walk", false);
            _mainAnimation.SetBool("isBackWalking", false);
            _mainAnimation.SetBool("Standing", false);
            _cameraIndex = 9;
            magic.SetActive(false);
            _mainAnimation.SetBool("Shoot", false);

        }
    }
    private void Stop()
    {
        _mainAnimation.SetBool("Walk", false);
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _shootingPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 50f, ForceMode.Impulse);
    }
}
