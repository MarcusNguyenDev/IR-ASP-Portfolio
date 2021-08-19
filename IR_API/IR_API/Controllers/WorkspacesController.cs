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
    public class WorkspacesController : ApiController
    {
        private ItemRentalEntities db = new ItemRentalEntities();

        // GET: api/Workspaces
        public IQueryable<Workspace> GetWorkspaces()
        {
            return db.Workspaces;
        }

        // GET: api/Workspaces/5
        [ResponseType(typeof(Workspace))]
        public IHttpActionResult GetWorkspace(int id)
        {
            Workspace workspace = db.Workspaces.Find(id);
            if (workspace == null)
            {
                return NotFound();
            }

            return Ok(workspace);
        }

        // PUT: api/Workspaces/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkspace(int id, Workspace workspace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workspace.WorkspaceId)
            {
                return BadRequest();
            }

            db.Entry(workspace).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkspaceExists(id))
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

        // POST: api/Workspaces
        [ResponseType(typeof(Workspace))]
        public IHttpActionResult PostWorkspace(Workspace workspace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workspaces.Add(workspace);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (WorkspaceExists(workspace.WorkspaceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = workspace.WorkspaceId }, workspace);
        }

        // DELETE: api/Workspaces/5
        [ResponseType(typeof(Workspace))]
        public IHttpActionResult DeleteWorkspace(int id)
        {
            Workspace workspace = db.Workspaces.Find(id);
            if (workspace == null)
            {
                return NotFound();
            }

            db.Workspaces.Remove(workspace);
            db.SaveChanges();

            return Ok(workspace);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkspaceExists(int id)
        {
            return db.Workspaces.Count(e => e.WorkspaceId == id) > 0;
        }
    }
}