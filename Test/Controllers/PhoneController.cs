using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductDB;
using ProductDB.Entitys;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Test.Service;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly IEntityService<Phone> _phoneService;

        public PhoneController(IEntityService<Phone> phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> GetAll()
        {
            var phones = await _phoneService.GetAll();
            return Ok(phones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Phone>> GetById(int id)
        {
            try
            {
                var phone = await _phoneService.GetById(id);
                return Ok(phone);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Phone>> Add(Phone phone)
        {
            await _phoneService.Add(phone);
            return Ok(phone);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Phone phone)
        {
            try
            {
                await _phoneService.Update(id, phone);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _phoneService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }
    }
}