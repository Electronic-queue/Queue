﻿using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Windows.Queries.GetWindowList
{
    public class GetWindowLisQuerytHandler(IWindowRepository windowRepository):IRequestHandler<GetWindowListQuery,Result>
    {
        public async Task<Result> Handle(GetWindowListQuery request,CancellationToken cancellationToken)
        {
            var windowQuery = await windowRepository.GetAllAsync();
            var window = windowQuery.Value;
            return Result.Success(window);
        }
    }
}