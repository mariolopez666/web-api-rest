using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace WebApplicationEjemplo.Filters
{
    public class ValidateAuthorizationRequestAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Obtiene o establece una lista delimitada por comas de roles que pueden acceder al recurso.
        /// </summary>
        public string? Roles { get; set; }
        public ValidateAuthorizationRequestAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string authorization = context.HttpContext.Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authorization))
                {
                    var mensaje = "No se ha encontrado el encabezado Authorization en la cabecera de la solicitud.";
                    context.Result = new UnauthorizedObjectResult(mensaje);
                    //throw new Exception(mensaje);//
                    return;
                }

                string token;
                if (!authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var mensaje = "Formato incorrecto del encabezado Authorization, 'Bearer' es requerido.";
                    throw new Exception(mensaje);
                }
                else
                    token = authorization.Substring("Bearer ".Length).Trim();

                if (string.IsNullOrEmpty(token))
                {
                    var mensaje = "No se ha encontrado Token en la cabecera del Request.";
                    throw new Exception(mensaje);
                }

                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var accessToResources = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "resource_access");
                var propiedades = accessToResources.Properties;
                var userId = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
                var valueJson = accessToResources.Value;

                var recursos = JsonConvert.DeserializeObject<Dictionary<string, ClsRoles>>(accessToResources.Value);
                if (recursos.ContainsKey("web-facturacion"))
                {
                    var rolesApi = Roles.Split(",");
                    var tieneRolApi = recursos["web-facturacion"].Roles.Any(rol => rolesApi.Contains(rol));
                    if (!tieneRolApi)
                    {
                        var mensaje = "No tiene el rol para invocar este metodo.";
                        //context.Result = new ObjectResult(mensaje) { StatusCode = StatusCodes.Status403Forbidden };
                        var problemDetails = new ProblemDetails();
                        problemDetails.Title = "El usuario autenticado no está autorizado.";
                        problemDetails.Detail = "El usuario <user> debe tener el rol de <role(s)>";
                        problemDetails.Status = StatusCodes.Status403Forbidden;
                        problemDetails.Instance = context.HttpContext.Request.Path;
                        context.Result = new ObjectResult(problemDetails) { StatusCode = StatusCodes.Status403Forbidden };
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

    public class ClsRoles
    {
        public string[] Roles { get; set; }
    }
}
