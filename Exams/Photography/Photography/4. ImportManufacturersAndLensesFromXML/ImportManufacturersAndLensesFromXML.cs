using System.Linq;

namespace _4.ImportManufacturersAndLensesFromXML
{
    using _1.EntityFrameworkMappingsDBFirst;
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System.Xml.XPath;

    class ImportManufacturersAndLensesFromXML
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();

            XDocument xmlDocument = XDocument.Load("../../manufacturers-and-lenses.xml");
            var xManufacturers = xmlDocument.XPathSelectElements("/manufacturers-and-lenses/manufacturer");
            int count = 0;

            foreach (var xManufacturer in xManufacturers)
            {
                Console.WriteLine("Processing manufacturer #{0} ...", ++count);
                Manufacturer manufacturer = CreateNewManufacturerIfNotExists(context, xManufacturer);
                var xLenses = xManufacturer.XPathSelectElements("lenses/lens");
                CreateNewLensesIfNotExists(context, manufacturer, xLenses);
                Console.WriteLine();
            }
        }

        private static void CreateNewLensesIfNotExists(PhotographySystemEntities context, Manufacturer manufacturer, IEnumerable<XElement> xLenses)
        {
            foreach (var xLense in xLenses)
            {
                var lenseModel = xLense.Attribute("model").Value;
                var lenseType = xLense.Attribute("type").Value;
                var xLensePrice = xLense.Attribute("price");
                string lensePrice = null;
                if (xLensePrice != null)
                {
                    lensePrice = xLensePrice.Value;
                }

                var lense = context.Lenses
                    .FirstOrDefault(l => l.Model == lenseModel);

                if (lense != null)
                {
                    Console.WriteLine("Existing lens: {0}", lenseModel);
                }
                else
                {
                    lense = new Lens()
                    {
                        Model = lenseModel,
                        Type = lenseType
                    };

                    if (lensePrice != null)
                    {
                        lense.Price = decimal.Parse(lensePrice);
                    }

                    context.Lenses.Add(lense);
                }

                AddLensToManufacturer(context, manufacturer, lense);
            }
        }

        private static void AddLensToManufacturer(PhotographySystemEntities context, Manufacturer manufacturer, Lens lense)
        {
            if (manufacturer != null)
            {
                if (manufacturer.Lenses.Contains(lense))
                {
                    Console.WriteLine("Existing lens: {0}", lense.Model);
                }
                else
                {
                    manufacturer.Lenses.Add(lense);
                    context.SaveChanges();
                    Console.WriteLine("Created lens: {0}", lense.Model);
                }
            }
        }

        private static Manufacturer CreateNewManufacturerIfNotExists(PhotographySystemEntities context, XElement xManufacturer)
        {
            Manufacturer manufacturer = null;
            var xManufacturerName = xManufacturer.Element("manufacturer-name");
            if (xManufacturerName != null)
            {
                string manufacturerName = xManufacturerName.Value;
                manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == manufacturerName);
                if (manufacturer != null)
                {
                    Console.WriteLine("Existing manufacturer: {0}", manufacturerName);
                }
                else
                {
                    manufacturer = new Manufacturer(){ Name = manufacturerName};
                    context.Manufacturers.Add(manufacturer);
                    context.SaveChanges();
                    Console.WriteLine("Created manufacturer: {0}", manufacturerName);
                }
            }

            return manufacturer;
        }
    }
}
