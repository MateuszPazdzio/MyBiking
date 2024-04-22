using MediatR;
using MyBiking.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Application.Functions.Command.Wheelie
{
    public class DeleteWheelieCommand : IRequest<Status>
    {
        public int Id { get; set; }
    }
}
