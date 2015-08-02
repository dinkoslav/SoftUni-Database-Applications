using System.Linq;

namespace _1.EntityFrameworkMappingsDBFirst
{
    using System;

    class EntityFrameworkMappings
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();
            var cameras = context.Cameras.OrderBy(c => c.Manufacturer.Name + c.Model);

            foreach (var camera in cameras)
            {
                Console.WriteLine("{0} {1}",camera.Manufacturer.Name, camera.Model);
            }
        }
    }
}
