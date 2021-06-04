using AutoMapper;
using BookStore.API.Contracts;
using BookStore.API.Data;
using BookStore.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Controllers
{
    /// <summary>
    /// Endpoint used to interact with the Books in the book store's database.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class BooksController : ControllerBase
    {
        // Type is interface but dependency injection will inject the concrete implementation (class) of this interface.
        private readonly IBookRepository _bookRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(IBookRepository bookRepository, ILoggerService logger, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        private string GetImagePath(string fileName) => $"{_webHostEnvironment.ContentRootPath}\\Uploads\\{fileName}";

        /// <summary>
        /// Get all Books.
        /// </summary>
        /// <returns>A list of Books.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books = await _bookRepository.FindAll();
                var response = _mapper.Map<IList<BookDTO>>(books);

                foreach (var item in response)
                {
                    if (string.IsNullOrEmpty(item.Image) == false)
                    {
                        var imagePath = GetImagePath(item.Image);

                        if (System.IO.File.Exists(imagePath))
                        {
                            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                            item.File = Convert.ToBase64String(imageBytes);
                        }
                    }
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Get an Book by ID (primary key).
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single Book</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int id)
        {
            try
            {
                var bookDTO = await _bookRepository.FindById(id);
                if (bookDTO == null)
                {
                    return NotFound();
                }
                BookDTO response = _mapper.Map<BookDTO>(bookDTO);

                if (string.IsNullOrEmpty(response.Image) == false)
                {
                    var imagePath = GetImagePath(bookDTO.Image);

                    if (System.IO.File.Exists(imagePath))
                    {
                        byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                        response.File = Convert.ToBase64String(imageBytes);
                    }
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }            
        }

        /// <summary>
        /// Creates an Book.
        /// </summary>
        /// <param name="bookCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BookCreateDTO bookCreateDTO)
        {
            try
            {
                if (bookCreateDTO == null)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                var book = _mapper.Map<Book>(bookCreateDTO);
                var isSuccess = await _bookRepository.Create(book);

                if (isSuccess == false)
                {
                    return InternalError($"Book creation failed.");
                }

                if (string.IsNullOrEmpty(bookCreateDTO.File) == false)
                {
                    var imagePath = GetImagePath(bookCreateDTO.Image);
                    byte[] imageBytes = Convert.FromBase64String(bookCreateDTO.File);
                    System.IO.File.WriteAllBytes(imagePath, imageBytes);
                }

                return Created("Create", new { book });
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Updates an Book by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDTO bookUpdateDTO)
        {
            try
            {
                if (id < 1 || bookUpdateDTO == null || id != bookUpdateDTO.Id)
                {
                    return BadRequest();
                }
                bool exists = await _bookRepository.Exists(id);
                if (exists == false)
                {
                    return NotFound();
                }
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                var oldImageName = await _bookRepository.GetImageFileName(id);
                var book = _mapper.Map<Book>(bookUpdateDTO);
                var isSuccess = await _bookRepository.Update(book);

                if (isSuccess == false)
                {
                    return InternalError($"Update operation failed.");
                }

                if (bookUpdateDTO.Image.Equals(oldImageName) == false)
                {
                    if (System.IO.File.Exists(GetImagePath(oldImageName)))
                    {
                        System.IO.File.Delete(GetImagePath(oldImageName));
                    }
                }

                if (string.IsNullOrEmpty(bookUpdateDTO.File) == false)
                {
                    byte[] imageBytes = Convert.FromBase64String(bookUpdateDTO.File);
                    System.IO.File.WriteAllBytes(GetImagePath(bookUpdateDTO.Image), imageBytes);
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Deletes an Book by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest();
                }

                bool exists = await _bookRepository.Exists(id);
                if (exists == false)
                {
                    return NotFound();
                }

                var book = await _bookRepository.FindById(id);
                var isSuccess = await _bookRepository.Delete(book);

                if (isSuccess == false)
                {
                    return InternalError("Book deletion failed.");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong on our side. Please contact the administrator.");
        }
    }
}
