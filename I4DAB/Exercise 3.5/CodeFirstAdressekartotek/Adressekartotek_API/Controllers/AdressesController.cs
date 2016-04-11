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
    public class AdressesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Adresses
        public IQueryable<Adresse> GetAdresses()
        {
            return db.Adresses;
        }

        // GET: api/Adresses/5
        [ResponseType(typeof(Adresse))]
        public async Task<IHttpActionResult> GetAdresse(long id)
        {
            Adresse adresse = await db.Adresses.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }

            return Ok(adresse);
        }

        // PUT: api/Adresses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAdresse(long id, Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != adresse.AdresseID)
            {
                return BadRequest();
            }

            db.Entry(adresse).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresseExists(id))
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

        // POST: api/Adresses
        [ResponseType(typeof(Adresse))]
        public async Task<IHttpActionResult> PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Adresses.Add(adresse);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = adresse.AdresseID }, adresse);
        }

        // DELETE: api/Adresses/5
        [ResponseType(typeof(Adresse))]
        public async Task<IHttpActionResult> DeleteAdresse(long id)
        {
            Adresse adresse = await db.Adresses.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }

            db.Adresses.Remove(adresse);
            await db.SaveChangesAsync();

            return Ok(adresse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdresseExists(long id)
        {
            return db.Adresses.Count(e => e.AdresseID == id) > 0;
        }
    }
}