﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.NetworkInformation;
using VidBox.Service.Dtos.Auth;
using VidBox.Service.Interfaces.Auth;
using VidBox.Service.Validators;
using VidBox.Service.Validators.Dtos.Auth;

namespace VidBox.WebApi.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto registerDto)
        {
            var validator = new RegisterValidator();
            var result = validator.Validate(registerDto);
            if (result.IsValid)
            {
                var serviceResult = await _authService.RegisterAsync(registerDto);
                return Ok(new { serviceResult.Result, serviceResult.CachedMinutes });
            }
            else return BadRequest(result.Errors);
        }

        [HttpPost("register/send-code")]
        [AllowAnonymous]
        public async Task<IActionResult> SendCodeRegisterAsync(string phone)
        {
            var result = PhoneNumberValidator.IsValid(phone);
            if (result == false) return BadRequest("Phone number is invalid!");

            var serviceResult = await _authService.SendCodeForRegisterAsync(phone);
            return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        }

        [HttpPost("register/verify")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyRegisterAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        {
            var serviceResult = await _authService.VerifyRegisterAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        //string islom;
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var validator = new LoginValidator();
            var valResult = validator.Validate(loginDto);
            if (valResult.IsValid == false) return BadRequest(valResult.Errors);

            string clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var serviceResult = await _authService.LoginAsync(loginDto);
            // Now 'clientIpAddress' contains the client's IP address.
            if (clientIpAddress== "::1")
            {
                return Ok(new { serviceResult.Result, serviceResult.Token });
            }

            else
            {
                return Ok($"Client IP Address: {clientIpAddress}");
            }
        }

        [HttpPost("reset/send-code")]
        public async Task<IActionResult> SentCodeResetPasswordAsync(string phone)
        {
            var result = PhoneNumberValidator.IsValid(phone);
            if (result == false) return BadRequest("Phone number is invalid!");
            var serviceResult = await _authService.SendCodeForResetPasswordAsync(phone);

            return Ok(new { serviceResult.Result, serviceResult.CachedVerificationMinutes });
        }

        [HttpPost("reset/verify")]
        public async Task<IActionResult> VerifyResetPasswordAsync([FromBody] VerifyRegisterDto verifyRegisterDto)
        {
            var serviceResult = await _authService.VerifyResetPasswordAsync(verifyRegisterDto.PhoneNumber, verifyRegisterDto.Code);

            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        [HttpGet]
        public IActionResult Get()
        {
            string clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            // Now 'clientIpAddress' contains the client's IP address.
            Console.WriteLine(clientIpAddress);
            return Ok($"Client IP Address: {clientIpAddress}");
        }
    }
}
