using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metsuoki.Application.Dtos;

public record RequestSmsCodeDto(string PhoneNumber);

public record ConfirmDto(string PhoneNumber, string Code, string Password);

public record AuthResponseDto(string Token);