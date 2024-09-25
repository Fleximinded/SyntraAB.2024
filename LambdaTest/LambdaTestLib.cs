using Fleximinded.Core.Parts.CLI;

namespace LambdaTest
{
    public class LambdaTestLib : ICliExecutable
    {
        Random rand = new Random();
        public string Name { get => "LambdaTest"; }
        public string Description { get => "This is a Lambda test"; }

        public bool Execute(ICliRuntime owner, ICliCommand prm)
        {
            switch(prm.Command)
            {
                case "simplelambda":
                    TestSimpleLambda();
                    return true;
                case "capturedlambda":
                    CapturedLambda();
                    return true;

            }
            return false;
        }

        private void TestSimpleLambda()
        {
            int test = rand.Next(4,11);  
            int src = rand.Next(4, 40);
            Func<int, int> multiply = x => x * src;
            for(int i = 0; i < test; i++) {
                src = rand.Next(4, 40);
                System.Console.WriteLine($"The product of {i} x {src}  = {multiply(i)}");
            }
        }
        int SqrFunc(int x,int y)
        {
            return x * y;
        }
        int SqrFunc(int x)
        {
            return x * x;
        }
        void CapturedLambda() {
            int v = 2;
            string sep = "";
            Action[] actions = new Action[3];
            for(int i = 0;i<actions.Length;i++) {   
                actions[i] = () => {
                    System.Console.Write($"{sep}{v+i}");
                };
                sep = ",";
            }
            foreach(Action action in actions) { action(); }
            Console.WriteLine();
        }
    }
}
