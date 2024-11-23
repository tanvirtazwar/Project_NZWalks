﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project_NZWalks.API.CustomActionFilters;

public class ValidateModel : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if(context.ModelState.IsValid == false)
        {
            context.Result = new BadRequestResult();
        }
    }
}
