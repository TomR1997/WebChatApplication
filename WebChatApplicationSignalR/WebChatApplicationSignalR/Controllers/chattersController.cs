using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebChatApplicationSignalR;

namespace WebChatApplicationSignalR.Controllers
{
    public class chattersController : ApiController
    {
        private MsSqlChatConnection db = new MsSqlChatConnection();

        // GET: api/chatters
        public IQueryable<chatter> Getchatters()
        {
            return db.chatters;
        }

        // GET: api/chatters/5
        [ResponseType(typeof(chatter))]
        public async Task<IHttpActionResult> Getchatter(int id)
        {
            chatter chatter = await db.chatters.FindAsync(id);
            if (chatter == null)
            {
                return NotFound();
            }

            return Ok(chatter);
        }

        // PUT: api/chatters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putchatter(int id, chatter chatter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != chatter.ChatterId)
            {
                return BadRequest();
            }

            db.Entry(chatter).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!chatterExists(id))
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

        // POST: api/chatters
        [ResponseType(typeof(chatter))]
        public async Task<IHttpActionResult> Postchatter(chatter chatter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.chatters.Add(chatter);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (chatterExists(chatter.ChatterId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = chatter.ChatterId }, chatter);
        }

        // DELETE: api/chatters/5
        [ResponseType(typeof(chatter))]
        public async Task<IHttpActionResult> Deletechatter(int id)
        {
            chatter chatter = await db.chatters.FindAsync(id);
            if (chatter == null)
            {
                return NotFound();
            }

            db.chatters.Remove(chatter);
            await db.SaveChangesAsync();

            return Ok(chatter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool chatterExists(int id)
        {
            return db.chatters.Count(e => e.ChatterId == id) > 0;
        }
    }
}