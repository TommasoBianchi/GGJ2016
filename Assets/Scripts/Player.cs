﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    public int vita;
    public int danno;
    public int velocità;
    public int velocitàAttacco;
    public float raggio;
    [Range(0, 2)]
    public float rotationSpeed;
    public float attackRate;
    public GameObject bulletPrefab;

    Rigidbody myRigidbody;
    float nextAvailableTimeForAttack;
    Health health;

	public AudioClip antShoot;

	public GameObject gameOverPlane;
	public GameObject playAgainButton;
	public GameObject menuButton;

    void Start ()
    {
        myRigidbody = GetComponent<Rigidbody>();
        nextAvailableTimeForAttack = Time.realtimeSinceStartup;
        health = GetComponent<Health>();
    }

    void Update()
    {
        health.ChangeHeartsNumber(vita);
    }

    public void move (Vector3 movimento)
    {
        Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), rotationSpeed);

        movimento = movimento.normalized;
        movimento = movimento * velocità * Time.deltaTime;
        myRigidbody.MovePosition(transform.position + movimento);

        myRigidbody.MoveRotation(rotation);
    }

    public void attack(InputManager.Side side)
    {
        if (Time.realtimeSinceStartup > nextAvailableTimeForAttack)
        {
			// Instantiate a bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward * 2, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * velocitàAttacco;
            bullet.GetComponent<BulletCollision>().player = gameObject;
            Destroy(bullet, raggio / velocitàAttacco);
            nextAvailableTimeForAttack = Time.realtimeSinceStartup + attackRate;

			// Play sound
			GetComponent<AudioSource>().clip = antShoot;
			GetComponent<AudioSource>().Play();
        }
    }

    public void Hit(int damage)
    {
        vita -= damage;
        if (vita <= 0)
        {
            // Dieeee => GAMEOVER
			gameOver();
        }
        else
            health.ChangeHeartsNumber(vita);
    }

	void gameOver() {
		/*
		// Load new scene
		// make spiders idle and disable spawner
		GetComponent<InputManager>().possibleSpiderCombos.ForEach(sc => sc.spider.movement = Spider.Movement.idle);

		// show gameover plane and buttons
		gameOverPlane.SetActive(true);
		playAgainButton.SetActive (true);
		menuButton.SetActive (true);

		// Destroy player
		Destroy (gameObject);
		*/
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Spider")
        {
            other.gameObject.GetComponent<Spider>().CollisionEnter(gameObject);
        }
    }
}