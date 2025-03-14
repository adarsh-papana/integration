using DigitalBookStoreManagement.Authentication;
using DigitalBookStoreManagement.Model;
using DigitalBookStoreManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBookStoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IAuth jwtAuth;

        public UserController(IUserService service, IAuth jwtAuth)
        {
            this.service = service;
            this.jwtAuth = jwtAuth;

        }
        //Get all the data stored in database 
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(service.GetUserInfo());
        }

        //Get the particular data according to the id
        [Authorize(Roles = "Customer")]
        [HttpGet("ById/{id}")]
        //[Route("ById/{id}")]
        public ActionResult Get(int id)
        {

            return Ok(service.GetUserInfo(id));
        }

        //Insert the data in the data 
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post(User userInfo)
        {
            return Ok(service.AddUser(userInfo));
        }

        //Delete the record from the database 
        //[HttpDelete]
        //[Route("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    return Ok(service.RemoveUser(id));
        //}

        //Update the record
        [AllowAnonymous]
        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(int id, User userInfo)
        {
            return Ok(service.UpdateUser(id, userInfo));
        }

        //Authentication of the user
        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential credential)
        {
            var token = jwtAuth.Authentication(credential.Email, credential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
