using KDS.Primitives.FluentResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Commands.UpdateWindow;

public record UpdateWindowCommand(int WindowId,
    int WindowNumber,
     int WindowStatusId,
        int CreatedBy) :IRequest<Result>;
