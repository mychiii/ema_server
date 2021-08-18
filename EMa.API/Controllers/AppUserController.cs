using EMa.Common.Helpers;
using EMa.Data.DataContext;
using EMa.Data.Entities;
using EMa.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EMa.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class AppUserController : Controller
    {
        private readonly DataDbContext _context;

        public AppUserController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetAll()
        {
            return await _context.AppUsers.ToListAsync();
        }

        [HttpGet("profile")]
        public async Task<ActionResult<AppUser>> Profile()
        {
            string tokenString = Request.Headers["Authorization"].ToString();
            // Get UserId, ChildName, PhoneNumber from token
            var infoFromToken = Authorization.GetInfoFromToken(tokenString);
            var userId = infoFromToken.Result.UserId;

            var quizType = await _context.AppUsers.FindAsync(Guid.Parse(userId));

            if (quizType == null)
            {
                return NotFound();
            }

            return Ok(quizType);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> PutUserInformation(UpdateProfileViewModel model)
        {
            string tokenString = Request.Headers["Authorization"].ToString();
            // Get UserId, ChildName, PhoneNumber from token
            var infoFromToken = Authorization.GetInfoFromToken(tokenString);
            var userId = infoFromToken.Result.UserId;

            AppUser updateItem = new AppUser()
            {
                Id = Guid.Parse(userId),
                ParentName = model.ParentName,
                ParentAge = model.ParentAge,
                ChildName = model.ChildName,
                ChildBirth = model.ChildBirth,
                ChildGender = model.ChildGender,
                IdentityCard = model.IdentityCard,
                Address = model.Address,
                UrlAvatar = model.UrlAvatar
            };
            _context.Entry(updateItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckExists(Guid.Parse(userId)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(updateItem);
        }

        //[HttpDelete("hide/{id}")]
        //public async Task<IActionResult> Hide(Guid id)
        //{
        //    string tokenString = Request.Headers["Authorization"].ToString();
        //    // Get UserId, ChildName, PhoneNumber from token
        //    var infoFromToken = Authorization.GetInfoFromToken(tokenString);
        //    var userId = infoFromToken.Result.UserId;

        //    AppUser updateItem = new AppUser()
        //    {
        //        Id = id,
        //        DeletedBy = userId,
        //        IsActive = true,
        //        IsDeleted = true
        //    };
        //    _context.Entry(updateItem).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CheckExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(updateItem);
        //}

        private bool CheckExists(Guid id)
        {
            return _context.AppUsers.Any(e => e.Id == id);
        }
    }
}
