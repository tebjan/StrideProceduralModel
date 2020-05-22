using Stride.Engine;

namespace StrideProceduralModel.Windows
{
    class StrideProceduralModelApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
