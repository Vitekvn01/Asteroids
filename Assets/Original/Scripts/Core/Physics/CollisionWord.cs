using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Original.Scripts.Core.Physics
{
    public class CollisionWord : IFixedTickable
    {
        private readonly List<CustomPhysics> _bodies = new();

        public void Register(CustomPhysics body) => _bodies.Add(body);
        public void Unregister(CustomPhysics body) => _bodies.Remove(body);
    
        public void FixedTick()
        {
            ResolveAll();
        }
        private void ResolveAll()
        {
            for (int i = 0; i < _bodies.Count; i++)
            {
                for (int j = i + 1; j < _bodies.Count; j++)
                {
                    if (_bodies[i].IsActive && _bodies[j].IsActive)
                    {
                        if(CanCollide(_bodies[i], _bodies[j]))
                        {
                            ResolveCollision(_bodies[i], _bodies[j]); 
                        }
                    }
                }
            }
        }

        private void ResolveCollision(CustomPhysics sender, CustomPhysics receiver)
        {
            float dist = Vector2.Distance(sender.Position, receiver.Position);
        
            float minDist = sender.Collider.Radius + receiver.Collider.Radius;
        
            if (dist <= minDist)
            {
                if (sender.Collider.IsTrigger || receiver.Collider.IsTrigger)
                {
                    if (sender.Collider.IsTrigger)
                    {
                        sender.Collider.OnTriggerEnter(receiver.Collider);
                    }
                
                    if (receiver.Collider.IsTrigger)
                    {
                        receiver.Collider.OnTriggerEnter(sender.Collider);
                    }
                }
                else
                {
                    Vector2 delta = sender.Position - receiver.Position;
                    Vector2 normal = delta.normalized;
                    float penetration = minDist - dist;
                
                    sender.Collider.OnCollisionEnter(receiver.Collider);
                    receiver.Collider.OnCollisionEnter(sender.Collider);
                
                
                    Vector2 correction = normal * (penetration / 2f);
                    sender.SetPosition(sender.Position + correction);
                    receiver.SetPosition(receiver.Position - correction);
                
                    sender.SetVelocity(Vector2.Reflect(sender.Velocity, normal) * sender.Bounce);
                    receiver.SetVelocity(Vector2.Reflect(receiver.Velocity, -normal) * receiver.Bounce);
                }
            }

        }
        
        private bool CanCollide(CustomPhysics sender, CustomPhysics receiver)
        {
            var senderCollider = sender.Collider;
            var receiverCollider = receiver.Collider;

            bool senderWantsReceiver = (senderCollider.CollisionMask & receiverCollider.Layer) != 0;
            bool receiverWantsSender = (receiverCollider.CollisionMask & senderCollider.Layer) != 0;

            return senderWantsReceiver || receiverWantsSender;
        }
 
    }
}