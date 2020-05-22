using Stride.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrideProceduralModel
{
    public static class ModelExtensions
    {
        /// <summary>
        /// Disposes all GPU buffers of a <see cref="Model"/>. Make sure it isn't used by the engine before calling this method.
        /// </summary>
        /// <param name="model">The model.</param>
        public static void DisposeBuffers(this Model model)
        {
            if (model != null)
            {
                foreach (var mesh in model.Meshes)
                {
                    mesh.Draw.DisposeBuffers();
                }
            }
        }

        /// <summary>
        /// Disposes all GPU buffers of a <see cref="MeshDraw"/>. Make sure it isn't used by the engine before calling this method.
        /// </summary>
        /// <param name="meshDraw">The mesh draw.</param>
        private static void DisposeBuffers(this MeshDraw meshDraw)
        {
            if (meshDraw != null)
            {
                meshDraw.IndexBuffer?.Buffer?.Dispose();

                if (meshDraw.VertexBuffers != null)
                {
                    foreach (var vb in meshDraw.VertexBuffers)
                    {
                        vb.Buffer?.Dispose();
                    }
                }
            }
        }
    }
}
