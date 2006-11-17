using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SnCore.Web.Tests.SnCore.WebServices;

namespace SnCore.Web.Tests
{
    [TestFixture]
    public class WebPlaceServiceTest
    {
        [Test]
        public void GetFavoritePlacesTest()
        {
            WebPlaceService service = new WebPlaceService();
            TransitPlaceQueryOptions qo = new TransitPlaceQueryOptions();
            int total = service.GetPlacesCount(qo);
            Console.WriteLine("There're a total of {0} places.", total);
            int favorites = service.GetFavoritePlacesCount();
            Console.WriteLine("There're a total of {0} favorite places.", favorites);
            TransitPlace[] places = service.GetFavoritePlaces(null);
            Console.WriteLine("Returned {0} favorite places.", places.Length);
            Assert.AreEqual(favorites, places.Length, "The number of total favorite places and returned favorite places doesn't match.");
            Assert.IsTrue(total >= places.Length, "The number of total places and favorite places is impossible.");
        }
    }
}