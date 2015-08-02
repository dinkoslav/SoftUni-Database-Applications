using System.Linq;
using System.Xml.XPath;

namespace Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Linq;

    class Catalog
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../catalog.xml");

            XmlNode rootNode = doc.DocumentElement;

            // Problem 2.DOM Parser: Extract Album Names

            //foreach (XmlNode node in rootNode.ChildNodes)
            //{
            //    Console.WriteLine("Album name: " + node.Attributes["name"].Value);
            //}

            // Problem 3.DOM Parser: Extract All Artists Alphabetically

            //SortedSet<string> artists = new SortedSet<string>();

            //foreach (XmlNode node in rootNode.ChildNodes)
            //{
            //    artists.Add(node.Attributes["artist"].Value);
            //}

            //foreach (var artist in artists)
            //{
            //    Console.WriteLine(artist);
            //}

            // Problem 4.DOM Parser: Extract Artists and Number of Albums

            //Dictionary<string, int> artistAlbums = new Dictionary<string, int>();

            //foreach (XmlNode node in rootNode.ChildNodes)
            //{
            //    string artist = node.Attributes["artist"].Value;

            //    if (artistAlbums.ContainsKey(artist))
            //    {
            //        artistAlbums[artist] += 1;
            //    }
            //    else
            //    {
            //        artistAlbums.Add(artist, 1);
            //    }
            //}

            //foreach (var artistAlbum in artistAlbums)
            //{
            //    Console.WriteLine("{0} have {1} albums.", artistAlbum.Key, artistAlbum.Value);
            //}

            // Problem 5.XPath: Extract Artists and Number of Albums

            //Dictionary<string, int> artistAlbums2 = new Dictionary<string, int>();

            //string xPathQuery = "/albums/album";

            //XmlNodeList artistAlbumsList = doc.SelectNodes(xPathQuery);
            //foreach (XmlNode artistNode in artistAlbumsList)
            //{
            //    string artist = artistNode.Attributes["artist"].Value;

            //    if (artistAlbums2.ContainsKey(artist))
            //    {
            //        artistAlbums2[artist] += 1;
            //    }
            //    else
            //    {
            //        artistAlbums2.Add(artist, 1);
            //    }
            //}

            //foreach (var artistAlbum in artistAlbums2)
            //{
            //    Console.WriteLine("{0} have {1} albums.", artistAlbum.Key, artistAlbum.Value);
            //}

            // Problem 6.DOM Parser: Delete Albums

            //foreach (XmlNode albumNode in rootNode.ChildNodes)
            //{
            //    if (Decimal.Parse(albumNode.Attributes["price"].Value) < 20)
            //    {
            //        albumNode.ParentNode.RemoveChild(albumNode);
            //    }
            //}

            //doc.Save("../../../cheap-albums-catalog.xml");

            // Problem 7.DOM Parser and XPath: Old Albums

            //string xPathQuery = "/albums/album";

            //XmlNodeList artistAlbumsList2 = doc.SelectNodes(xPathQuery);
            //foreach (XmlNode node in artistAlbumsList2)
            //{
            //    DateTime targetYear = DateTime.Now.AddYears(-5);
            //    if (int.Parse(node.Attributes["year"].Value) <= targetYear.Year)
            //    {
            //        Console.WriteLine("{0} cost {1} leva.", node.Attributes["name"].Value, node.Attributes["price"].Value);
            //    }
            //}

            // Problem 8.LINQ to XML: Old Albums

            //XDocument catalog = XDocument.Load(@"..\..\..\catalog.xml");

            //var oldAlbums =
            //    (from album in catalog.Descendants("album")
            //     where Decimal.Parse(album.Attribute("year").Value) <= (DateTime.Now.Year - 5)
            //     select new
            //     {
            //         Name = album.Attribute("name").Value,
            //         Price = album.Attribute("price").Value
            //     }
            //    ).ToList();

            //foreach (var album in oldAlbums)
            //{
            //    Console.WriteLine("Album Title : {0}, Album Price : {1}", album.Name, album.Price);
            //}
        }
    }
}
