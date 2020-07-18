using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PunkAPI.Controllers;


namespace PunkAPITest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            BeerController controller = new BeerController();
            var result = controller.GetBeerReviewsByBeerName("Buzz");
        }
    }
}
