using System;

namespace CannonAttack
{
    public sealed class Cannon
    {
        private static Cannon cannonSingletonInstance;
        static readonly object padlock = new object();
        private readonly int MAXDISTANCE = 20000;

        private Cannon()
        {
            Random r = new Random();
            SetTarget(r.Next(MAXDISTANCE));
        }
        private readonly string CANNONID = "Humano";
        private string CannonID;
        public string ID
        {
            get
            {
                return (string.IsNullOrWhiteSpace(CannonID)) ? CANNONID : CannonID;
            }
            set
            {
                CannonID = value;
            }
        }

        public static readonly int MAXANGLE = 90;
        public static readonly int MINANGLE = 1;
        private static readonly int MAXSPEED = 300000000;
        private int targetDistance;
        public int TargetDistance
        {
            get
            {
                return targetDistance;
            }
            set
            {
                targetDistance = value;
            }
        }

        private readonly int TARGETRADIUS = 50;
        private readonly double GRAVITY = 9.8;

        public static Cannon GetInstance()
        {
            lock (padlock)
            {
                if (cannonSingletonInstance == null)
                {
                    cannonSingletonInstance = new Cannon(); 
                }


            }
            return cannonSingletonInstance;
        }

        public Tuple<bool, string> Shoot(int angle, int speed)
        {
            string msg;
            bool hit;
            int shotDistance = CalculateShotDistance(angle, speed);

            if(angle > MAXANGLE || angle < MINANGLE)
            {
                msg = "Angulo incorreto!";
                hit = false;
            }
            if(speed > MAXSPEED)
            {
                msg = "Velocidade acima do limite";
                hit = false;
            }
            if(shotDistance.WithinRange(this.targetDistance, TARGETRADIUS))
            {
                msg = String.Format("Bala de canhão acertou o alvo em {0}m!", shotDistance);
                hit = true;
            }
            else
            {
                msg = String.Format("Bala de canhão errou o alvo em {0}m!", shotDistance);
                hit = false;
            }
            return Tuple.Create(hit, msg);
        }

        private int CalculateShotDistance(int angle, int speed)
        {
            int time = 0;
            double height = 0;
            double distance = 0;
            // double radiansAngle = (Math.PI / 180) * angle;
            double radiansAngle = (3.1415926536 / 180) * angle;

            while (height >= 0)
            {
                time++;
                distance = speed * Math.Cos(radiansAngle) * time;
                height = (speed * Math.Sin(radiansAngle) * time) - (GRAVITY * Math.Pow(time, 2)) / 2;
            }

            return (int)distance;
        }

        public void SetTarget(int targetDistance)
        {
            this.targetDistance = targetDistance;
        }
    }
}