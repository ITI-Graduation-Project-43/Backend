﻿using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : BaseController<Wishlist, WishlistDto, int>
    {
        private readonly IWishlistService _wishlistService;
        private readonly WishlistMappingService _wishlistMappingService;

        public WishlistController(IWishlistService wishlistService, WishlistMappingService wishlistMappingService) : base(wishlistMappingService)
        {
            _wishlistService = wishlistService ?? throw new ArgumentNullException(nameof(wishlistService));
            _wishlistMappingService = wishlistMappingService ?? throw new ArgumentNullException(nameof(wishlistMappingService));
        }

        #region GET

        // GET: api/Wishlist
        [HttpGet]
        public async Task<ActionResult<IQueryable<WishlistDto>>> GetAllWishlist([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(_wishlistService.GetAllAsync, pagination, "Wishlists", e => e.Course, e => e.Student);
        }

        // GET: api/Wishlist/Student/{StudentId}

        [HttpGet("Student/{StudentId}")]
        public async Task<ActionResult<IEnumerable<WishlistDto>>> GetWishlistsByStudentId(string StudentId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _wishlistService.GetAllByStudentIdAsync(StudentId), pagination, "Wishlists");
        }

        // GET: api/Wishlist/Course/{CourseId}

        [HttpGet("Course/{CourseId}")]
        public async Task<ActionResult<IEnumerable<WishlistDto>>> GetWishlistsByCourseId(int CourseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _wishlistService.GetAllByCourseIdAsync(CourseId), pagination, "Wishlists");
        }

        // GET: api/Wishlist/{wishlistId}
        [HttpGet("{wishlistId}")]
        public async Task<ActionResult<WishlistDto>> GetWishlistById(int wishlistId)
        {
            return await GetEntityResponse(() => _wishlistService.GetByIdAsync(wishlistId, w => w.Course, w => w.Student), "Wishlist");
        }

        #endregion GET

        #region Add

        // POST: api/Wishlist
        [HttpPost]
        public async Task<ActionResult<WishlistDto>> AddWishlist([FromBody] WishlistDto wishlistDTO)
        {
            return await AddEntityResponse(_wishlistService.AddAsync, wishlistDTO, "Wishlist", nameof(GetWishlistById));
        }

        #endregion Add

        #region Delete

        // DELETE: api/Wishlist/Delete/{wishlistId}
        [HttpDelete("Delete/{wishlistId}")]
        public async Task<IActionResult> DeleteWishlist(int wishlistId)
        {
            return await DeleteEntityResponse(_wishlistService.GetByIdAsync, _wishlistService.DeleteAsync, wishlistId);
        }

        // DELETE: api/Wishlist/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var course = await _wishlistService.GetByIdAsync(id);

            if (course == null)
                return NotFound(NotFoundResponse("Course"));
            await _wishlistService.SoftDeleteAsync(id);
            return NoContent();
        }

        #endregion Delete

        #region Edit Put

        // PUT: api/Wishlist/{wishlistId}
        [HttpPut("{wishlistId}")]
        public async Task<ActionResult> UpdateWishlist(int wishlistId, WishlistDto wishlistDto)
        {
            return await UpdateEntityResponse(_wishlistService.GetByIdAsync, _wishlistService.UpdateAsync, wishlistId, wishlistDto, "Wishlist");
        }

        #endregion Edit Put
    }
}