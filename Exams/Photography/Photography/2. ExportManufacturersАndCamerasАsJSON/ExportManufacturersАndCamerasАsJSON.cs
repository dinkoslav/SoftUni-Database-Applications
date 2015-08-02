using System.Linq;

namespace _2.ExportManufacturersАndCamerasАsJSON
{
    using _1.EntityFrameworkMappingsDBFirst;
    using System.Web.Script.Serialization;

    class ExportManufacturersАndCamerasАsJson
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();
            JavaScriptSerializer js = new JavaScriptSerializer();

            var manufacturers = context.Manufacturers
                .OrderBy(m => m.Name)
                .Select(m => new
                {
                    manufacturer = m.Name,
                    cameras = m.Cameras
                        .OrderBy(c => c.Model)
                        .Select(c => new
                        {
                            model = c.Model,
                            price = c.Price
                        })
                });

            var json = js.Serialize(manufacturers);
            System.IO.File.WriteAllText(@"..\..\manufactureres-and-cameras.json", json);
        }
    }
}
