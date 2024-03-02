using MyBiking.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Dtos
{
    public class RegisterModelView
    {
        public RegisterUserDto RegisterUserDto { get; set; }
        public List<Nationality> Nationalities{ get; set; }
    }
}
