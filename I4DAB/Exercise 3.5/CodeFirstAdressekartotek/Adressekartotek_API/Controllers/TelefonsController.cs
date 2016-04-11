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
using Adressekartotek_API;

namespace Adressekartotek_API.Controllers
{
    public class TelefonsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Telefons
        public IQueryable<Telefon> GetTelefons()
        {
            return db.Telefons;
        }

        // GET: api/Telefons/5
        [ResponseType(typeof(Telefon))]
        public async Task<IHttpActionResult> GetTelefon(long id)
        {
            Telefon telefon = await db.Telefons.FindAsync(id);
            if (telefon == null)
            {
                return NotFound();
            }

            return Ok(telefon);
        }

        // PUT: api/Telefons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTelefon(long id, Telefon telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != telefon.TelefonID)
            {
                return BadRequest();
            }

            db.Entry(telefon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonExists(id))
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

        // POST: api/Telefons
        [ResponseType(typeof(Telefon))]
        public async Task<IHttpActionResult> PostTelefon(Telefon telefon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Telefons.Add(telefon);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = telefon.TelefonID }, telefon);
        }

        // DELETE: api/Telefons/5
        [ResponseType(typeof(Telefon))]
        public async Task<IHttpActionResult> DeleteTelefon(long id)
        {
            Telefon telefon = await db.Telefons.FindAsync(id);
            if (telefon == null)
            {
                return NotFound();
            }

            db.Telefons.Remove(telefon);
            await db.SaveChangesAsync();

            return Ok(telefon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TelefonExists(long id)
        {
            return db.Telefons.Count(e => e.TelefonID == id) > 0;
        }
    }
}