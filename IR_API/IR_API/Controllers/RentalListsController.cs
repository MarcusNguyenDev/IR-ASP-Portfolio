using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using IR_API.Models;

namespace IR_API.Controllers
{
    public class RentalListsController : ApiController
    {
        private ItemRentalEntities db = new ItemRentalEntities();

        // GET: api/RentalLists
        public IQueryable<RentalList> GetRentalLists()
        {
            return db.RentalLists;
        }

        // GET: api/RentalLists/5
        [ResponseType(typeof(RentalList))]
        public IHttpActionResult GetRentalList(int id)
        {
            RentalList rentalList = db.RentalLists.Find(id);
            if (rentalList == null)
            {
                return NotFound();
            }

            return Ok(rentalList);
        }

        [HttpGet]
        [Route("api/RentalItemsById/{id}")]
        [ResponseType(typeof(RentalList))]
        public IQueryable<RentalList> GetRentalItemsById(int Id)
        {
            return db.RentalLists.Where(ri => ri.RentalId == Id);
        }


        // PUT: api/RentalLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRentalList(int id, RentalList rentalList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rentalList.RentalListId)
            {
                return BadRequest();
            }

            db.Entry(rentalList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalListExists(id))
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

        // POST: api/RentalLists
        [ResponseType(typeof(RentalList))]
        public IHttpActionResult PostRentalList(RentalList rentalList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RentalLists.Add(rentalList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RentalListExists(rentalList.RentalListId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rentalList.RentalListId }, rentalList);
        }

        // DELETE: api/RentalLists/5
        [ResponseType(typeof(RentalList))]
        public IHttpActionResult DeleteRentalList(int id)
        {
            RentalList rentalList = db.RentalLists.Find(id);
            if (rentalList == null)
            {
                return NotFound();
            }

            db.RentalLists.Remove(rentalList);
            db.SaveChanges();

            return Ok(rentalList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RentalListExists(int id)
        {
            return db.RentalLists.Count(e => e.RentalListId == id) > 0;
        }
    }
}