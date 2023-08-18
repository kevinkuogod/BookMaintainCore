using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace bookMaintain.Common.BackEnd
{
    /// <summary>Encapsulates the information that is required for using an <see cref="T:System.Web.Mvc.AuthorizeAttribute" /> attribute.</summary>
    public class AuthorizationContext : ControllerContext
    {
        /// <summary>Provides information about the action method that is marked by the <see cref="T:System.Web.Mvc.AuthorizeAttribute" /> attribute, such as its name, controller, parameters, attributes, and filters.</summary>
        /// <returns>The action descriptor for the action method that is marked by the <see cref="T:System.Web.Mvc.AuthorizeAttribute" /> attribute.</returns>
        public new virtual ActionDescriptor ActionDescriptor { get; set; }

        /// <summary>Gets or sets the result that is returned by an action method.</summary>
        /// <returns>The result that is returned by an action method.</returns>
        public ActionResult Result { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.AuthorizationContext" /> class.</summary>
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
        public AuthorizationContext()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.AuthorizationContext" /> class using the specified controller context.</summary>
        /// <param name="controllerContext">The context within which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        [Obsolete("The recommended alternative is the constructor AuthorizationContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor).")]
        public AuthorizationContext(ControllerContext controllerContext)
            : base(controllerContext)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.AuthorizationContext" /> class using the specified controller context and action descriptor.</summary>
        /// <param name="controllerContext">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="actionDescriptor">An object that provides information about an action method, such as its name, controller, parameters, attributes, and filters.</param>
        public AuthorizationContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
            : base(controllerContext)
        {
            if (actionDescriptor == null)
            {
                throw new ArgumentNullException("actionDescriptor");
            }

            ActionDescriptor = actionDescriptor;
        }
    }
}
