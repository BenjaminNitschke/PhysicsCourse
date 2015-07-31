using System;
using System.Collections;
using System.Collections.Generic;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Meshes;
using Jitter.Dynamics;
using Jitter.Dynamics.Joints;
using Jitter.LinearMath;

namespace GraphicsEngine.Tests
{
	public class ParticlesPhysics3D : GraphicsApp
	{
		public ParticlesPhysics3D() : base("ParticlesPhysics3D Tests") {}

		public static void Main()
		{
			new ParticlesPhysics3D().Run(RenderMode.Render3D);
		}

		protected override void Init()
		{
			base.Init();
			emitter = new Emitter(0.1f, 5.0f, new Vector3D(0, 0, 3));
		}

		private Emitter emitter;

		protected override void Update()
		{
			base.Update();
			emitter.Update(TimeDeltaInSeconds);
		}
	}

	public class Emitter
	{
		public Emitter(float spawnTime, float maxLifeTime, Vector3D position)
		{
			this.spawnTime = spawnTime;
			this.maxLifeTime = maxLifeTime;
			this.position = position;
			cubeTexture = new Texture("BoxDiffuse.jpg");
			random = new Random((int)DateTime.Now.Ticks);
		}

		private readonly Vector3D position;
		private readonly float spawnTime;
		private readonly float maxLifeTime;
		private float time;
		private Texture cubeTexture;
		private List<Cube> particles = new List<Cube>();
		private Random random;

		public void Update(float timeDeltaInSeconds)
		{
			time += timeDeltaInSeconds;
			if (time > spawnTime)
			{
				time -= spawnTime;
				var newParticle = new Cube(cubeTexture, position);
				newParticle.body.ApplyImpulse(new JVector(
					-0.25f + 0.5f * (float)random.NextDouble(),
					-0.25f + 0.5f * (float)random.NextDouble(), 1)*15);
        particles.Add(newParticle);
			}
			foreach (var particle in particles)
			{
				particle.lifeTime += timeDeltaInSeconds;
				if (particle.lifeTime > maxLifeTime)
				{
					particle.Dispose();
					particles.Remove(particle);
					break;
				}
			}
		}
	}
}