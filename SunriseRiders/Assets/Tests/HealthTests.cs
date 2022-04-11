using System.Collections;
using System.Collections.Generic;
using Game.Characters.Props;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class HealthTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void HealthTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator HealthTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [UnityTest]
        public IEnumerator PropHealthTakeZeroDamageTest()
        {
            var prop = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Props/Barrel"));
            var propHealth = prop.GetComponent<PropHealth>();
            propHealth.Awake();
            yield return null;
            var startingHealth = propHealth.CurrentHealth;
            propHealth.TakeDamage(0);
            Assert.IsTrue(startingHealth == propHealth.CurrentHealth);
        }
        
        [UnityTest]
        public IEnumerator PropHealthTakeOneDamageTest()
        {
            var prop = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Props/Barrel"));
            var propHealth = prop.GetComponent<PropHealth>();
            propHealth.Awake();
            yield return null;
            var startingHealth = propHealth.CurrentHealth;
            propHealth.TakeDamage(1);
            Assert.IsTrue(propHealth.CurrentHealth == startingHealth-1);
        }
        
        [UnityTest]
        public IEnumerator PropHealthHealNothingTest()
        {
            var prop = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Props/Barrel"));
            var propHealth = prop.GetComponent<PropHealth>();
            propHealth.Awake();
            yield return null;
            var startingHealth = propHealth.CurrentHealth;
            propHealth.Heal(0);
            Assert.IsTrue(propHealth.CurrentHealth == startingHealth);
        }
        
        [UnityTest]
        public IEnumerator PropHealthHealWhenNotDamagedTest()
        {
            var prop = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Props/Barrel"));
            var propHealth = prop.GetComponent<PropHealth>();
            propHealth.Awake();
            yield return null;
            var startingHealth = propHealth.CurrentHealth;
            propHealth.Heal(1);
            Assert.IsTrue(propHealth.CurrentHealth == startingHealth);
        }
        
        [UnityTest]
        public IEnumerator PropHealthHealWhenDamagedTest()
        {
            var prop = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Props/Barrel"));
            var propHealth = prop.GetComponent<PropHealth>();
            propHealth.Awake();
            yield return null;
            var startingHealth = propHealth.CurrentHealth;
            propHealth.Hurt(1);
            propHealth.Heal(1);
            Assert.IsTrue(propHealth.CurrentHealth == startingHealth);
        }
        
        [UnityTest]
        public IEnumerator PropTakeFullDamageTest()
        {
            var prop = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Props/Barrel"));
            var propHealth = prop.GetComponent<PropHealth>();
            propHealth.Awake();
            yield return null;
            var startingHealth = propHealth.CurrentHealth;
            propHealth.Hurt(startingHealth);
            Assert.IsTrue(!prop.activeInHierarchy);
        }
    }
}
