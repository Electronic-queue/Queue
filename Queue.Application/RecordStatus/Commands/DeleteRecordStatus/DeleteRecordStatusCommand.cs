using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.RecordStatus.Commands.DeleteRecordStatus;

public record DeleteRecordStatusCommand(int RecordStatusId):IRequest<Result>;
