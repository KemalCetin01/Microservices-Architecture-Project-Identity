using Microsoft.AspNetCore.Mvc;
using MS.Services.Core.Base.Api;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.Login;
using MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.Register;
using MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.ResetPassword;
using MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;
using MS.Services.Identity.Application.Handlers.Auth.Queries;

namespace MS.Services.Identity.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
[ApiController]
public class B2BAuthController : BaseApiController //Business to Business Authentication Controller
{
    private readonly IRequestBus _requestBus;
    public B2BAuthController(IRequestBus requestBus)
    {
        _requestBus = requestBus;
    }
    /// <remarks>
    /// just email 
    /// 
    /// 
    ///     POST /b2b/login
    ///     {
    ///        "Email": "b2bfirm@gmail.com",
    ///        "Password":"12345"
    ///     }
    /// 
    /// </remarks>
    /// <summary>
    /// b2b login
    /// </summary>
    [HttpPost("b2b/login")]
    public async Task<IActionResult> B2BLogin([FromBody] B2BLoginCommand loginCommand)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(loginCommand));
    }

    /// <remarks>
    /// refresh token 
    /// 
    /// 
    /// 
    /// </remarks>
    /// <summary>
    /// B2B refresh token
    /// </summary>
    [HttpPost("b2b/refresh-token")]
    public async Task<IActionResult> B2BRefreshToken([FromQuery] B2BRefreshTokenCommand loginCommand)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(loginCommand));
    }



    /// <remarks>
    /// Billing Address validations
    /// 
    /// 
    /// 
    /// </remarks>
    /// <summary>
    /// Billing Address validations
    /// </summary>
    [HttpPost("b2b/validate-signup-adress")]
    public async Task<IActionResult> B2BValidateSignUpAddress([FromBody] B2BValidateSignUpAddressQuery b2BValidateSignUpAddressQuery)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(b2BValidateSignUpAddressQuery));
    }

    /// <remarks>
    /// signup - send code
    /// 
    /// 
    /// 
    /// </remarks>
    /// <summary>
    /// signup - send code
    /// </summary>
    [HttpPost("b2b/signup")]
    public async Task<IActionResult> B2BSignUp([FromForm] B2BSignUpCommand b2BSignUpCommand)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(b2BSignUpCommand));
    }

    /// <remarks>
    /// b2b verify otp
    /// 
    /// 
    /// 
    /// </remarks>
    /// <summary>
    /// b2b verify otp
    /// </summary>
    [HttpPost("b2b/verify-otp")]
    public async Task<IActionResult> B2BVerifyOtp([FromBody] B2BVerifyOtpCommand b2BVerifyOtpCommand)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(b2BVerifyOtpCommand));
    }

    /// <remarks>
    /// b2b resend otp
    /// 
    /// 
    /// 
    /// </remarks>
    /// <summary>
    /// b2b resend otp
    /// </summary>
    [HttpPost("b2b/resend-otp")]
    public async Task<IActionResult> B2BResendOtp([FromBody] B2BResendOtpCommand b2BResendOtpCommand)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(b2BResendOtpCommand));
    }

    #region reset password
    /// <remarks>
    ///b2b reset password
    /// 
    /// 
    /// 
    /// </remarks>
    /// <summary>
    /// b2b reset password
    /// </summary>
    [HttpPost("b2b/reset-password")]
    public async Task<IActionResult> B2BResetPassword([FromBody] B2BResetPasswordCommand resetPasswordCommand)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(resetPasswordCommand));
    }

    /// <remarks>
    /// b2b reset password verify otp
    /// 
    /// 
    /// 
    /// </remarks>
    /// <summary>
    /// b2b reset password verify otp
    /// </summary>
    [HttpPost("b2b/reset-verify-otp")]
    public async Task<IActionResult> B2BResetPasswordVerifyOtp([FromBody] B2BResetVerifyOtpCommand b2BResetVerifyOtpCommand)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(b2BResetVerifyOtpCommand));
    }

    [HttpPut("b2b/change-password")]
    public async Task<IActionResult> B2BChangePassword([FromBody] B2BChangePasswordCommand b2BChangePasswordCommand)
    {
        return StatusCode(StatusCodes.Status200OK, await _requestBus.Send(b2BChangePasswordCommand));
    }

    #endregion
}

