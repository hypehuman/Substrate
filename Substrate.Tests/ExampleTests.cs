using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Substrate.Tests
{
    [TestClass]
    public class ExampleTests
    {
        [TestMethod]
        /// <summary>
        /// Doesn't test the output yet; just makes sure there are no exceptions.
        /// </summary>
        public void TestExample()
        {
            Replace(@"C:\Users\hypehuman\Documents\Dev Libraries\GitHub\minecraft-dotnet\Substrate\Substrate.Tests\Data\GoldTrees", 0, 1);
        }

        public static void Replace(string pathToWorld, int oldid, int newid)
        {
            NbtWorld world = NbtWorld.Open(pathToWorld);
            var chunkManager = world.GetChunkManager();
            int numChunks = chunkManager.Count();
            System.Diagnostics.Debug.WriteLine("Num chunks: " + numChunks);
            foreach (ChunkRef chunk in chunkManager)
            {
                // Process Chunk
                for (int y = 0; y <= 127; y++)
                {
                    for (int x = 0; x <= 15; x++)
                    {
                        for (int z = 0; z <= 15; z++)
                        {
                            // Attempt to replace block
                            int oldBlock = chunk.Blocks.GetID(x, y, z);
                            if (oldBlock == oldid)
                            {
                                chunk.Blocks.SetID(x, y, z, newid);
                                chunk.Blocks.SetData(x, y, z, 0);
                                // TileEntity consistency is implicitly maintained
                            }
                        }
                    }
                }

                // Save after each chunk so we can release unneeded chunks back to the system
                chunkManager.Save();
            }
        }
    }
}
