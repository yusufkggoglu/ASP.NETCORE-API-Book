using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record TokenDto
    {
        [Required(ErrorMessage = "AccessToken is a required field.")]
        public String AccessToken { get; init; }

        [Required(ErrorMessage = "RefreshToken is a required field.")]
        public String RefreshToken { get; init; }
    }
}
