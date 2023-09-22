﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var validator = new LoginValidator();
            var valResult = validator.Validate(loginDto);
            if (valResult.IsValid == false) return BadRequest(valResult.Errors);

            var serviceResult = await _authService.LoginAsync(loginDto);
            return Ok(new { serviceResult.Result, serviceResult.Token });
        }

        /*[HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var validator = new LoginValidator();
            var valResult = validator.Validate(loginDto);
            if (valResult.IsValid == false) return BadRequest(valResult.Errors);

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface netInterface in networkInterfaces)
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    if (netInterface != null)
                    {
                        UnicastIPAddressInformation wifiIpAddress = netInterface.GetIPProperties()
                            .UnicastAddresses
                            .FirstOrDefault(address => address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

                        if (wifiIpAddress != null)
                        {
                            // Foydalanuvchi IP manzili "10.10.3.241" ga teng bo'lmasa, kirishni rad etamiz
                            if (wifiIpAddress.Address.ToString() != "172.20.10.11")
                            {
                                return Unauthorized(); // 401 Unauthorized status kodni qaytarish
                            }
                            else
                            {
                                var serviceResult = await _authService.LoginAsync(loginDto);
                                return Ok(new { serviceResult.Result, serviceResult.Token });
                            }
                        }
                    }
                }
            }
            return Ok();
        }
*/
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
    }
}
