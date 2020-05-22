using Stride.Core.Diagnostics;
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

        MyProceduralModel myModel;
        ModelComponent modelComponent;
        Material material;

        ProfilingKey UpdateModelProfilingKey = new ProfilingKey("Update My Model");

        public override async Task Execute()
        {
            // Setup the custom model
            await CreateMyModel();

            while (Game.IsRunning)
            {
                // Do stuff every new frame
                Entity.Transform.Rotation *= Quaternion.RotationY(MathUtil.DegreesToRadians(RotationSpeed));

                await Script.NextFrame();

                using (Profiler.Begin(UpdateModelProfilingKey))
                {
                    UpdateMyModel(); 
                }
            }
        }

        async Task CreateMyModel()
        {
            await Task.Run(() =>
            {
                // The model classes
                myModel = new MyProceduralModel();
                modelComponent = new ModelComponent(new Model());

                // Generate the procedual model
                myModel.Generate(Services, modelComponent.Model);

                // Add a meterial
                material = Content.Load<Material>("MyModel Material");
                modelComponent.Model.Add(material);

                // Add everything to the entity
                Entity.Add(modelComponent);
            });
        }

        void UpdateMyModel()
        {
            // Dispose previous vertex and index buffer
            modelComponent.Model.DisposeBuffers();

            // Create new model, so the entity processors pick it up
            modelComponent.Model = new Model();

            // Don't forget the material
            modelComponent.Model.Add(material);

            // Re-generate the procedual model
            myModel.Generate(Services, modelComponent.Model);
        }
    }
}
