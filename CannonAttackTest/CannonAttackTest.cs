using Microsoft.VisualStudio.TestTools.UnitTesting;
using CannonAttack;

namespace CannonAttackTest
{
    [TestClass]
    public class CannonAttackTest
    {
        private static Cannon cannon;

        [ClassInitialize()]
        public static void CannonTestsInitialize(TestContext testContext)
        {
            cannon = Cannon.GetInstance();
        }

        [TestMethod]
        public void TestCannonIDValid()
        {
            Assert.IsNotNull(cannon.ID);
        }
        [TestMethod]
        public void TestCannonMultipliInstances()
        {
            Cannon cannon2 = Cannon.GetInstance();
            Assert.IsTrue(cannon == cannon2);
        }

        [TestMethod]

        public void TestCannonShootIncorrectAngle()
        {
            var shot = cannon.Shoot(95, 100);
            Assert.IsFalse(shot.Item1);
        }

        [TestMethod]
        public void TestCannonShootIncorrectSpeed()
        {
            var shot = cannon.Shoot(45, 300000001);
            Assert.IsFalse(shot.Item1 );
        }
        [TestMethod]
        public void TestCannonShootMiss()
        {
            cannon.SetTarget(4000);
            var shot = cannon.Shoot(45, 350);
            Assert.IsTrue(shot.Item2 == "Bala de canhão errou o alvo em 12621m!");
        }
        [TestMethod]
        public void TestCannonShootHit()
        {
            cannon.SetTarget(12621);
            var shot = cannon.Shoot(45, 350);
            Assert.IsTrue(shot.Item2 == "Bala de canhão acertou o alvo em 12621m!");
        }
    }
}
