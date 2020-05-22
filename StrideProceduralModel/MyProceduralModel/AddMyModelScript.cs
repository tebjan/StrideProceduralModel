using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Rendering;
using System.Threading.Tasks;

namespace StrideProceduralModel
{
    public class AddMyModelScript : AsyncScript
    {
        // Declared public member fields and properties will show in the game studio
        public float RotationSpeed { get; set; } = 1;

        public override async Task Execute()
        {
            // Setup the custom model
            await CreateMyModel();

            while (Game.IsRunning)
            {
                // Do stuff every new frame
                Entity.Transform.Rotation *= Quaternion.RotationY(MathUtil.DegreesToRadians(RotationSpeed));

                await Script.NextFrame();
            }
        }

        async Task CreateMyModel()
        {
            // The model classes
            var myModel = new MyProceduralModel();
            var model = new Model();
            var modelComponent = new ModelComponent(model);

            // Generate the procedual model
            myModel.Generate(Services, model);

            // Add a meterial
            var material = Content.Load<Material>("MyModel Material");
            model.Add(material);

            // Add everything to the entity
            Entity.Add(modelComponent);
        }
    }
}
