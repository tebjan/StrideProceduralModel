using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Graphics;
using Stride.Rendering.ProceduralModels;

namespace StrideProceduralModel
{
    [DataContract("MyProceduralModel")]
    [Display("MyModel")] // This name shows up in the procedural model dropdown list
    public class MyProceduralModel : PrimitiveProceduralModelBase
    {
        // A custom property that shows up in Game Studio
        /// <summary>
        /// Gets or sets the size of the model.
        /// </summary>
        /// <value>The size.</value>
        /// <userdoc>The size of the model along the Ox, Oy and Oz axis.</userdoc>
        [DataMember(10)]
        public Vector3 Size { get; set; } = Vector3.One;

        protected override GeometricMeshData<VertexPositionNormalTexture> CreatePrimitiveMeshData()
        {
            // First generate the arrays for vertices and indices with the correct size
            var vertexCount = 4;
            var indexCount = 6;
            var vertices = new VertexPositionNormalTexture[vertexCount];
            var indices = new int[indexCount];

            // Create custom vertices, in this case just a quad facing in Y direction
            var normal = Vector3.UnitZ;
            vertices[0] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0.5f, 0) * Size, normal, new Vector2(0, 0));
            vertices[1] = new VertexPositionNormalTexture(new Vector3(0.5f, 0.5f, 0) * Size, normal, new Vector2(1, 0));
            vertices[2] = new VertexPositionNormalTexture(new Vector3(-0.5f, -0.5f, 0) * Size, normal, new Vector2(0, 1));
            vertices[3] = new VertexPositionNormalTexture(new Vector3(0.5f, -0.5f, 0) * Size, normal, new Vector2(1, 1));

            // Create custom indices
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 1;
            indices[4] = 3;
            indices[5] = 2;

            // Create the primitive object for further processing by the base class
            return new GeometricMeshData<VertexPositionNormalTexture>(vertices, indices, isLeftHanded: false) { Name = "MyModel" };
        }
    }
}