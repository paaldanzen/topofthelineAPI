﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using topoftheline;

namespace topoftheline.Controllers
{
    public class FoodsController : ApiController
    {
        private topofContext db = new topofContext();

        // GET: api/Foods
        public IQueryable<Food> GetFoods()
        {
            return db.Foods;
        }

        // GET: api/Foods/5
        [ResponseType(typeof(Food))]
        public IHttpActionResult GetFood(int id)
        {
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        // PUT: api/Foods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFood(int id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.FoodID)
            {
                return BadRequest();
            }

            db.Entry(food).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Foods
        [ResponseType(typeof(Food))]
        public IHttpActionResult PostFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Foods.Add(food);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = food.FoodID }, food);
        }

        // DELETE: api/Foods/5
        [ResponseType(typeof(Food))]
        public IHttpActionResult DeleteFood(int id)
        {
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            db.Foods.Remove(food);
            db.SaveChanges();

            return Ok(food);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodExists(int id)
        {
            return db.Foods.Count(e => e.FoodID == id) > 0;
        }

    }
}