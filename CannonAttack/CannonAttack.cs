using System;

namespace CannonAttack
{
    class CannonAttack
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao jogo Ataque de Canhão!");
            bool stillPlaying = true;

            while(stillPlaying)
            {
                bool isHit = false;
                Cannon cannon = Cannon.GetInstance();

                while(!isHit)
                {
                    int angle;
                    int speed;

                    Console.WriteLine(String.Format("Distância do alvo: {0}m ", cannon.TargetDistance));
                    GetInputVariable(out angle, out speed);

                    var shot = cannon.Shoot(angle, speed);
                    isHit = shot.Item1;
                    Console.WriteLine(shot.Item2);
                }

                stillPlaying = GetPlayAgain();
            }

            Console.WriteLine("Obrigado por jogar o jogo Ataque de Canhão");
        }

        private static bool GetPlayAgain()
        {
            bool playAgain = false;
            bool validAnswer = false;

            while(!validAnswer)
            {
                Console.WriteLine("Você gostaria de jogar novamente ? (S/N)");
                string playChoice = Console.ReadLine();
                if (playChoice == "s" || playChoice == "S")
                {
                    playAgain = true;
                    validAnswer = true;
                }
                if (playChoice == "n" || playChoice == "N")
                {
                    playAgain = false;
                    validAnswer = true;
                }
            }

            return playAgain;
        }

        private static void GetInputVariable(out int angle, out int speed)
        {
            string angleBuffer;
            string speedBuffer;

            Console.Write(String.Format("Por favor, entre com um ângulo ({0}-{1}): ", Cannon.MINANGLE, Cannon.MAXANGLE));
            angleBuffer = Console.ReadLine();
            if (!int.TryParse(angleBuffer, out angle))
            {
                Console.WriteLine("Valor não numérico, adicionando ângulo padrão de 45°");
                angle = 45;
            }

            Console.Write("Por favor entre com a velocidade em m/s: ");
            speedBuffer = Console.ReadLine();
            if(!int.TryParse(speedBuffer, out speed))
            {
                Console.WriteLine("Valor não numérico, adicionando velocidade padrão de 100m/s");
                speed = 100;
            }
        }
    }
}
