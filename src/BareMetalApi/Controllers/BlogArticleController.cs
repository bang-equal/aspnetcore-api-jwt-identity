using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BareMetalApi.Data;
using BareMetalApi.Data.Entities;

namespace BareMetalApi.Controllers
{
    [Route("blog/[controller]")]
    public class BlogArticleController : ControllerBase
    {
        private readonly IBlogArticleRepository _repository;

        public BlogArticleController(IBlogArticleRepository repository)
        {
            _repository = repository;
        }
        
        // GET blog/blogarticle
        [HttpGet]
        public IActionResult Get()
        {
            return Ok( _repository.GetAll().Result);
            
        }

        // GET blog/blogarticle/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST blog/blogarticle
        [HttpPost]
        public IActionResult Post([FromBody]BlogArticle blogarticle)
        {
            try
            {
                if (blogarticle == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TitleAndContentRequired.ToString());
                }
                bool itemExists = _repository.DoesItemExist(blogarticle.Id).Result;
                if (itemExists)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.IDInUse.ToString());
                }
                _repository.AddAsync(blogarticle);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
            return Ok(blogarticle);
        }

        // PUT blog/blogarticle/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BlogArticle blogarticle)
        {
            try
            {
                if (blogarticle == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TitleAndContentRequired.ToString());
                }
                var existingBlogArticle = _repository.GetById(id);
                if (existingBlogArticle == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _repository.UpdateAsync(blogarticle);
                
            }
            catch (Exception e)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
             try
            {
                var blogarticle = _repository.GetById(id).Result;
                if (blogarticle == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _repository.DeleteAsync(blogarticle);
            }
            catch (Exception e)
            {
                return BadRequest(ErrorCode.CouldNotDeleteItem.ToString());
            }
            return NoContent();
        }

    public enum ErrorCode
    {
        TitleAndContentRequired,
        IDInUse,
        RecordNotFound,
        CouldNotCreateItem,
        CouldNotUpdateItem,
        CouldNotDeleteItem
    }
    }
}
