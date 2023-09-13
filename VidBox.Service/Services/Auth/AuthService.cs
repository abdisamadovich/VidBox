using Microsoft.Extensions.Caching.Memory;
using VidBox.DataAccess.Interfaces.Users;
using VidBox.Domain.Entities.Users;
using VidBox.Domain.Exceptions.Auth;
using VidBox.Domain.Exceptions.Users;
using VidBox.Service.Common.Helpers;
using VidBox.Service.Common.Security;
using VidBox.Service.Dtos.Auth;
using VidBox.Service.Dtos.Notification;
using VidBox.Service.Dtos.Security;
using VidBox.Service.Interfaces;
using VidBox.Service.Interfaces.Auth;

namespace VidBox.Service.Services.Auth;

public class AuthService : IAuthService
{

    private readonly IMemoryCache _memoryCache;
    private readonly IUserRepository _userRepository;
    private readonly ISmsSender _smsSender;
    private readonly ITokenService _tokenService;
    private const int CACHED_MINUTES_FOR_REGISTER = 60;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 50;
    private const string REGISTER_CACHE_KEY = "register_";
    private const string VERIFY_REGISTER_CACHE_KEY = "verify_register_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
    public AuthService(IMemoryCache memoryCache,
        IUserRepository userRepository,
        ISmsSender smsSender,
        ITokenService tokenService)
    {
        this._memoryCache = memoryCache;
        this._userRepository = userRepository;
        this._smsSender = smsSender;
        this._tokenService = tokenService;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByPhoneAsync(loginDto.PhoneNumber);
        if (user is null) throw new UserNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, user.PasswordHash, user.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenService.GenerateToken(user);
        return (Result: true, Token: token);
    }

    public async Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto)
    {
        var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
        if (user is not null) throw new UserAlreadyExistException(dto.PhoneNumber);

        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + dto.PhoneNumber, out RegisterDto cachedRegisterDto))
        {
            cachedRegisterDto.Name = cachedRegisterDto.Name;
            _memoryCache.Remove(REGISTER_CACHE_KEY + dto.PhoneNumber);
        }
        else _memoryCache.Set(REGISTER_CACHE_KEY + dto.PhoneNumber, dto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_REGISTER));

        return (Result: true, CachedMinutes: CACHED_MINUTES_FOR_REGISTER);
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
        {
            VerificationDto verificationDto = new VerificationDto();
            verificationDto.Attempt = 0;
            verificationDto.CreatedAt = TimeHelper.GetDateTime();

            // make confirm code as random
            verificationDto.Code = 1234;//CodeGenerator.GenerateRandomNumber();

            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
            {
                _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
            }

            _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

            SmsMessage smsMessage = new SmsMessage();
            smsMessage.Title = "ProFex";
            smsMessage.Content = "Your verification code : " + verificationDto.Code;
            smsMessage.Recipent = phone.Substring(1);

            var smsResult = true;//await _smsSender.SendAsync(smsMessage);
            if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
            else return (Result: false, CachedVerificationMinutes: 0);
        }
        else throw new UserCacheDataExpiredException();
    }

    public async Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(REGISTER_CACHE_KEY + phone, out RegisterDto registerDto))
        {
            if (_memoryCache.TryGetValue(VERIFY_REGISTER_CACHE_KEY + phone, out VerificationDto verificationDto))
            {
                if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                    throw new VerificationTooManyRequestsException();
                else if (verificationDto.Code == code)
                {
                    var dbResult = await RegisterToDatabaseAsync(registerDto);
                    if (dbResult is true)
                    {
                        var user = await _userRepository.GetByPhoneAsync(phone);
                        string token = _tokenService.GenerateToken(user);
                        return (Result: true, Token: token);
                    }
                    else return (Result: false, Token: "");
                }
                else
                {
                    _memoryCache.Remove(VERIFY_REGISTER_CACHE_KEY + phone);
                    verificationDto.Attempt++;
                    _memoryCache.Set(VERIFY_REGISTER_CACHE_KEY + phone, verificationDto,
                        TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));
                    return (Result: false, Token: "");
                }
            }
            else throw new VerificationCodeExpiredException();
        }
        else throw new UserCacheDataExpiredException();
    }

    private async Task<bool> RegisterToDatabaseAsync(RegisterDto registerDto)
    {
        var user = new User();
        user.Name = registerDto.Name;
        user.PhoneNumber = registerDto.PhoneNumber;
        user.PhoneNumberConfirmed = true;
        var haserResult = PasswordHasher.Hash(registerDto.Password);
        user.PasswordHash = haserResult.Hash;
        user.Salt = haserResult.Salt;
        user.CreatedAt = user.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _userRepository.CreateAsync(user);

        return dbResult > 0;
    }

    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForResetPasswordAsync(string phone)
    {
        var user = await _userRepository;
        if (user is null) throw new UserNotFoundException();
        VerificationDto verificationDto = new VerificationDto();
        verificationDto.Attempt = 0;
        verificationDto.CreatedAt = TimeHelper.GetDateTime();
        //verificationDto.Code = CodeGenerator.GenerateRandomNumber();
        verificationDto.Code = 11111;

        if (_memoryCache.TryGetValue(VERIFY_RESET_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
        {
            _memoryCache.Remove(VERIFY_RESET_CACHE_KEY + phone);
        }

        _memoryCache.Set(VERIFY_RESET_CACHE_KEY + phone, verificationDto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

        SmsMessage smsMessage = new SmsMessage();
        smsMessage.Title = "On Med";
        smsMessage.Content = "Sizning tasdiqlash kodingiz : " + verificationDto.Code;
        smsMessage.Recipent = phone.Substring(1);


        var smsResult = true; //await _smsSender.SendAsync(smsMessage);
        if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
        else return (Result: false, CachedVerificationMinutes: 0);
    }

    public Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code)
    {
        throw new NotImplementedException();
    }
}
