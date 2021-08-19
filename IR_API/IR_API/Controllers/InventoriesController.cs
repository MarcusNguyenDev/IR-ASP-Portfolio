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
    public class InventoriesController : ApiController
    {
        private ItemRentalEntities db = new ItemRentalEntities();

        // GET: api/Inventories
        public IQueryable<Inventory> GetInventories()
        {
            return db.Inventories;
        }

        // GET: api/Inventories/5
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult GetInventory(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }

        // PUT: api/Inventories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInventory(int id, Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventory.ToolId)
            {
                return BadRequest();
            }

            db.Entry(inventory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventories
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult PostInventory(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Inventories.Add(inventory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InventoryExists(inventory.ToolId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = inventory.ToolId }, inventory);
        }

        // DELETE: api/Inventories/5
        [ResponseType(typeof(Inventory))]
        public IHttpActionResult DeleteInventory(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return NotFound();
            }

            db.Inventories.Remove(inventory);
            db.SaveChanges();

            return Ok(inventory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryExists(int id)
        {
            return db.Inventories.Count(e => e.ToolId == id) > 0;
        }
    }
}