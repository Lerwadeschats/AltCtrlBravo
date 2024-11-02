using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using vector2 = UnityEngine.Vector2;

public class Shower : MonoBehaviour
{
    // Get the Simulation object
    public GameObject Simulation;
    // Get the Base_Particle object from Scene
    public GameObject Base_Particle;
    public Vector2 init_speed = new Vector2(1.0f, 0.0f);
    public float spawn_rate = 1f;
    private float time;
    private float _timer;
    private float _red;
    private float _green;
    private float _blue;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
        _red = 1;
        _green = 1;
        _blue = 1;

    }

    // Update is called once per frame
    void Update()
    {
        // Limit the number of particles
        if (Simulation.transform.childCount < 1000)
        {
            // Spawn particles at a constant rate
            time += Time.deltaTime;
            if (time < 1.0f / spawn_rate)
            {
                return;
            }
            // Create a new particle at the current position of the object
            GameObject new_particle = Instantiate(Base_Particle, transform.position, Quaternion.identity);
            {
                SpriteRenderer sr = new_particle.GetComponent<SpriteRenderer>();
                _timer += Time.deltaTime;
                if (_timer > 3)
                {
                    _red = (1 + Mathf.Sin(Time.time)) / 2f;
                    _green = (1 + Mathf.Sin(Time.time + Mathf.PI / 2f)) / 2f;
                    _blue = (1 + Mathf.Sin(Time.time + Mathf.PI)) / 2f;
                    _timer = 0;
                }
                sr.color = new Color(_red, _green, _blue);
                // update the particle's position
                new_particle.GetComponent<Particle>().pos = transform.position;
                new_particle.GetComponent<Particle>().previous_pos = transform.position;
                new_particle.GetComponent<Particle>().visual_pos = transform.position;
                new_particle.GetComponent<Particle>().vel = init_speed;

                // Set as child of the Simulation object
                new_particle.transform.parent = Simulation.transform;

                // Reset time
                time = 0.0f;
            }
        }
    }
}
