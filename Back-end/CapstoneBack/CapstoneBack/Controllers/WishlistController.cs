﻿using Microsoft.AspNetCore.Mvc;
using CapstoneBack.Services.Interfaces;
using System.Security.Claims;
using CapstoneBack.Models.DTO.WishlistDTO;
using Microsoft.AspNetCore.Authorization;

namespace CapstoneBack.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

      
        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistCreateDto wishlistDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var wishlistItem = await _wishlistService.AddToWishlistAsync(userId, wishlistDto);
            return CreatedAtAction(nameof(GetWishlistItem), new { id = wishlistItem.WishlistId }, wishlistItem);
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishlistItem(int id)
        {
            var wishlistItem = await _wishlistService.GetWishlistItemByIdAsync(id);
            if (wishlistItem == null)
            {
                return NotFound();
            }
            return Ok(wishlistItem);
        }


       
        [HttpGet]
        public async Task<IActionResult> GetAllWishlistItems()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var wishlistItems = await _wishlistService.GetWishlistByUserIdAsync(userId);
            return Ok(wishlistItems);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWishlistItem(int id, [FromBody] WishlistUpdateDto updateDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var updatedWishlistItem = await _wishlistService.UpdateWishlistItemAsync(userId, id, updateDto);
            if (updatedWishlistItem == null)
            {
                return NotFound();
            }
            return Ok(updatedWishlistItem);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromWishlist(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var success = await _wishlistService.RemoveFromWishlistAsync(userId, id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("transfer-to-cart/{wishlistId}")]
        public async Task<IActionResult> TransferToCart(int wishlistId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool result = await _wishlistService.TransferToCartAsync(userId, wishlistId);

            if (!result)
            {
                return NotFound("Wishlist item not found or failed to transfer.");
            }
            return Ok("Item transferred to cart successfully.");
        }

    }
}
