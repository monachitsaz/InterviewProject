using InterviewProject.Helpers;
using InterviewProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InterviewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("shell")]
    [EnableCors("interview-project")]
    public class UserController : ControllerBase
    {
        private IUserHelper _userHelper;

        public UserController(IUserHelper userHelper)
        {
            this._userHelper = userHelper;

        }
        /// <summary>
        /// Show all users details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result= this._userHelper.GetUsers();
            return Ok(result);
        }

        /// <summary>
        /// Show details of one user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[EnableCors("sidenav")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user=this._userHelper.GetById(id);
            return Ok(user);
        }

        /// <summary>
        /// Add one user
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {          
                var message= this._userHelper.InsertUser(model);              
                return Ok(message);
          
        }

        /// <summary>
        /// Update details of user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] User model)
        {
            var message = this._userHelper.UpdateUser(model);
            return Ok(message);
        }

        /// <summary>
        /// Delete one user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await this._userHelper.DeleteUser(id);
            return Ok(message);
        }
    }
}
