using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.PublicClasses
{
	public class PermissionCheckerAttribute : Attribute, IAuthorizationFilter
	{
		private readonly int _permissionId;
		private IIdentityService _identityService;

		public PermissionCheckerAttribute(int permissionId)
		{
			_permissionId = permissionId;
		}
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if(context.HttpContext.User.Identity.IsAuthenticated)
			{
				_identityService = (IIdentityService)context.HttpContext.RequestServices.GetService(typeof(IIdentityService));
				string phoneNumeber = context.HttpContext.User.Identity.Name;

				if(!_identityService.CheckPerimission(_permissionId, phoneNumeber))
				{
					context.Result = new RedirectResult("/login");
				}

			}
			else
			{
				context.Result = new RedirectResult("/login");
			}
		}
	}
}
