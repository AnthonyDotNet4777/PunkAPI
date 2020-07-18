using PunkAPI.DataTransferObject;
using PunkAPI.Helpers;
using PunkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace PunkAPI.Controllers
{
    public class BeerController : ApiController
    {
        public BeerController()
        {
            PunkAPIHelper.Initialize();
        }

        [HttpPost]
        [Route("addBeerRating")]
        public async Task<IHttpActionResult> AddBeerRating(int id, BeerReviewObject reviewObject)
        {
            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Json(new { message });
            }

            bool isIdValid = await PunkAPIHelper.ValidateID(id);
            if (!isIdValid)
            {
                return BadRequest("No beer exists for this id '" + id + "'");
            }

            InputValidation review = new InputValidation
            {
                id = id,
                Username = reviewObject.Username,
                Rating = reviewObject.Rating,
                Comments = reviewObject.Comments
            };

            JSONHelper.AddReview(review);

            return Ok("Beer Review inserted into database.json");
        }

        [HttpGet]
        [Route("getBeerReviewsByBeerName")]
        public async Task<IHttpActionResult> GetBeerReviewsByBeerName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return BadRequest("Input parameter name is null or empty");
            }

            var beers = await PunkAPIHelper.GetBeerByName(name);
            var reviews = JSONHelper.FetchAllReviews();

            var beerReviews = from b in beers
                              join r in reviews
                              on b.id equals r.id into br
                              select new
                              {
                                  Id = b.id,
                                  Name = b.name,
                                  Description = b.description,
                                  UserRatings = ConvertToReviews(br)
                              };

            return Ok(beerReviews);
        }

        private IEnumerable<BeerReviewObject> ConvertToReviews(IEnumerable<InputValidation> beerReviews)
        {
            List<BeerReviewObject> ReturnReviews = new List<BeerReviewObject>();

            foreach (var beerReview in beerReviews)
            {
                ReturnReviews.Add(new BeerReviewObject
                {
                    Comments = beerReview.Comments,
                    Username = beerReview.Username,
                    Rating = beerReview.Rating
                });
            }
            return ReturnReviews;
        }
    }
}
