namespace _3.ExportPhotographsAsXML
{
    using System.Linq;
    using _1.EntityFrameworkMappingsDBFirst;
    using System.Xml.Linq;
    using System;

    class ExportPhotographsAsXML
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();

            var photographs = context.Photographs
                .OrderBy(p => p.Title)
                .Select(p => new
                {
                    p.Title,
                    Category = p.Category.Name,
                    p.Link,
                    Camera = new {
                        p.Equipment.Camera.Model,
                        p.Equipment.Camera.Megapixels
                    },
                    Lens = new
                    {
                        p.Equipment.Lens.Model,
                        p.Equipment.Lens.Price
                    }
                });

            XElement photographsElement = new XElement("photographs");

            foreach (var photograph in photographs)
            {
                XElement photographElement = new XElement("photograph");
                photographElement.SetAttributeValue("title", photograph.Title);
                XElement categoryElement = new XElement("category", photograph.Category);
                photographElement.Add(categoryElement);
                XElement linkElement = new XElement("link", photograph.Link);
                photographElement.Add(linkElement);

                XElement equipmentElement = new XElement("equipment");

                XElement cameraElement = new XElement("camera", photograph.Camera.Model);
                cameraElement.SetAttributeValue("megapixels", photograph.Camera.Megapixels);
                equipmentElement.Add(cameraElement);

                XElement lensElement = new XElement("lens", photograph.Lens.Model);
                if (photograph.Lens.Price != null)
                {
                    lensElement.SetAttributeValue("price", String.Format("{0:0.00}", photograph.Lens.Price));
                }
                equipmentElement.Add(lensElement);
                photographElement.Add(equipmentElement);
                photographsElement.Add(photographElement);
            }

            var resultXmlDoc = new XDocument();
            resultXmlDoc.Add(photographsElement);
            resultXmlDoc.Save("../../photographs.xml");
        }
    }
}
