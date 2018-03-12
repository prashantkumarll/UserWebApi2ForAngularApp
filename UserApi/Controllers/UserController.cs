using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UserApi.Models;

namespace UserApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        public UserDetailsEntities context = new UserDetailsEntities();

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(context.UserDs.ToList());
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("createuser")]
        public IHttpActionResult AddUser([FromBody]UserViewModel newUser)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Please provide a valid model");

                UserD model = new UserD();
                model.FirstName = newUser.FirstName;
                model.LastName = newUser.LastName;
                model.Dob = newUser.Dob;
                model.Designation = newUser.Designation;

                context.UserDs.Add(model);
                context.SaveChanges();

                return Ok(model);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (id <= 0) return NotFound();
                var data = context.UserDs.Where(x => x.Id == id).SingleOrDefault();
                if (data == null) return NotFound();

                context.UserDs.Remove(data);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
